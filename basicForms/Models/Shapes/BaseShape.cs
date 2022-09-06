using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicForms.Models.Shapes
{
    public class BaseShape
    {
        public string Name = "Nouvelle forme";

        public BaseShape(string name)
        {
            Name = name;
        }

        public virtual double Surface()
        {
            throw new NotImplementedException();
        }

        public virtual double Perimeter()
        {
            throw new NotImplementedException();
        }

        public virtual void Display()
        {
            Console.WriteLine($"Nom de la forme : {Name}");
            Console.WriteLine($"Aire : {Surface()}");
            Console.WriteLine($"Périmètre : {Perimeter()}");
        }
    }
}
