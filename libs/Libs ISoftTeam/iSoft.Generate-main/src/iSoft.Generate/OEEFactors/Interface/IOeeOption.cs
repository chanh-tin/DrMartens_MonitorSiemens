using System.Collections.Generic;

namespace SourceBaseBE.MainService.OEEFactors.Interface
{
    public interface IOeeOption
    {
        Dictionary<string, object> GetVariables();
        string AvailableFormula { get; }
        string PerformanceFormula { get; }
        string QualityFormula { get; }
        string OeeFormula { get; }
    }
}
