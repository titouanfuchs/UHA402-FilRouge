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
        public string Name { get; set; }

        [Key] public int Id { get; set; }

        public double Surface { get => CalculateSurface(); }
        public double Perimeter { get => CalculatePerimeter(); }
        #endregion


        public BaseShape(string name)
        {
            Name = name;
        }

        public BaseShape()
        {
            Name = "";
        }

        #region Methods
        public virtual double CalculateSurface() { return 0; }

        public virtual double CalculatePerimeter() { return 0; }

        public virtual void Display()
        {
            Console.WriteLine($"Nom de la forme : {Name}");
            Console.WriteLine($"Aire : {CalculateSurface()}");
            Console.WriteLine($"Périmètre : {CalculatePerimeter()}");
        }

        #endregion
    }
}
