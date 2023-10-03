using PokemonPvpRanker.Controllers.DTOs;

namespace PokemonPvpRanker.Domain.Entities;

public class RankedPokemonEntity
{
    public string SpeciesId { get; set; } = null!;
    public string SpeciesName { get; set; } = null!;
    public int Rating { get; set; }
    // public List<string> moveset { get; set; } = new();
    public double Score { get; set; }

    public static RankedPokemonEntity FromDTO(RankedPokemonDTO dto) =>
        new()
        {
            SpeciesId = dto.speciesId,
            SpeciesName = dto.speciesName,
            Rating = dto.rating,
            Score = dto.score
        };
}