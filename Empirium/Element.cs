using System;

namespace Empirium
{
    public class Element
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double MassNumber { get; set; }
    }

    public class UnknownMassNumberException : Exception
    {
        public UnknownMassNumberException(Element element) : base(element.Name + " has no known value. Cannot compute.") { }
    }
}
