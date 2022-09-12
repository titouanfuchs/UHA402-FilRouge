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
        protected double _Lenght = 0;
        protected double _Width = 0;
        public double Width { get => _Width; }
        public double SetWidth
        {
            set => _Width = value;
        }

        public double Lenght { get => _Lenght; }
        public double SetLenght
        {
            set => _Lenght = value;
        }
        #endregion

        public RectangleShape(string name, double lenght = 1, double width = 1) : base(name) {
            _Lenght = lenght;
            _Width = width;
        }

        #region Methods
        public override double Surface()
        {
            return _Lenght * _Width;
        }

        public override double Perimeter()
        {
            return _Lenght * (double)2 + _Width * (double)2;
        }

        public override void Display()
        {
            Console.WriteLine("Type de la forme : Rectangle");
            base.Display();
        }
        #endregion
    }
}