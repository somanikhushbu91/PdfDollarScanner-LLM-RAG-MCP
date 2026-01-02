using System.Security.Cryptography;
using System.Text;

namespace PdfDollarScanner;

public class LocalEmbeddingService
{
    private const int VectorSize = 128;

    public float[] GenerateEmbedding(string text)
    {
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(text));

        var vector = new float[VectorSize];

        for (int i = 0; i < VectorSize; i++)
        {
            vector[i] = hash[i % hash.Length] / 255f;
        }

        return vector;
    }
}
