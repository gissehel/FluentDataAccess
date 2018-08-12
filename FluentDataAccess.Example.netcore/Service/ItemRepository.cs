using FluentDataAccess.Core.Service;
using FluentDataAccess.Example.netcore.Core.Service;
using FluentDataAccess.Example.netcore.DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace FluentDataAccess.Example.netcore.Service
{
    public class ItemRepository : IItemRepository
    {
        private IDataAccessService DataAccessService { get; set; }

        public ItemRepository(IDataAccessService dataAccessService) => DataAccessService = dataAccessService;

        public void Init() =>
            DataAccessService
                .GetQuery("create table if not exists item (id integer primary key, name text, a integer, b integer, constraint name_unique unique (name));")
                .Execute();

        public void AddItem(string name, int a, int b) =>
            DataAccessService
                .GetQuery("insert or replace into item (name, a, b) values (@name, @a, @b)")
                .WithParameter("name", name)
                .WithParameter("a", a)
                .WithParameter("b", b)
                .Execute();

        public void AddItem(Item item) => AddItem(item.Name, item.IntA, item.IntB);

        public Item ReadItem(string name) =>
            BindResultToItem
                (
                    DataAccessService
                        .GetQuery("select " + ItemSelectString + " from item where name=@name")
                        .WithParameter("name", name)
                        .Returning<Item>()
                )
                .Execute()
                .FirstOrDefault()
            ;

        public IEnumerable<Item> ReadItems() =>
            BindResultToItem
                (
                    DataAccessService
                        .GetQuery("select " + ItemSelectString + " from item order by name")
                        .Returning<Item>()
                )
                .Execute()
            ;

        private string ItemSelectString => "name, a, b";

        private IDataAccessQueryWithResult<Item> BindResultToItem(IDataAccessQueryWithResult<Item> dataAccessQueryWithResult) =>
            dataAccessQueryWithResult
                .Reading("name", (Item item, string data) => item.Name = data)
                .Reading("a", (Item item, int a) => item.IntA = a)
                .Reading("b", (Item item, int b) => item.IntB = b)
            ;
    }
}