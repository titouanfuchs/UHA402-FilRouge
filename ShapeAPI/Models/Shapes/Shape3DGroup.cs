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
        private List<I3DShape> _Shapes = new List<I3DShape>();
        public List<I3DShape> Shapes { get => _Shapes; }

        private string _GroupName = "Nouveau Groupe de formes 3D";
        public string GroupName { get => _GroupName; }
        public string SetGroupName { set => _GroupName = value; }
        #endregion

        public Shape3DGroup(string name)
        {
            _GroupName = name;
        }

        #region Methods
        public void AddShape(I3DShape shape)
        {
            _Shapes.Add(shape);
        }

        public double CalculateTotalVolume()
        {
            double totalVolume = 0;
            _Shapes.ForEach(s => totalVolume += s.Volume());

            return totalVolume;
        }

        public int CountExistingTriangles()
        {
            int total = 0;

            foreach (BaseShape shape in _Shapes)
            {
                if (shape.GetType() == typeof(TriangleShape3D))
                {
                    TriangleShape3D? triangle = shape as TriangleShape3D;

                    if (triangle is null)
                        continue;

                    if (triangle.TriangleExist())
                        total++;
                }
            }

            return total;
        }

        public void Display()
        {
            Console.WriteLine($"Nom du groupe : {_GroupName}");
            Console.WriteLine($"Nombre de formes : {_Shapes.Count()}");
            Console.WriteLine($"Nombre de Triangles3D : {_Shapes.Count(s => s.GetType() == typeof(TriangleShape3D))}");
            Console.WriteLine($"Nombre de Triangles3D qui éxistent : {CountExistingTriangles()}");
            Console.WriteLine($"Nombre de Cylindres : {_Shapes.Count(s => s.GetType() == typeof(CircleShape3D))}");
            Console.WriteLine($"Nombre de Rectangle3D : {_Shapes.Count(s => s.GetType() == typeof(RectangleShape3D))}");

            Console.WriteLine("");
            Console.WriteLine($"Volume total de toutes les formes : {CalculateTotalVolume()}");
        }
        #endregion
    }
}
