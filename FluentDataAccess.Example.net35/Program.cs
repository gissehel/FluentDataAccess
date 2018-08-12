using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentDataAccess.Example.net35
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