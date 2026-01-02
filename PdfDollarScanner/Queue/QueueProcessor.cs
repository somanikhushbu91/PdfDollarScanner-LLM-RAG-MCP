using PdfDollarScanner.Services;

namespace PdfDollarScanner.Queue;

public class QueueProcessor
{
    private readonly LocalQueue _queue;
    private readonly PdfScannerService _scanner;

    public QueueProcessor(LocalQueue queue, PdfScannerService scanner)
    {
        _queue = queue;
        _scanner = scanner;
    }

    public async Task StartAsync(int workerCount, CancellationToken cancellationToken)
    {
        var workers = Enumerable.Range(0,workerCount).Select(id => Task.Run(async() =>
        {
           await foreach (var message in _queue.DequeueAsync(cancellationToken))
                {
                    Console.WriteLine($" Worker {id} processing {message}");
                    await _scanner.ScanAsync(message);
                }
            }, cancellationToken));

        await Task.WhenAll(workers);
    }
}