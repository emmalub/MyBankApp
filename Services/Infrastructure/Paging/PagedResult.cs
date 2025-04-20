namespace Services.Infrastructure.Paging
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public List<T> Results { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        //public IList<T> Results { get; set; }
        //public PagedResult()
        //{
        //    Results = new List<T>();
        //}
    }
}
