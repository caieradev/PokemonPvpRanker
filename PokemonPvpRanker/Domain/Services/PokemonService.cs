using System.Text.Json;
using System.Transactions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PokemonPvpRanker.Configs;
using PokemonPvpRanker.Controllers.DTOs;
using PokemonPvpRanker.Domain.Entities;
using PokemonPvpRanker.Extensions;
using PokemonPvpRanker.Infrastructure.Repositories;


namespace PokemonPvpRanker.Domain.Services;

public class PokemonService : IScoped
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly PvPokeRepository _pvPokeRepository;
    private readonly MyPokemonsRepository _myPokemonsRepository;
    public PokemonService(PvPokeRepository pvPokeRepository, IHttpClientFactory httpClientFactory, MyPokemonsRepository myPokemonsRepository) =>
        (_httpClientFactory, _pvPokeRepository, _myPokemonsRepository) = (httpClientFactory, pvPokeRepository, myPokemonsRepository);

    public async Task<IActionResult> InitializeAsync(FormFileDTO pokeGenieCsvFile, Action<IAsyncDisposable> registerForDisposeAsync, CancellationToken cancellationToken)
    {
        if (pokeGenieCsvFile?.file == null)
            throw new ArgumentNullException(nameof(pokeGenieCsvFile));

        if (!pokeGenieCsvFile.file.ContentType.Contains("csv"))
            throw new FormatException("O arquivo deve ser do tipo CSV.");

        var greatLeaguePokemons = await this.GetPokemonsFromPvPokeAsync(PvPokeConfig.GreatLeagueJson, "GreatLeague", cancellationToken);
        var ultraLeaguePokemons = await this.GetPokemonsFromPvPokeAsync(PvPokeConfig.UltraLeagueJson, "UltraLeague", cancellationToken);

        var myPokemons = await this.GetMyPokemonsFromPokeGenieAsync(pokeGenieCsvFile, registerForDisposeAsync, cancellationToken);

        using (TransactionScope ts = new(TransactionScopeAsyncFlowOption.Enabled))
        {
            this.LoadPvPokeData(greatLeaguePokemons, ultraLeaguePokemons);
            this.LoadMyPokemons(myPokemons);

            ts.Complete();
        }

        var greatLoaded = this._pvPokeRepository.GetGreatLeaguePokemons().Any();
        var ultraLoaded = this._pvPokeRepository.GetUltraLeaguePokemons().Any();
        var myPokemonsLoaded = this._myPokemonsRepository.GetMyPokemons().Any();
        string message;

        switch (greatLoaded, ultraLoaded, myPokemonsLoaded)
        {
            case (true, true, true):
                message = "Data loaded successfully.";
                break;
            case (false, _, _):
                message = "Failed to load Great League data.";
                break;
            case (_, false, _):
                message = "Failed to load Ultra League data.";
                break;
            default:
                message = "Failed to load My Pokemons data.";
                break;
        }

        return new BadRequestObjectResult(message);
    }

    private void LoadMyPokemons(IEnumerable<PokemonEntity> myPokemons) =>
        this._myPokemonsRepository.LoadMyPokemons(myPokemons);

    private async Task<IEnumerable<PokemonEntity>> GetMyPokemonsFromPokeGenieAsync(FormFileDTO pokeGenieCsvFile, Action<IAsyncDisposable> registerForDisposeAsync, CancellationToken cancellationToken)
    {
        var memoryStream = new MemoryStream();
        registerForDisposeAsync.Invoke(memoryStream);

        pokeGenieCsvFile.file.CopyTo(memoryStream);
        memoryStream.Position = 0;

        return await PokemonEntity.ParseCsvFile(memoryStream, cancellationToken);
    }

    private void LoadPvPokeData(IEnumerable<RankedPokemonEntity> greatLeaguePokemons, IEnumerable<RankedPokemonEntity> ultraLeaguePokemons)
    {
        this._pvPokeRepository.InsertGreatLeaguePokemons(greatLeaguePokemons);
        this._pvPokeRepository.InsertUltraLeaguePokemons(ultraLeaguePokemons);
    }

    private async Task<IEnumerable<RankedPokemonEntity>> GetPokemonsFromPvPokeAsync(string leagueJson, string clientIdentifier, CancellationToken cancellationToken)
    {
        using (var client = _httpClientFactory.CreateClient(clientIdentifier))
        {
            HttpResponseMessage response = await client.GetAsync(leagueJson, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync(cancellationToken);

                IEnumerable<RankedPokemonEntity> objetos = Deserializer<IEnumerable<RankedPokemonDTO>>(json)
                    .Select(RankedPokemonEntity.FromDTO);

                return objetos
                    .Take(100);
            }
            else
                throw new Exception("Falha ao recuperar o JSON.");

        }
    }

    private static T Deserializer<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json) ??
            throw new Exception("Falha ao deserializar o JSON.");
    }

    public IEnumerable<PokemonDTO> GetMy() =>
        this._myPokemonsRepository.GetMyPokemons()
            .Select(PokemonDTO.FromEntity);

    public IEnumerable<RankedPokemonDTO> GetGreat() =>
        this._pvPokeRepository.GetGreatLeaguePokemons()
            .OrderByDescending(p => p.Score)
            .Select(RankedPokemonDTO.FromEntity);

    public IEnumerable<RankedPokemonDTO> GetUltra() =>
        this._pvPokeRepository.GetUltraLeaguePokemons()
            .OrderByDescending(p => p.Score)
            .Select(RankedPokemonDTO.FromEntity);
}