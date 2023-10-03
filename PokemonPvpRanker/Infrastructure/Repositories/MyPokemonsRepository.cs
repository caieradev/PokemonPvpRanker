using PokemonPvpRanker.Domain.Entities;
using PokemonPvpRanker.Extensions;


namespace PokemonPvpRanker.Infrastructure.Repositories;
public class MyPokemonsRepository : ITransient
{
    private readonly PokemonDbContext _db;
    public MyPokemonsRepository(PokemonDbContext db) =>
        _db = db;

    public void LoadMyPokemons(IEnumerable<PokemonEntity> myPokemons) =>
        this._db.MyPokemons.AddRange(myPokemons);

    public List<PokemonEntity> GetMyPokemons() =>
        this._db.MyPokemons;
}