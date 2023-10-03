using PokemonPvpRanker.Domain.Entities;

namespace PokemonPvpRanker.Controllers.DTOs;

public class RankedPokemonDTO
{
    public string speciesId { get; set; } = null!;
    public string speciesName { get; set; } = null!;
    public int rating { get; set; }
    public double score { get; set; }

    internal static RankedPokemonDTO FromEntity(RankedPokemonEntity entity) =>
        new()
        {
            speciesId = entity.SpeciesId,
            speciesName = entity.SpeciesName,
            rating = entity.Rating,
            score = entity.Score
        };
}