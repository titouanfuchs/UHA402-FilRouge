using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeAPI.Models.Shapes
{
    public class TriangleShape : BaseShape
    {
        #region Fields
        //A
        protected double _BaseLenght = 1;
        public double BaseLenght { get => _BaseLenght; }
        public double SetBaseLenght { set => _BaseLenght = value; }

        //B
        protected double _SideOne = 1;
        public double SideOne { get => _SideOne; }
        public double SetSideOne { set => _SideOne = value; }

        //C
        protected double _SideTwo = 1;
        public double SideTwo { get => _SideTwo; }
        public double SetSideTwo { set => _SideTwo = value; }

        #endregion

        public TriangleShape(string name, double baseLenght, double sideOne, double sideTwo) : base(name)
        {
            _BaseLenght = baseLenght < 0 ? -baseLenght : baseLenght;
            _SideOne = sideOne < 0 ? -sideOne : sideOne;
            _SideTwo = sideTwo <0 ? -sideTwo : sideTwo;
            _ShapeType = ShapeType.Triangle;
        }

        #region Methods

        public override double CalculateSurface()
        {
            if (!TriangleExist())
                return 0;

            double p = CalculatePerimeter() / (double)2;

            return Math.Sqrt(p*(p-_BaseLenght)*(p-_SideOne)*(p-_SideTwo));
        }

        public override double CalculatePerimeter()
        {
            return _BaseLenght + _SideOne + _SideTwo;
        }

        public bool TriangleExist()
        {
            if (_BaseLenght + _SideOne <= _SideTwo) return false;
            if (_BaseLenght + _SideTwo <= _SideOne) return false;
            if (_SideOne + _SideTwo <= _BaseLenght) return false;

            return true;
        }


        public override void Display()
        {
            Console.WriteLine("Type de la forme : Triangle");
            Console.WriteLine($"Le triangle existe : {TriangleExist()}");

            if (TriangleExist())
                base.Display();
        }

        #endregion
    }
}
