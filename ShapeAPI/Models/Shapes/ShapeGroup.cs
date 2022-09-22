using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeAPI.Models.Shapes
{
    public class ShapeGroup
    {
        #region Fields

        [Key]
        public int Id { get; set; }
        public List<BaseShape> Shapes { get; set; }
        public string GroupName { get; set; }
        public string Owner { get; set; } = "EVERY";
        public Position GroupPosition { get; set; }

        public double Surface { get => CalculateTotalSurface(); }
        public double Perimeter { get => CalculateTotalPerimeter(); }
        #endregion

        public ShapeGroup(string groupName = "Nouveau Groupe")
        {
            GroupName = groupName;
        }

        public ShapeGroup(Position pos, string groupName = "Nouveau Groupe")
        {
            GroupName = groupName;
            GroupPosition = pos;
        }

        public ShapeGroup()
        {
            GroupName = "";
        }

        #region Methods
        public void AddShape(BaseShape shape)
        {
            Shapes.Add(shape);
        }

        public double CalculateTotalPerimeter()
        {
            double total = 0;
            Shapes.ForEach(s => total += s.CalculatePerimeter());

            return total;
        }

        public double CalculateTotalSurface()
        {
            double total = 0;
            Shapes.ForEach(s => total += s.CalculateSurface());

            return total;
        }

        public int CountExistingTriangles()
        {
            int total = 0;

            foreach(BaseShape shape in Shapes)
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
            Console.WriteLine($"Nombre de formes : {Shapes.Count}");
            Console.WriteLine($"Nombre de Triangles : {Shapes.Count(s => s.GetType() == typeof(TriangleShape))}");
            Console.WriteLine($"Nombre de Triangles qui éxistent : {CountExistingTriangles()}");
            Console.WriteLine($"Nombre de Cercles : {Shapes.Count(s => s.GetType() == typeof(CircleShape))}");
            Console.WriteLine($"Nombre de Rectangle : {Shapes.Count(s => s.GetType() == typeof(RectangleShape))}");

            Console.WriteLine("");
            Console.WriteLine($"Périmètre total des formes : {CalculateTotalPerimeter()}");
            Console.WriteLine($"Aire totale des formes : {CalculateTotalSurface()}");
        }
        #endregion
    }
}
