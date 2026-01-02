using UglyToad.PdfPig;

namespace PdfDollarScanner;

public class McpClient
{
    public string ExtractText(string pdfPath)
    {
        using var doc = PdfDocument.Open(pdfPath);
        return string.Join("\n",doc.GetPages().Select(p => p.Text));
    }

    public List<string> FindDollarCandidates(string text)
    {
        return System.Text.RegularExpressions.Regex.Matches(text, @"\$\d+(,\d{3})*(\.\d{2})?").Select(m => m.Value).Distinct().ToList();
    }
}