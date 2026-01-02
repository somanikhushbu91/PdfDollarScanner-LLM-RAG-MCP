using System.Formats.Asn1;
using PdfDollarScanner;
using PdfDollarScanner.Queue;
using PdfDollarScanner.Services;

var vectorStore = new InMemoryVectorStore();
var ragIndexer = new RagIndexer(vectorStore);

var scanner = new PdfScannerService(
    new McpClient(),
    new LlmService(),
   ragIndexer,
    new AlertService()
);

var queue = new LocalQueue();
var processor = new QueueProcessor(queue, scanner);

await queue.EnqueueAsync("sample.pdf");
await queue.EnqueueAsync("sample2.pdf");

queue.Complete();

using var cts = new CancellationTokenSource();
await processor.StartAsync(workerCount: 2, cts.Token);
