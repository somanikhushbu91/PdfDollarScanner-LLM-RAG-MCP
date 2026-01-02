namespace PdfDollarScanner.Services;

public class LlmService
{
    public async Task<double> EvaluateConfidenceAsync(
        string documentText,
        List<string> dollarAmounts)
    {
        return await Task.Run(() =>
        {
            if (dollarAmounts.Count == 0)
                return 0.0;

            // Semantic heuristics (mocked LLM reasoning)
            var textLower = documentText.ToLower();

            double confidence = 0.7;

            if (dollarAmounts.Any(d => d.Contains(",")))
            {
                confidence += 0.20; // large financial values
            }

            return Math.Min(confidence, 0.95);
        });
    }
}
