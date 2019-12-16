using Core.DataAccess;
using System;

namespace Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Abrindo aplicação");

            var context = new CoreContext();
            new Seeds.CoreSeed().Execute(context);

            Console.WriteLine("Fechando aplicação");
        }
    }
}
