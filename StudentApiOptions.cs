namespace Lab9.Api.Students.Options
{
    public class StudentApiOptions
    {
        public int MaxCountStudents { get; init; }
        public bool AllowAdd { get; init; }
        public bool AllowDelete { get; init; }
        public bool AllowUpdate { get; init; }
        public string Code { get; init; } = string.Empty;
    }
}
