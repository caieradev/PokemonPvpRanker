using PokemonPvpRanker.Domain.Entities;
using PokemonPvpRanker.Extensions;

namespace PokemonPvpRanker.Infrastructure;
public class PokemonDbContext : ISingleton
{
    public List<RankedPokemonEntity> GreatLeaguePvPoke { get; set; } = new List<RankedPokemonEntity>();
    public List<RankedPokemonEntity> UltraLeaguePvPoke { get; set; } = new List<RankedPokemonEntity>();
    public List<PokemonEntity> MyPokemons { get; set; } = new List<PokemonEntity>();
}