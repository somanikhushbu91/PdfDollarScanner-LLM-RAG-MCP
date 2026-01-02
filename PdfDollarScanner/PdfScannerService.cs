namespace PdfDollarScanner;

public class PdfScannerService
{
    private readonly McpClient _mcp;
    private readonly LlmService _llm;
    private readonly RagIndexer _rag;
    private readonly AlertService _alert;

    public PdfScannerService(
        McpClient mcp,
        LlmService llm,
        RagIndexer rag,
        AlertService alert)
    {
        _mcp = mcp;
        _llm = llm;
        _rag = rag;
        _alert = alert;
    }

    public async Task ScanAsync(string pdfPath)
    {
        var text = _mcp.ExtractText(pdfPath);

        var candidates = _mcp.FindDollarCandidates(text);
        if (!candidates.Any())
        {
            Console.WriteLine("No dollar patterns found.");
        }

        var result = await _llm.ValidateDollarAmounts(text, candidates);

        if (!result.HasRealAmount)
        {
            Console.WriteLine("LLM rejected flase positive");
            return;
        }

        _rag.Index(text);
        _alert.Send(result);
    }
}