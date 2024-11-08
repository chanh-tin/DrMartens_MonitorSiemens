using SourceBaseBE.MainService.OEEFactors.Interface;
using System.Collections.Generic;

namespace SourceBaseBE.MainService.OEEFactors
{
    public class OeeOption : IOeeOption
    {
        public OeeOption(Dictionary<string, object> variables, string availableFormula, string performanceFormula, string qualityFormula, string oeeFormula = "default")
        {
            _Variables = variables;
            AvailableFormula = availableFormula;
            PerformanceFormula = performanceFormula;
            QualityFormula = qualityFormula;
            OeeFormula = oeeFormula;
        }

        private Dictionary<string, object> _Variables { get; set; }

        public Dictionary<string, object> GetVariables()
        {
            return _Variables;
        }

        public string AvailableFormula { get; private set; }
        public string PerformanceFormula { get; private set; }
        public string QualityFormula { get; private set; }
        public string OeeFormula { get; private set; } = "default";
    }
}
