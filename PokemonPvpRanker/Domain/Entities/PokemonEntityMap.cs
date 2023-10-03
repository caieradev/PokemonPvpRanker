using CsvHelper.Configuration;

namespace PokemonPvpRanker.Domain.Entities;

public class PokemonEntityMap : ClassMap<PokemonEntity>
{
    public PokemonEntityMap()
    {
        Map(m => m.Name).Index(1).Name("Name");
        Map(m => m.Form).Index(2).Name("Form");
        Map(m => m.CombatPower).Index(6).Name("CP");
        Map(m => m.HealthPower).Index(7).Name("HP");
        Map(m => m.AttackIV).Index(8).Name("Atk IV");
        Map(m => m.DefenseIV).Index(9).Name("Def IV");
        Map(m => m.StaminaIV).Index(10).Name("Sta IV");
        Map(m => m.IVAverage).Index(11).Name("IV Avg");
        Map(m => m.isShadow).Index(23).Name("Shadow/Purified");
        Map(m => m.RankPercentageGLString).Index(26).Name("Rank % (G)");
        Map(m => m.NameGL).Index(31).Name("Name (G)");
        Map(m => m.FormGL).Index(32).Name("Form (G)");
        Map(m => m.ShadowGLInt).Index(33).Name("Sha/Pur (G)");
        Map(m => m.RankPercentageULString).Index(34).Name("Rank % (U)");
        Map(m => m.NameUL).Index(39).Name("Name (U)");
        Map(m => m.FormUL).Index(40).Name("Form (U)");
        Map(m => m.ShadowULInt).Index(41).Name("Sha/Pur (U)");
    }
}