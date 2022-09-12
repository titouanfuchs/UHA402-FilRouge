using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeAPI.Models.Shapes
{
    public class RectangleShape : BaseShape
    {
        #region Fields
        public double Lenght { get; set; } = 0;
        public double Width { get; set; } = 0;
        #endregion

        public RectangleShape(string name, double lenght = 1, double width = 1) : base(name) {
            Lenght = lenght;
            Width = width;
            _ShapeType = ShapeType.Rectangle;
        }

        public RectangleShape() : base() { }

        #region Methods
        public override double CalculateSurface()
        {
            return Lenght * Width;
        }

        public override double CalculatePerimeter()
        {
            return Lenght * (double)2 + Width * (double)2;
        }

        public override void Display()
        {
            Console.WriteLine("Type de la forme : Rectangle");
            base.Display();
        }
        #endregion
    }
}