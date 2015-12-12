using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Element GetElementFromAtomicNumber(double atomicNumber)
        {
            var foundElement = _elements.Find(element => element.MassNumber.Equals(atomicNumber));

            if (foundElement != null)
            {
                return _elements.Find(element => element.MassNumber.Equals(atomicNumber));
            }

            throw new ArgumentNullException();
        }

        public static ElementDictionary LoadFromJSON(string directory)
        {
            var reader = new StreamReader(directory);
            return new ElementDictionary {_elements = JsonConvert.DeserializeObject<List<Element>>(reader.ReadToEnd())};
        }
    }
}
