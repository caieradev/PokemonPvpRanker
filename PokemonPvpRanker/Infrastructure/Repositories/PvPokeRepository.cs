using PokemonPvpRanker.Domain.Entities;
using PokemonPvpRanker.Extensions;


namespace PokemonPvpRanker.Infrastructure.Repositories;
public class PvPokeRepository : ITransient
{
    private readonly PokemonDbContext _db;
    public PvPokeRepository(PokemonDbContext db) =>
        _db = db;

    public void InsertGreatLeaguePokemons(IEnumerable<RankedPokemonEntity> greatLeaguePokemons) =>
        this._db.GreatLeaguePvPoke.AddRange(greatLeaguePokemons);

    public void InsertUltraLeaguePokemons(IEnumerable<RankedPokemonEntity> ultraLeaguePokemons) =>
        this._db.UltraLeaguePvPoke.AddRange(ultraLeaguePokemons);

    public List<RankedPokemonEntity> GetGreatLeaguePokemons() =>
        this._db.GreatLeaguePvPoke;

    public List<RankedPokemonEntity> GetUltraLeaguePokemons() =>
        this._db.UltraLeaguePvPoke;
}