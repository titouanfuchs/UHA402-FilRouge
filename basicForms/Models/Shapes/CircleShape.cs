using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicForms.Models.Shapes
{
    public class CircleShape : BaseShape
    {
        #region Fields
        private double _Diameter = 0;
        public double Diameter { get => _Diameter; }
        public double SetDiameter { set => _Diameter = value; }
        #endregion

        public CircleShape(string name, double diameter) : base(name)
        {
            _Diameter = diameter;
        }

        #region Methods
        public override double Perimeter()
        {
            return 2 * Math.PI * (_Diameter/2);
        }

        public override double Surface()
        {
            double r = _Diameter / 2;

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
