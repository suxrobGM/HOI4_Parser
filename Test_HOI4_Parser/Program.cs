using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOI4_Parser;

namespace Test_HOI4_Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            TechTree techTree = new TechTree("air_doctrine.txt");

            /*foreach(var tech_id in techTree.TechIDs)
            {
                Console.WriteLine(tech_id);
            }
            */
            
            foreach(var root_id in techTree.RootIDs)
            {
                Console.WriteLine(root_id);
            }

            Console.ReadLine();
        }
    }
}
