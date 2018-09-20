using FluentDataAccess.Example.net35.DomainModel;

namespace FluentDataAccess.Example.net35.Service
{
    public static class DataAccessHelper
    {
        public static IDataAccessQueryWithResult<Item> ReadingItemProperties(this IDataAccessQueryWithResult<Item> dataAccessQueryWithResult) =>
            dataAccessQueryWithResult
                .Reading("name", (Item item, string data) => item.Name = data)
                .Reading("a", (Item item, int a) => item.IntA = a)
                .Reading("b", (Item item, int b) => item.IntB = b)
            ;
    }
}