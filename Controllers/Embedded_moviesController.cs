using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;

namespace MongoExample.Controllers;

[Controller]
[Route("api/[controller]")]
public class Embedded_moviesController: Controller{

    private readonly MongoDBService _mongoDBService;
    public Embedded_moviesController(MongoDBService mongoDBService){
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Embedded_movies>> Get(){
        return await _mongoDBService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Embedded_movies embedded_Movies){
        await _mongoDBService.CreateAsync(embedded_Movies);
        return CreatedAtAction(nameof(Get), new{ id = embedded_Movies.Id }, embedded_Movies);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToEmbedded_movies(string id, [FromBody] string movieId){
        await _mongoDBService.AddToEmbedded_moviesAsync(id, movieId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id){
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }
}

