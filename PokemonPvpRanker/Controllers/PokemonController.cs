using Microsoft.AspNetCore.Mvc;
using PokemonPvpRanker.Controllers.DTOs;
using PokemonPvpRanker.Domain.Services;

namespace PokemonPvpRanker.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/Pokemon")]
[ApiVersion("1.0")]
public class PokemonController : ControllerBase
{
    private readonly PokemonService _pokemonService;
    public PokemonController(PokemonService pokemonService) =>
        _pokemonService = pokemonService;

    [HttpPost("InitializeData")]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> Initialize([FromForm] FormFileDTO pokeGenieCsv, CancellationToken cancellationToken) =>
        Ok(await this._pokemonService.InitializeAsync(pokeGenieCsv, Response.RegisterForDisposeAsync, cancellationToken));

    [HttpGet("My")]
    [MapToApiVersion("1.0")]
    public IActionResult GetMy() =>
        Ok(this._pokemonService.GetMy());

    [HttpGet("Great")]
    [MapToApiVersion("1.0")]
    public IActionResult GetGreat() =>
        Ok(this._pokemonService.GetGreat());

    [HttpGet("Ultra")]
    [MapToApiVersion("1.0")]
    public IActionResult GetUltra() =>
        Ok(this._pokemonService.GetUltra());
}
