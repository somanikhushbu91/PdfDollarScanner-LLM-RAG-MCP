namespace PdfDollarScanner;

public class RagIndexer
{
    private readonly InMemoryVectorStore _vectorStore;
    private readonly LocalEmbeddingService _embedding = new();

    public RagIndexer(InMemoryVectorStore vectorStore)
    {
        _vectorStore = vectorStore;
    }

    public void Index(string documentText, string source)
    {
        foreach (var charChunk in documentText.Chunk(500))
        {
            var chunk = new string(charChunk);

            _vectorStore.Add(new VectorRecord
            {
                Id = Guid.NewGuid().ToString(),
                Content = chunk,
                Embedding = _embedding.GenerateEmbedding(chunk),
                Source = source
            });
        }

        Console.WriteLine("Indexed into local vector store.");
    }
}
