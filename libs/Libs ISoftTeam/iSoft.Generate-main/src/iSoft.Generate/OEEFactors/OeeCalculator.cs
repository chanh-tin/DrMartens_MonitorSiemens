using SourceBaseBE.MainService.OEEFactors.Interface;
using System.Collections.Generic;
using System;
using CodingSeb.ExpressionEvaluator;
using System.Linq;
using iMag.Oee.OEEFactors;
using Org.BouncyCastle.Asn1.Pkcs;
using NPOI.SS.Formula.Functions;
using SourceBaseBE.Database.Enums;
using SourceBaseBE.Database.Entities;
using iSoft.Common.Payloads;

namespace SourceBaseBE.MainService.OEEFactors
{

    public class OeeCalculator : IOeeCalculator
    {
        public Dictionary<string, object> _variables { get; set; }
        public string _availableFormula { get; set; }
        public string _performanceFormula { get; set; }
        public string _qualityFormula { get; set; }
        public string _oeeFormula { get; set; } = "default";
        public long? _operationTime { get; set; }
        public long? _plannedDowntime { get; set; }
        public long? _unplannedDowntime { get; set; }
        public long? _idealCycleTime { get; set; }
        public IOeeFactor _oeeFactor { get; set; }

        public OeeCalculator()
        {
            _variables = new Dictionary<string, object>();
        }

        public OeeCalculator(
            Dictionary<string, object> variables,
            string availableFormula = "",
            string performanceFormula = "",
            string qualityFormula = "",
            string oeeFormula = "default")
        {
            _variables = variables ?? new Dictionary<string, object>();
            _availableFormula = availableFormula;
            _performanceFormula = performanceFormula;
            _qualityFormula = qualityFormula;
            _oeeFormula = oeeFormula;
        }

        public OeeCalculator(IOeeOption option)
        {
            _variables = option?.GetVariables() ?? new Dictionary<string, object>();
            _availableFormula = option?.AvailableFormula ?? "";
            _performanceFormula = option?.PerformanceFormula ?? "";
            _qualityFormula = option?.QualityFormula ?? "";
            _oeeFormula = option?.OeeFormula ?? "default";
        }

        public IOeeCalculator AddVariables(Dictionary<string, object> variables)
        {
            foreach (var variable in variables)
            {
                _variables[variable.Key] = variable.Value;
            }
            return this;
        }

        public IOeeCalculator UseAvailableFormula(string formula)
        {
            _availableFormula = formula;
            return this;
        }

        public IOeeCalculator UsePerformanceFormula(string formula)
        {
            _performanceFormula = formula;
            return this;
        }

        public IOeeCalculator UseQualityFormula(string formula)
        {
            _qualityFormula = formula;
            return this;
        }

        public IOeeCalculator UseOeeFormula(string formula)
        {
            _oeeFormula = formula;
            return this;
        }

        public IOeeFactor Calculate()
        {
            var evaluator = new ExpressionEvaluator();
            foreach (var variable in _variables)
            {
                evaluator.Variables[variable.Key] = variable.Value;
            }

            if (_oeeFactor == null)
            {
                _oeeFactor = new OeeFactor();
            }
            var totalOutput = Convert.ToSingle(_variables["TotalOutput"]);
            var operationTime = Convert.ToSingle(_variables["OperationTime"]);
            var _P = evaluator.Evaluate(_performanceFormula);
            _oeeFactor.Performance = Convert.ToSingle(_P);

            var _Q = evaluator.Evaluate(_qualityFormula);
            _oeeFactor.Quality = Convert.ToSingle(_Q);

            var _A = evaluator.Evaluate(_availableFormula);
            _oeeFactor.Available = Convert.ToSingle(_A);

            if (_oeeFormula == "default" || _oeeFormula == null)
            {
                _oeeFactor.OEE = (float)(((Convert.ToSingle(_A)) * (Convert.ToSingle(_P)) * (Convert.ToSingle(_Q))) / 10000);
            }
            else
            {
                var _OEE = evaluator.Evaluate(_oeeFormula);
                _oeeFactor.OeeCustomValue = Convert.ToSingle(_OEE);
            }

            return _oeeFactor;
        }

        public IOeeFactor CalculateOeePointsRealtime(
                                    OeePointEntity points, 
                                    OeePointConfigEntity configuration,
                                    DevicePayloadMessage messageData,
                                    DateTime? startShift,
                                    DateTime? endShift,
                                    long breakTime)
        {

            LoadConfigurationRealtime(points, configuration, messageData, startShift, endShift, breakTime);
            var oeeFactor = Calculate();

            return oeeFactor;
        }

