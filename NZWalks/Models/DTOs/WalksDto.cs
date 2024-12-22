namespace NZWalks.Models.DTOs
{
    public class WalksDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LenghtInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public string RegionName { get; set; }
        public string DifficultyName { get; set; }
        //public RegionDto Region { get; set; }
        //public DifficultyDto Difficulty { get; set; }
    }
}
