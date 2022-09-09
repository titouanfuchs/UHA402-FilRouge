using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeAPI.Models.Shapes
{
    public class BaseShape
    {
        #region Fields
        protected string _Name = "Nouvelle forme";
        public string Name { get => _Name; }
        public string SetName { set => _Name = value; }

        [Key] public int Id { get; set; }

        protected ShapeType _ShapeType;
        public ShapeType ShapeType { get => _ShapeType; }

        public double Surface { get => CalculateSurface(); }
        public double Perimeter { get => CalculatePerimeter(); }
        #endregion


        public BaseShape(string name)
        {
            _Name = name;
        }

        public BaseShape()
        {
            _Name = "";
        }

        #region Methods
        public virtual double CalculateSurface() { return 0; }

        public virtual double CalculatePerimeter() { return 0; }

        public virtual void Display()
        {
            Console.WriteLine($"Nom de la forme : {_Name}");
            Console.WriteLine($"Aire : {CalculateSurface()}");
            Console.WriteLine($"Périmètre : {CalculatePerimeter()}");
        }

        #endregion
    }
}
