using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicForms.Models.Shapes
{
    public class TriangleShape3D : TriangleShape, I3DShape
    {
        #region Fields
        private double _Height = 1;
        public double SetHeight { set => _Height = value; }
        public double Height { get => _Height; }
        #endregion

        public TriangleShape3D(string name, double baseLenght, double sideOne, double sideTwo, double height) : base(name, baseLenght, sideOne, sideTwo)
        {
            _Height = height;
        }

        #region Methods
        public double Volume()
        {
            return _Height * Surface();
        }
        #endregion
    }
}
