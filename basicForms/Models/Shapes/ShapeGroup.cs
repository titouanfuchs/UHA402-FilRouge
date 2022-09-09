using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicForms.Models.Shapes
{
    public class ShapeGroup
    {
        #region Fields
        private List<BaseShape> _Shapes = new List<BaseShape>();
        public List<BaseShape> Shapes { get => _Shapes; }

        private string _GroupName = "Nouveau Groupe";
        public string GroupName { get => _GroupName; }
        public string SetGroupName { set => _GroupName = value; }
        #endregion

        public ShapeGroup(string groupName = "Nouveau Groupe")
        {
            _GroupName = groupName;
        }

        #region Methods
        public void AddShape(BaseShape shape)
        {
            _Shapes.Add(shape);
        }

        public double CalculateTotalPerimeter()
        {
            double total = 0;
            _Shapes.ForEach(s => total += s.Perimeter());

            return total;
        }

        public double CalculateTotalSurface()
        {
            double total = 0;
            _Shapes.ForEach(s => total += s.Surface());

            return total;
        }

        public int CountExistingTriangles()
        {
            int total = 0;

            foreach(BaseShape shape in _Shapes)
            {
                if (shape.GetType() == typeof(TriangleShape))
                {
                    TriangleShape? triangle = shape as TriangleShape;

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
            Console.WriteLine($"Nombre de formes : {_Shapes.Count}");
            Console.WriteLine($"Nombre de Triangles : {_Shapes.Count(s => s.GetType() == typeof(TriangleShape))}");
            Console.WriteLine($"Nombre de Triangles qui éxistent : {CountExistingTriangles()}");
            Console.WriteLine($"Nombre de Cercles : {_Shapes.Count(s => s.GetType() == typeof(CircleShape))}");
            Console.WriteLine($"Nombre de Rectangle : {_Shapes.Count(s => s.GetType() == typeof(RectangleShape))}");

            Console.WriteLine("");
            Console.WriteLine($"Périmètre total des formes : {CalculateTotalPerimeter()}");
            Console.WriteLine($"Aire totale des formes : {CalculateTotalSurface()}");
        }
        #endregion
    }
}
