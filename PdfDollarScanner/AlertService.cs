namespace PdfDollarScanner;

public class AlertService
{
    public void Send(LlmResult result)
    {
        Console.WriteLine("ALERT: Dollar Amount Detected");
        foreach (var amt in result.Amounts)
        {
            Console.WriteLine($" - {amt.Value}: {amt.Context}");
        }
    }
}