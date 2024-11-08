using SourceBaseBE.MainService.OEEFactors.Interface;

namespace SourceBaseBE.MainService.OEEFactors
{
    public class OeeFactor : IOeeFactor
    {
        public float Available { get; set; }
        public float Performance { get; set; }
        public float Quality { get; set; }
        public float OEE { get; set; }
        public float OeeCustomValue { get; set; }


        public OeeFactor()
        {
            Available = 0;
            Performance = 0;
            Quality = 0;
            OEE = 0;
            OeeCustomValue = 0;
        }
    }
}
