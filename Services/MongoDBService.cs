using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoExample.Services;

public class MongoDBService {
    private readonly IMongoCollection<Embedded_movies> _embedded_moviesCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _embedded_moviesCollection = database.GetCollection<Embedded_movies>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Embedded_movies embedded_Movies){
        await _embedded_moviesCollection.InsertOneAsync(embedded_Movies);
        return;
    }

    public async Task<List<Embedded_movies>> GetAsync(){
        return await _embedded_moviesCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddToEmbedded_moviesAsync(string id, string movieId){
        FilterDefinition<Embedded_movies> filter = Builders<Embedded_movies>.Filter.Eq("Id", id);
        UpdateDefinition<Embedded_movies> update = Builders<Embedded_movies>.Update.AddToSet<string>("movieId", movieId);
        await _embedded_moviesCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id){
        FilterDefinition<Embedded_movies> filter = Builders<Embedded_movies>.Filter.Eq("Id",id);
        await _embedded_moviesCollection.DeleteOneAsync(filter);
        return;
    }
}