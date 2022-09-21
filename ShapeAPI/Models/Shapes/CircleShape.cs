using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeAPI.Models.Shapes
{
    public class CircleShape : BaseShape
    {
        #region Fields
        public Double Diameter { get; set; }

        #endregion

        public CircleShape(string name, double diameter) : base(name)
        {
            Diameter = diameter;
        }
        public CircleShape()
        {

        }

        #region Methods
        public override double CalculatePerimeter()
        {
            return 2 * Math.PI * (Diameter/2);
        }

        public override double CalculateSurface()
        {
            double r = Diameter / 2;

            return Math.Pow(r, 2) * Math.PI;
        }

        public override void Display()
        {
            Console.WriteLine("Type de la forme : Cercle");
            base.Display();
        }
        #endregion
    }
}
