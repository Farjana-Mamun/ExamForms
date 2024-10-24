using System.Web.Mvc.Ajax;

namespace ExamForms.Utility;

public class DeleteModalOptions
{
    public string? Modal { get; set; }
    public string? Key { get; set; }
    public string? KeyValue { get; set; }
    public string? AreaName { get; set; }
    public string? Title { get; set; }
    public object? Message { get; set; }
    public string? LinkText { get; set; }
    public string? ActionName { get; set; }
    public string? ControllerName { get; set; }
    public string? Protocol { get; set; }
    public string? HostName { get; set; }
    public string? Fragment { get; set; }
    public string? RouteValues { get; set; }
    public AjaxOptions? AjaxOptions { get; set; }
    public object? HtmlAttributes { get; set; }
    public string? OnComplete { get; set; }
}
