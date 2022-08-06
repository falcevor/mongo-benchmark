using System.Diagnostics;
using Falcevor.MongoBenchmark.Models;
using MongoDB.Driver;

namespace Falcevor.MongoBenchmark.Services;

public abstract class BenchmarkBase
{
    protected IMongoClient _client;
    protected IMongoDatabase _db;
    protected IMongoCollection<SimpleDocument> _collection;
    
    public BenchmarkBase()
    {
        _client = new MongoClient("mongodb://localhost");
        _db = _client.GetDatabase("Falcevor");
        _collection = _db.GetCollection<SimpleDocument>("BenchMark");
    }
    
    public async Task<TimeSpan> ExecuteAsync()
    {
        await PrepareAsync();
        
        var sw = new Stopwatch();
        
        var tasks = ExecuteInternalAsync();

        foreach (var task in tasks)
        {
            sw.Start();
            await task();
            sw.Stop();
        }
        
        return sw.Elapsed;
    }

    protected abstract IEnumerable<Func<Task>> ExecuteInternalAsync();
    protected abstract Task PrepareAsync();
}