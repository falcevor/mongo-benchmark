using Falcevor.MongoBenchmark.Models;

namespace Falcevor.MongoBenchmark.Services;

public class WriteBenchmark : BenchmarkBase
{
    public long TotalCount { get; set; }
    public long BatchSize { get; set; }

    protected override async Task PrepareAsync()
    {
        await _db.DropCollectionAsync("BenchMark");
    }
    
    protected override IEnumerable<Func<Task>> ExecuteInternalAsync()
    {
        long batchIndex = 0;
        var batchArray = new SimpleDocument[BatchSize];
        
        for (long i = 0; i < TotalCount; ++i)
        {
            var document = new SimpleDocument(i, $"Name{i}", $"Description{i}", $"Parameter1{i}", $"Parameter2{i}", $"Parameter3{i}");
            batchArray[batchIndex] = document;
            batchIndex++;
            
            if (batchIndex == BatchSize)
            {
                batchIndex = 0;
                yield return () => _collection.InsertManyAsync(batchArray);
            }
        }
        
        if (batchIndex > 0)
            yield return () => _collection.InsertManyAsync(batchArray.Take((int)batchIndex));
    }


}