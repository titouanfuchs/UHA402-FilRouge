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

        protected int _Id;
        public int Id { get => _Id; }

        protected ShapeType _ShapeType;
        public ShapeType ShapeType { get => _ShapeType; }

        public BaseShape(string name)
        {
            _Name = name;
            _Id = ShapesService.GetNewID();
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
