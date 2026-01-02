namespace PdfDollarScanner;

public class LlmResult
{
    public bool HasRealAmount { get; set;}
    public List<DollarAmount> Amounts {get; set; } = [];
}

public class DollarAmount
{
    public string Value { get; set; }
    public string Context {get; set; }
}