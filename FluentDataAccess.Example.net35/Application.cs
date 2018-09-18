using FluentDataAccess.Example.net35.Core.Service;
using FluentDataAccess.Example.net35.Service;
using System;

namespace FluentDataAccess.Example.net35
{
    public class Application
    {
        public IItemRepository ItemRepository { get; set; }

        public void MainCode()
        {
            IDataAccessPathConfigurationService dataAccessPathConfigurationService = new DataAccessPathConfigurationService();
            IDataAccessService dataAccessService = DataAccessSQLite.GetService(dataAccessPathConfigurationService);
            IItemRepository itemRepository = new ItemRepository(dataAccessService);

            ItemRepository = itemRepository;

            dataAccessService.Init();
            ItemRepository.Init();

            Run();
        }

        private void Run()
        {
            string data = "abcdefghijklmnopqrstuvwxyz";
            int first = 0;
            int second = 1;
            for (int index = 0; index < data.Length - 3; index++)
            {
                var name = data.Substring(index, 3);
                ItemRepository.AddItem(name, first, second);
                var third = first + second;
                first = second;
                second = third;
            }

            {
                Console.WriteLine("----------");
                Console.WriteLine("Here are all the items:");
                Console.WriteLine("----------");
                foreach (var item in ItemRepository.ReadItems())
                {
                    Console.WriteLine(string.Format("[{0}] : ({1}, {2})", item.Name, item.IntA, item.IntB));
                }
                Console.WriteLine("==========");
                Console.WriteLine("");
            }

            {
                Console.WriteLine("----------");
                Console.WriteLine("Here is the 'mno' value:");
                Console.WriteLine("----------");
                var item2 = ItemRepository.ReadItem("mno");
                if (item2 != null)
                {
                    Console.WriteLine(string.Format("[{0}] : ({1}, {2})", item2.Name, item2.IntA, item2.IntB));
                }
                else
                {
                    Console.WriteLine("No data");
                }
                Console.WriteLine("==========");
                Console.WriteLine("");
            }

            {
                Console.WriteLine("----------");
                Console.WriteLine("Here is the 'grut' value:");
                Console.WriteLine("----------");
                var item3 = ItemRepository.ReadItem("grut");
                if (item3 != null)
                {
                    Console.WriteLine(string.Format("[{0}] : ({1}, {2})", item3.Name, item3.IntA, item3.IntB));
                }
                else
                {
                    Console.WriteLine("No data");
                }
                Console.WriteLine("----------");
            }
        }
    }
}