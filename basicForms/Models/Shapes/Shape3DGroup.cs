using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeAPI.Models.Shapes
{
    public class Shape3DGroup
    {
        #region Fields
        private List<Shape3D> _Shapes = new List<Shape3D>();
        public List<Shape3D> Shapes { get => _Shapes; }

        private string _GroupName = "Nouveau Groupe de formes 3D";
        public string GroupName { get => _GroupName; }
        public string SetGroupName { set => _GroupName = value; }
        #endregion

        public Shape3DGroup(string name)
        {
            _GroupName = name;
        }

        #region Methods
        public void AddShape(Shape3D shape)
        {
            _Shapes.Add(shape);
        }

        public double CalculateTotalVolume()
        {
            double totalVolume = 0;
            _Shapes.ForEach(s => totalVolume += s.Volume);

            return totalVolume;
        }

        public void Display()
        {
            Console.WriteLine($"Nom du groupe : {_GroupName}");
            Console.WriteLine($"Nombre de formes : {_Shapes.Count()}");
            Console.WriteLine($"Nombre de Triangles3D : {_Shapes.Count(s => s.Shape.GetType() == typeof(TriangleShape))}");
            Console.WriteLine($"Nombre de Cylindres : {_Shapes.Count(s => s.Shape.GetType() == typeof(CircleShape))}");
            Console.WriteLine($"Nombre de Rectangle3D : {_Shapes.Count(s => s.Shape.GetType() == typeof(RectangleShape))}");

            Console.WriteLine("");
            Console.WriteLine($"Volume total de toutes les formes : {CalculateTotalVolume()}");
        }
        #endregion
    }
}
