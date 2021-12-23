namespace Kryptos.Web.Shared.Models;

public class CrackingResult
{
    public string Password { get; set; }
    public string TotalTime { get; set; }
    public string? ErrorMessage { get; set; }
    public int WordCount { get; set; }
}