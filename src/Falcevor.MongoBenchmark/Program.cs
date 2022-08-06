using Falcevor.MongoBenchmark.Services;

var runCount = 10;
long totalCount = 1_000_000;
var batches = new long[] {1000, 10_000, 50_000, 100_000, 300_000, 500_000, 800_000, 1_000_000};

foreach (long batchSize in batches)
{
    var totalElapsed = TimeSpan.Zero;
    for (int i = 0; i < runCount; ++i)
    {
        var benchmark = new WriteBenchmark()
        {
            TotalCount = totalCount,
            BatchSize = batchSize
        };

        totalElapsed += await benchmark.ExecuteAsync();
    }

    var avgElapsed = totalElapsed / runCount;
    Console.WriteLine(batchSize + ": " + avgElapsed.TotalSeconds);
} 

