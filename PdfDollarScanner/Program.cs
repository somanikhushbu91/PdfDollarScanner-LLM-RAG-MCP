using PdfDollarScanner;

Console.WriteLine("starting scheduled PDF scan...");

var scanner = new PdfScannerService(
    new McpClient(),
    new LlmService(),
    new RagIndexer(),
    new AlertService()
);

await scanner.ScanAsync("sample.pdf");

Console.WriteLine("Scan Complete");