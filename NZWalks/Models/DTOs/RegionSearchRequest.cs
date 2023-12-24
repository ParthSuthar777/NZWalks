namespace NZWalks.Models.DTOs
{
    public class RegionSearchRequest
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 

    }
}
