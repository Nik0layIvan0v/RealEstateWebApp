namespace RealEstate.Models
{
    public class EstateFeature
    {
        public string EstateId { get; set; }

        public Estate Estate { get; set; }

        public string FeatureId { get; set; }

        public Feature Feature { get; set; }
    }
}
