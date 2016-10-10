using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Empirium
{
    public class EmpiricalCalculator
    {
        public static ElementDictionary ElementDictionary = new ElementDictionary();

        private const string UncalculatableMessage = "Could not work it out! Remember, this program rounds numbers (in order to gain a result without requiring stupid calculations) and uses one of many sets of mass numbers.";

        public static string Calculate(string input)
        {
            // Format input into element and mass
            var components = Regex.Split(input, @"[,\s]\s *");
            var inputDictionary = new Dictionary<Element, double>();

            foreach (var component in components)
            {
                try
                {
                    var mass = component.Split(' ')[0].Replace("g", string.Empty);
                    var elementIdentifier = component.Split(' ')[1];

                    inputDictionary.Add(
                        elementIdentifier.Length <= 2
                            ? ElementDictionary.GetElementFromSymbol(elementIdentifier)
                            : ElementDictionary.GetElementFromName(elementIdentifier), Convert.ToDouble(mass));
                }
                catch (FormatException)
                {
                    return "One or more entered components incorrectly formatted!";
                }
                catch (IndexOutOfRangeException)
                {
                    return "Too few entered components.";
                }
                catch (ArgumentNullException)
                {
                    return "Could not ascertain element from input.";
                }

            }

            // If the mass number is 0, do not attempt to calculate
            if (inputDictionary.Keys.Any(element => element.MassNumber.Equals(0)))
            {
                throw new UnknownMassNumberException(inputDictionary.Keys.First(element => element.MassNumber.Equals(0)));
            }

            // Get the quantities by dividing the mass given in the input by the mass number of the given element, then dividing that by the smallest result
            var quantities = inputDictionary.Select(component => component.Value / component.Key.MassNumber).ToList().Select(mass => mass / inputDictionary.Select(component => component.Value / component.Key.MassNumber).ToList().Min()).ToList();
            for (var index = 0; index < quantities.Count; index++)
            {
                quantities[index] = Math.Round(quantities[index], 1);
            }

            // While there is any value that is not an integer (as in, whole number, not data type), multiply them by an increasing number
            var i = 1;
            while (quantities.Any(quantity => !quantity.Equals((int)quantity)) && i <= 1000)
            {
                // Set upper limit on mass
                if (i == 1000)
                {
                    return UncalculatableMessage;
                }

                // If multipling from by the increasing number will not work, do not bother
                if (quantities.Any(quantity => !(quantity * 2).Equals((int)(quantity * 2))))
                {
                    i++;
                }
                else
                {
                    for (var j = 0; j <= quantities.Count - 1; j++)
                    {
                        quantities[j] = quantities[j] * i;
                    }

                    i++;
                }
                
            }

            // Initialize dictionary for output (element symbol, and quantity) then populate
            var outputDictionary = new Dictionary<string, double>();

            for (var j = 0; j <= inputDictionary.Count - 1; j++)
            {
                outputDictionary.Add(inputDictionary.Keys.ElementAt(j).Symbol, quantities[j]);
            }

            // Separate output and format
            var empiricalFormula = outputDictionary.Aggregate("", (current, component) =>
                current + component.Key + (component.Value.Equals(1) || component.Value.Equals(0) ? string.Empty: component.Value.ToString()) + " ");

            return empiricalFormula;
        }
    }
}
