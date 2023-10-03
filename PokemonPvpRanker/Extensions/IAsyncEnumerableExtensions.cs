namespace PokemonPvpRanker.Extensions;
public static class IAsyncEnumerableExtensions
{
    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerator<T> asyncEnumerator)
    {
        var resultList = new List<T>();
        try
        {
            while (await asyncEnumerator.MoveNextAsync())
            {
                resultList.Add(asyncEnumerator.Current);
            }
        }
        finally
        {
            await asyncEnumerator.DisposeAsync();
        }

        return resultList;
    }
}