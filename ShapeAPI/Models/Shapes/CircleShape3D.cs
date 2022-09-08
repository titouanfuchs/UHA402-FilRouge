using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeAPI.Models.Shapes
{
    public class CircleShape3D : CircleShape, I3DShape
    {
        #region Fields
        private double _Height = 1;
        public double SetHeight { set => _Height = value; }
        public double Height { get => _Height; }
        #endregion

        public CircleShape3D(string name, double diameter, double height) : base(name, diameter)
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
