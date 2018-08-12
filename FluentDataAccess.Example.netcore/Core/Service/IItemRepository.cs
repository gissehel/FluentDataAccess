using FluentDataAccess.Example.netcore.DomainModel;
using System.Collections.Generic;

namespace FluentDataAccess.Example.netcore.Core.Service
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