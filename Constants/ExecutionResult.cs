namespace ExamForms.Constants;

public class ExecutionResult
{
    public ExecutionResult()
    {
        this.Message = new List<string>();
    }

    public bool IsSuccess { get; set; }
    public bool IsCreate { get; set; }
    public bool IsEdit { get; set; }
    public bool IsExists { get; set; }
    public object? DataItem { get; set; }
    public bool HasError { get; set; }
    public bool IsNoRecordFound { get; set; }
    public int UpdatedRowCount { get; set; }
    public int Count { get; set; }
    public string? Url { get; set; }
    public Exception? Exception { get; set; }
    public List<string>? Message { get; set; }
}
