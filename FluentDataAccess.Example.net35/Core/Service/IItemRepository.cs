using FluentDataAccess.Example.net35.DomainModel;
using System.Collections.Generic;

namespace FluentDataAccess.Example.net35.Core.Service
{
    public interface IItemRepository
    {
        void Init();

        void AddItem(string name, int a, int b);

        void AddItem(Item item);

        Item ReadItem(string name);

        IEnumerable<Item> ReadItems();
    }
}