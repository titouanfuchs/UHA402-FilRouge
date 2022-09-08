using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeAPI.Models.Shapes
{
    public class RectangleShape3D : RectangleShape, I3DShape
    {
        #region Fields
        private double _Height = 1;
        public double SetHeight { set=>_Height = value; }
        public double Height { get => _Height; }
        #endregion

        public RectangleShape3D(string name, double lenght = 1, double width = 1, double height = 1) : base(name, lenght, width)
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
