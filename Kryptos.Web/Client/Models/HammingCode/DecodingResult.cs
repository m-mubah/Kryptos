namespace Kryptos.Web.Client.Models.HammingCode;

public record DecodingResult
{
    public List<int> OriginalSequence { get; set; }
    public List<int> FixedSequence { get; set; }
    public List<int> DecodedSequence { get; set; }
    public int ErrorPosition { get; set; }
}