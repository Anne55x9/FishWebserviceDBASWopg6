using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatchDBRESTClientopg6.ServiceReference1;


namespace CatchDBRESTClientopg6
{
    class Program
    {
        static void Main(string[] args)
        {

            using (FishServiceDBClient client = new FishServiceDBClient("BasicHttpsBinding_IFishServiceDB"))
            {
                Fangst[] allFangsts = client.GetCatchesDB();

                foreach (var fa in allFangsts)
                {
                    Console.WriteLine(fa.Navn + fa.Uge);
                }

                client.AddCatchDB("Lis", "Torsk", 8, "Dk", 27);

                Fangst[] allFangstsUge = client.GetWeekCatchDB(27);

                foreach (var fa in allFangstsUge)
                {
                    Console.WriteLine(fa.Navn + fa.Art + fa.Uge);
                }

                client.AddCatchDB("Gert","Sild",66,"Polen",88);

                


            }

            Console.ReadLine();
        }
    }
}
