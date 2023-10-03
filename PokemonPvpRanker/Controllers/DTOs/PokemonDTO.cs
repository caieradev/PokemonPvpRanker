using PokemonPvpRanker.Domain.Entities;

namespace PokemonPvpRanker.Controllers.DTOs;
public class PokemonDTO
{
    public string? Name { get; set; }
    public string? Form { get; set; }
    public int CombatPower { get; set; }
    public int HealthPower { get; set; }
    public int AttackIV { get; set; }
    public int DefenseIV { get; set; }
    public int StaminaIV { get; set; }
    public double IVAverage { get; set; }
    public bool isShadow { get; set; }
    public string? RankPercentageGL { get; set; }
    public string? NameGL { get; set; }
    public string? FormGL { get; set; }
    public bool ShadowGL { get; set; }
    public string? RankPercentageUL { get; set; }
    public string? NameUL { get; set; }
    public string? FormUL { get; set; }
    public bool ShadowUL { get; set; }

    public static PokemonDTO FromEntity(PokemonEntity entity) =>
        new()
        {
            Name = entity.Name,
            Form = entity.Form,
            CombatPower = entity.CombatPower,
            HealthPower = entity.HealthPower,
            AttackIV = entity.AttackIV,
            DefenseIV = entity.DefenseIV,
            StaminaIV = entity.StaminaIV,
            IVAverage = entity.IVAverage,
            isShadow = entity.isShadow,
            RankPercentageGL = entity.RankPercentageGLString,
            NameGL = entity.NameGL,
            FormGL = entity.FormGL,
            ShadowGL = entity.ShadowGL,
            RankPercentageUL = entity.RankPercentageULString,
            NameUL = entity.NameUL,
            FormUL = entity.FormUL,
            ShadowUL = entity.ShadowUL,
        };
}