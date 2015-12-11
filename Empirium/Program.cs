using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirium
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EmpiricalCalculator.ElementDictionary = ElementDictionary.LoadFromJSON("../../../elements.json");

            while (true)
            {
                Console.Write("Enter input: ");
                var input = Console.ReadLine();
                Console.WriteLine("Output = " + EmpiricalCalculator.Calculate(input) + "\n");
            }
        }
    }
}
