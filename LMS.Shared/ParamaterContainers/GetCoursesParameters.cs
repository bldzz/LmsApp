namespace LMS.Shared.ParamaterContainers
{
    public class GetCoursesParameters
    {
        public bool TrackChanges { get; set; } = false;
        public bool IncludeModules { get; set; } = false;
        public bool IncludeDocuments { get; set; } = false;
        public bool CascadeIncludeActivities { get; set; } = false;
        public bool CascadeIncludeModuleDocs { get; set; } = false;
        public bool CascadeIncludeActivityDocs { get; set; } = false;
        public bool IncludeUsers { get; set; } = false;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "StartDate";
        public string SortOrder { get; set; } = "asc";
        public string? SearchTerm { get; set; }
    }
}
