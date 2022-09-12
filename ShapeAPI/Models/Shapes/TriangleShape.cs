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
        public double BaseLenght { get; set; }

        //B
        public double SideOne { get; set; }

        //C
        public double SideTwo { get; set; }

        #endregion

        public TriangleShape(string name, double baseLenght, double sideOne, double sideTwo) : base(name)
        {
            BaseLenght = baseLenght < 0 ? -baseLenght : baseLenght;
            SideOne = sideOne < 0 ? -sideOne : sideOne;
            SideTwo = sideTwo <0 ? -sideTwo : sideTwo;
            _ShapeType = ShapeType.Triangle;
        }

        #region Methods

        public override double CalculateSurface()
        {
            if (!TriangleExist())
                return 0;

            double p = CalculatePerimeter() / (double)2;

            return Math.Sqrt(p*(p-BaseLenght)*(p-SideOne)*(p-SideTwo));
        }

        public override double CalculatePerimeter()
        {
            return BaseLenght + SideOne + SideTwo;
        }

        public bool TriangleExist()
        {
            if (BaseLenght + SideOne <= SideTwo) return false;
            if (BaseLenght + SideTwo <= SideOne) return false;
            if (SideOne + SideTwo <= BaseLenght) return false;

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
