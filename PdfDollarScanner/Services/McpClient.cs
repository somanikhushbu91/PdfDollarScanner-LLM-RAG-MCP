using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using System.Text;

namespace PdfDollarScanner.Services;

public class McpClient
{
    public async Task<string> ExtractTextAsync(string pdfPath)
    {
        return await Task.Run(() =>
        {
            var sb = new StringBuilder();

            using var document = PdfDocument.Open(pdfPath);

            foreach (Page page in document.GetPages())
            {
                sb.AppendLine(page.Text);
            }

            return sb.ToString();
        });
    }
}
