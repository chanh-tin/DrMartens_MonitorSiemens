using iMag.Oee.OEEFactors;
using iSoft.ConnectionCommon.MessageBroker;
using iSoft.Common.Payloads;
using SourceBaseBE.Database.Entities;
using System;
using System.Collections.Generic;

namespace SourceBaseBE.MainService.OEEFactors.Interface
{
    public interface IOeeCalculator
    {
        IOeeCalculator AddVariables(Dictionary<string, object> variables);
        IOeeCalculator UseAvailableFormula(string formula);
        IOeeCalculator UsePerformanceFormula(string formula);
        IOeeCalculator UseQualityFormula(string formula);
        IOeeCalculator UseOeeFormula(string formula);
        IOeeFactor Calculate();
        IOeeFactor CalculateOeePointsRealtime(OeePointEntity points,
                                             OeePointConfigEntity Configuration,
                                             DevicePayloadMessage messageData,
                                             DateTime? startTime,
                                             DateTime? endTime,
                                             long breakTime);


        public void LoadConfigurationRealtime(OeePointEntity points,
                                              OeePointConfigEntity configuration,
                                              DevicePayloadMessage messageData,
                                              DateTime? startTime,
                                              DateTime? endTime,
                                              long breakTime);
            }
}
