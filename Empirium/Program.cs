using System;

namespace Empirium
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EmpiricalCalculator.ElementDictionary = ElementDictionary.LoadFromJson("../../../elements.json");

            while (true)
            {
                Console.Write("Enter input: ");
                var input = Console.ReadLine();
                Console.WriteLine("Output = " + EmpiricalCalculator.Calculate(input) + "\n");
            }
        }
    }
}
