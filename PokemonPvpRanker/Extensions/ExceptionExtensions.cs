namespace PokemonPvpRanker.Extensions;

public static class ExceptionExtensions
{
    public static string GetExceptionMessage(this Exception ex) =>
        ex.InnerException == null ? ex.Message : ex.GetExceptionMessage();
}