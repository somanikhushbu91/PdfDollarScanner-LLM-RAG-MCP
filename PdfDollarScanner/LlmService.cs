namespace PdfDollarScanner;

public class LlmService
{
    public Task<LlmResult> ValidateDollarAmounts(string text,List<string> candidates)
    {
        return Task.FromResult(new LlmResult
        {
           HasRealAmount = true,
           Amounts = candidates.Select(c => new DollarAmount
           {
            Value = c,
            Context = "Detected financial reference"
           }).ToList()
        });
    }
}