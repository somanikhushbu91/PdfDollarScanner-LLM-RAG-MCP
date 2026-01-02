using System.Text.RegularExpressions;

namespace PdfDollarScanner.Services;

public class PdfScannerService
{
    private readonly McpClient _mcpClient;
    private readonly LlmService _llmService;
    private readonly RagIndexer _ragIndexer;
    private readonly AlertService _alertService;

    private static readonly Regex DollarRegex =
        new(@"\$\s?\d{1,3}(,\d{3})*(\.\d{2})?",
            RegexOptions.Compiled);

    public PdfScannerService(
        McpClient mcpClient,
        LlmService llmService,
        RagIndexer ragIndexer,
        AlertService alertService)
    {
        _mcpClient = mcpClient;
        _llmService = llmService;
        _ragIndexer = ragIndexer;
        _alertService = alertService;
    }

    public async Task ScanAsync(string pdfPath)
    {
        Console.WriteLine($" Scanning {pdfPath}");

        // Extract real PDF text
        var text = await _mcpClient.ExtractTextAsync(pdfPath);

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine(" No text extracted.");
            return;
        }

        // Index into RAG
        _ragIndexer.Index(text, pdfPath);

        // find dollar amounts
        var matches = DollarRegex.Matches(text);

        if (matches.Count == 0)
        {
            Console.WriteLine("No dollar amounts found.");
            return;
        }

        var amounts = matches.Select(m => m.Value).ToList();
        Console.WriteLine($" Found {amounts.Count} dollar values.");

        // LLM semantic confidence
        var confidence = await _llmService.EvaluateConfidenceAsync(text, amounts);

        // Alert
        if (confidence >= 0.7)
        {
            _alertService.SendAlert(pdfPath, matches, confidence);
        }
    }
}
