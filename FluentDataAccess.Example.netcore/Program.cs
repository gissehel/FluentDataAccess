using System;

namespace FluentDataAccess.Example.netcore
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var application = new Application();
            application.MainCode();
            Console.ReadLine();
        }
    }
}