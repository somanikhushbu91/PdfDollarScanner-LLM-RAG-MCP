namespace PdfDollarScanner;

public class VectorRecord
{
    public required string Id { get; set; }
    public required string Content { get; set; }
    public required float[] Embedding { get; set; }
    public required string Source { get; set; }
}
