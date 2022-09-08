using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeAPI.Models.Shapes
{
    public abstract class BaseShape
    {
        #region Fields
        protected string _Name = "Nouvelle forme";
        public string Name { get => _Name; }
        public string SetName { set => _Name = value; }
        #endregion

        public BaseShape(string name)
        {
            _Name = name;
        }

        #region
        public abstract double Surface();

        public abstract double Perimeter();

        public virtual void Display()
        {
            Console.WriteLine($"Nom de la forme : {_Name}");
            Console.WriteLine($"Aire : {Surface()}");
            Console.WriteLine($"Périmètre : {Perimeter()}");
        }

        #endregion
    }
}