        public void LoadConfigurationRealtime(OeePointEntity points,
                                      OeePointConfigEntity configuration,
                                      DevicePayloadMessage messageData,
                                      DateTime? startShift,
                                      DateTime? endShift,
                                      long breakTime )
        {
            _availableFormula = "(OperationTime / PlannedProductionTime)*100.0";
            _performanceFormula = "(TotalOutput / IdealOutput / OperationTime)*100.0";
            _qualityFormula = "(GoodOutput / TotalOutput)*100.0";

            DateTime now = DateTime.Now;
            //Run Time 
            long downTime = (long)points.ListMachineBlockData
                                    .Where(x => x.StartDateTime != null
                                            && (!((x.StartDateTime <= startShift
                                            && x.EndDateTime != null && x.EndDateTime <= endShift)
                                            || (x.StartDateTime >= endShift)))
                                            && x.MachineStatus == EnumMachineStatus.StopUnplanned)
                                    .Sum(m =>
                                    {
                                        DateTime endAt = m.EndDateTime ?? now;
                                        double duration = m.DurationInMiliSeconds.Value;

                                        if (endAt > endShift)
                                        {
                                            duration -= (endAt - endShift).Value.TotalSeconds;
                                        }

                                        return duration;
                                    });


            double totalCounter = (long)(messageData.GetValueByKey(configuration.TotalCountInTag.ToString()) ?? 0);
            double rejectCounter = (long)(messageData.GetValueByKey(configuration.TotalNGCountTag.ToString()) ?? 0);

            TimeSpan LengthShift = endShift.Value - startShift.Value;
            long PlannedProductionTime = (long)LengthShift.TotalSeconds - breakTime;

            _variables["OperationTime"] = (double)(PlannedProductionTime - downTime);
            _variables["PlannedProductionTime"] = (double)(LengthShift.TotalSeconds - breakTime);
            _variables["IdealOutput"] = points.IdealRunRate;
            _variables["GoodOutput"] = (double)(totalCounter - rejectCounter);
            _variables["TotalOutput"] = (double)(totalCounter);

        }

        //public void LoadConfiguration(OeePointEntity points,
        //                              List<MachineBlockDataEntity> MachineBlockDatas,
        //                              OeePointConfigEntity configuration,
        //                              Dictionary<string, object> messageData,
        //                              DateTime startShift,
        //                              DateTime endShift,
        //                              long breakTime,
        //                              long idealOutput)
        //{
        //    var data = messageData[configuration.AvailabilityFormulaDataTagId.ToString()];
        //    _availableFormula = (string)(messageData[configuration.AvailabilityFormulaDataTagId.ToString()] ?? "");
        //    _performanceFormula = (string)(messageData[configuration.PerformanceFormulaDataTagId.ToString()] ?? "");
        //    _qualityFormula = (string)(messageData[configuration.QualityFormulaDataTagId.ToString()] ?? "");
        //    DateTime now = DateTime.Now;

        //    //Run Time 
        //    long downTime = (long)MachineBlockDatas
        //        .Where(m => m.Status == EnumStatusMachine.Stop && m.Duration.HasValue &&
        //            m.StartAt.Value >= startShift && m.StartAt.Value <= endShift)
        //        .Sum(m =>
        //        {
        //            DateTime endAt = m.EndAt ?? now;
        //            double duration = m.Duration.Value;

        //            if (endAt > endShift)
        //            {
        //                duration -= (endAt - endShift).TotalSeconds;
        //            }

        //            return duration;
        //        });


        //    //double counterIn = (long)(messageData[configuration.TotalCounterTagId.ToString()] ?? 0);
        //    double totalCounter = (long)(messageData[configuration.TotalCounterTagId.ToString()] ?? 0);
        //    double rejectCounter = (long)(messageData[configuration.RejectCounterTagId.ToString()] ?? 0);

        //    TimeSpan LengthShift = startShift - endShift;
        //    long PlannedProductionTime = (long)LengthShift.TotalSeconds - breakTime;

        //    _variables["OperationTime"] = (double)(PlannedProductionTime - downTime);
        //    _variables["PlannedProductionTime"] = (double)(LengthShift.TotalSeconds - breakTime);
        //    _variables["IdealOutput"] = idealOutput;
        //    _variables["GoodOutput"] = (double)(totalCounter - rejectCounter);
        //    _variables["TotalOutput"] = (double)(totalCounter);

        //}
    }
}
