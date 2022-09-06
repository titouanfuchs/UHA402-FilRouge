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
        private double _HLenght = 1;
        public double HLenght { get => _HLenght; }
        public double SetHLenght { set => _HLenght = value; }

        private double _BaseLenght = 1;
        public double BaseLenght { get => _BaseLenght; }
        public double SetBaseLenght { set => _BaseLenght = value; }
        #endregion

        public TriangleShape(string name, double baseLenght, double hLenght) : base(name)
        {
            _HLenght = hLenght;
            _BaseLenght = baseLenght;
        }

        public override double Surface()
        {
            return (_BaseLenght * _HLenght);
        }

        public override double Perimeter()
        {
            return base.Perimeter();
        }
    }
}
