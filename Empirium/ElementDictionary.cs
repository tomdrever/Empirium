using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Empirium
{
    public class ElementDictionary
    {
        private List<Element> _elements;

        public ElementDictionary()
        {
            _elements = new List<Element>();
        }

        public Element GetElementFromName(string name)
        {
            return _elements.Find(element => element.Name == name.ToLower());
        }

        public Element GetElementFromSymbol(string symbol)
        {
            return _elements.Find(element => element.Symbol == symbol);
        }

        public Element GetElementFromMassNumber(double massNumber)
        {
            var foundElement = _elements.Find(element => element.MassNumber.Equals(massNumber));

            if (foundElement != null)
            {
                return _elements.Find(element => element.MassNumber.Equals(massNumber));
            }

            throw new ArgumentNullException();
        }

        public static ElementDictionary LoadFromJson(string directory)
        {
            var reader = new StreamReader(directory);
            return new ElementDictionary {_elements = JsonConvert.DeserializeObject<List<Element>>(reader.ReadToEnd())};
        }
    }
}
