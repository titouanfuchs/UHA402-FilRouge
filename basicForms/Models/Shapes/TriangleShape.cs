using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicForms.Models.Shapes
{
    public class TriangleShape : BaseShape
    {
        #region Fields

        private bool _IsRectangle = false;
        public bool IsRectangle { get => _IsRectangle; }

        // H
        private double _HLenght = 1;
        public double HLenght { get => _HLenght; }
        public double SetHLenght { set => _HLenght = value; }

        //BC
        private double _BaseLenght = 1;
        public double BaseLenght { get => _BaseLenght; }
        public double SetBaseLenght { set => _BaseLenght = value; }
        #endregion

        //BA
        private double _SideOne = 1;
        public double SideOne { get => _SideOne; }
        public double SetSideOne { set => _SideOne = value; }

        //CA
        private double _SideTwo = 1;
        public double SideTwo { get => _SideTwo; }
        public double SetSideTwo { set => _SideTwo = value; }

        public TriangleShape(string name, bool isRectangle,double baseLenght, double sideOne, double sideTwo) : base(name)
        {
            _IsRectangle = isRectangle;
            _HLenght = _CalculateH();
            _BaseLenght = baseLenght;
            _SideOne = sideOne;
            _SideTwo = sideTwo;
        }

        private double _CalculateH()
        {
            double diva;
            double pa;
            double pb;
            double pc;
            double hauteur;
            double peri;
            peri = Perimeter() / 2;
            pc = peri - _SideTwo;
            pb = peri - _SideOne;
            pa = peri - _BaseLenght;
            hauteur = peri * pa * pb * pc;
            hauteur = Math.Sqrt(hauteur);
            diva = _BaseLenght;
            hauteur = diva * hauteur;

            return hauteur;
        }

        public override double Surface()
        {
            double surface = 0;

            double p = Perimeter() / 2;

            return Math.Sqrt(p*(p-_BaseLenght)*(p-_SideOne)*(p-_SideTwo));
        }

        public override double Perimeter()
        {
            return _BaseLenght + _SideOne + _SideTwo;
        }

        public override void Display()
        {
            Console.WriteLine("Type de la forme : Triangle");
            Console.WriteLine($"Hauteur : {_HLenght}");
            base.Display();
        }
    }
}
