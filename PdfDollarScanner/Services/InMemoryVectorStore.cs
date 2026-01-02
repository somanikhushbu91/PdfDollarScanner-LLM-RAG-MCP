using System.Collections.Concurrent;

namespace PdfDollarScanner;

public class InMemoryVectorStore
{
    private readonly ConcurrentBag<VectorRecord> _store = new();

    public void Add(VectorRecord record)
    {
        _store.Add(record);
    }

    public IEnumerable<VectorRecord> Search(float[] queryVector, int topK = 3)
    {
        return _store
            .Select(r => new
            {
                Record = r,
                Score = CosineSimilarity(queryVector, r.Embedding)
            })
            .OrderByDescending(x => x.Score)
            .Take(topK)
            .Select(x => x.Record);
    }

    private static double CosineSimilarity(float[] a, float[] b)
    {
        double dot = 0, magA = 0, magB = 0;

        for (int i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            magA += a[i] * a[i];
            magB += b[i] * b[i];
        }

        return dot / (Math.Sqrt(magA) * Math.Sqrt(magB));
    }
}
