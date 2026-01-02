using System.Text.RegularExpressions;
namespace PdfDollarScanner;

public class AlertService
{
    public void SendAlert(
        string source,
        MatchCollection matches,
        double confidence)
    {
        Console.WriteLine(
            $" ALERT: {source} | Confidence: {confidence:P}");
    }
}