namespace SourceBaseBE.MainService.OEEFactors.Interface
{
    public interface IOeeFactor
    {
        public float Available { get; set; }
        public float Performance { get; set; }
        public float Quality { get; set; }
        public float OEE { get; set; }
        public float OeeCustomValue { get; set; }
    }
}
