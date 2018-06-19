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
            General.HOI4_Path = @"G:\CSharp Projects\HOI4_Parser\Test_HOI4_Parser\bin\Debug";
            TechTree techTree = new TechTree("ammunition.txt");

            int count = 0;
            foreach (var tech_id in techTree.TechIDs)
            {
                Console.WriteLine($"{++count}  {tech_id}");
            }

            Console.WriteLine();
            Console.WriteLine(techTree.GetFolderName());

            /*foreach(var root_id in techTree.RootIDs)
            {
                Console.WriteLine(root_id);
            }
            */

            Console.ReadLine();
        }
    }
}
