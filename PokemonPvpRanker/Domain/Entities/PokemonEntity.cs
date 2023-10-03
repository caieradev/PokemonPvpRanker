using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using PokemonPvpRanker.Extensions;

namespace PokemonPvpRanker.Domain.Entities;

public class PokemonEntity
{
    public string Name { get; set; } = null!;
    public string Form { get; set; } = null!;
    public int CombatPower { get; set; }
    public int HealthPower { get; set; }
    public int AttackIV { get; set; }
    public int DefenseIV { get; set; }
    public int StaminaIV { get; set; }
    public double IVAverage { get; set; }
    public bool isShadow { get; set; }
    public double RankPercentageGL { get; set; }
    internal string RankPercentageGLString
    {
        get => this.RankPercentageGL.ToString();
        set => this.RankPercentageGL = Convert.ToDouble(value.Replace("%", ""));
    }
    public string NameGL { get; set; } = null!;
    public string FormGL { get; set; } = null!;
    public bool ShadowGL { get; set; }
    internal int ShadowGLInt
    {
        get => this.ShadowGL ? 1 : 0;
        set => this.ShadowGL = value == 1;
    }
    public double RankPercentageUL { get; set; }
    internal string RankPercentageULString
    {
        get => this.RankPercentageUL.ToString();
        set => this.RankPercentageUL = Convert.ToDouble(value.Replace("%", ""));
    }
    public string NameUL { get; set; } = null!;
    public string FormUL { get; set; } = null!;
    public bool ShadowUL { get; set; }
    internal int ShadowULInt
    {
        get => this.ShadowUL ? 1 : 0;
        set => this.ShadowUL = value == 1;
    }

    public static async Task<IEnumerable<PokemonEntity>> ParseCsvFile(MemoryStream csvFile, CancellationToken cancellationToken)
    {
        if (csvFile == null || csvFile.Length <= 0)
            throw new InvalidOperationException("Não foi possível ler os registros do arquivo CSV.");

        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ","
        };

        using (var reader = new StreamReader(csvFile))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            csv.Context.RegisterClassMap<PokemonEntityMap>();

            var records = await csv.GetRecordsAsync<PokemonEntity>()
                .GetAsyncEnumerator(cancellationToken)
                .ToListAsync() ??
                    throw new InvalidOperationException("Não foi possível ler os registros do arquivo CSV.");

            return records;
        }
    }
}