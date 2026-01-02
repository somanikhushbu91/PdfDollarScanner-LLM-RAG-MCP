using System.Threading.Channels;

namespace PdfDollarScanner.Queue;

public class LocalQueue
{
    private readonly Channel<string> _channel;
    
    public LocalQueue()
    {
        _channel = Channel.CreateUnbounded<string>(new UnboundedChannelOptions
        {
            SingleWriter = false,
            SingleReader = false
        });    
    } 

    public async Task EnqueueAsync(string message)
    {
        await _channel.Writer.WriteAsync(message);
    }

    public IAsyncEnumerable<string> DequeueAsync(CancellationToken cancellationToken = default)
    {
        return _channel.Reader.ReadAllAsync(cancellationToken);
    }

    public void Complete()
    {
        _channel.Writer.Complete();
    }
}