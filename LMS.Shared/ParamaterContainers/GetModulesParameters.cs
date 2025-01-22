namespace LMS.Shared.ParamaterContainers
{
    public class GetModulesParameters
    {
        public bool TrackChanges { get; set; } = false;
        public bool IncludeActivities { get; set; } = false;
        public bool IncludeDocuments { get; set; } = false;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "StartDate";
        public string SortOrder { get; set; } = "asc";
        public string? SearchTerm { get; set; }
    }
}
