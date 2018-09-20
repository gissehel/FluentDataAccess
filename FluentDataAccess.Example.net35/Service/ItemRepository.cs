using FluentDataAccess.Example.net35.Core.Service;
using FluentDataAccess.Example.net35.DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace FluentDataAccess.Example.net35.Service
{
    public class ItemRepository : IItemRepository
    {
        private IDataAccessService DataAccessService { get; set; }

        public ItemRepository(IDataAccessService dataAccessService) => DataAccessService = dataAccessService;

        public void Init() =>
            DataAccessService
                .GetQuery("create table item (id integer IDENTITY(1,1) primary key, name nvarchar(255), a integer, b integer, constraint name_unique unique (name));")
                .Execute();

        public void AddItem(string name, int a, int b) =>
            DataAccessService
                .GetQuery("insert into item (name, a, b) values (@name, @a, @b)")
                .WithParameter("name", name)
                .WithParameter("a", a)
                .WithParameter("b", b)
                .Execute();

        public void AddItem(Item item) => AddItem(item.Name, item.IntA, item.IntB);

        public Item ReadItem(string name) =>
            DataAccessService
                .GetQuery("select " + ItemSelectString + " from item where name=@name")
                .WithParameter("name", name)
                .Returning<Item>()
                .ReadingItemProperties()
                .Execute()
                .FirstOrDefault()
            ;

        public IEnumerable<Item> ReadItems() =>
            DataAccessService
                .GetQuery("select " + ItemSelectString + " from item order by name")
                .Returning<Item>()
                .ReadingItemProperties()
                .Execute()
            ;

        private string ItemSelectString => "name, a, b";
    }
}