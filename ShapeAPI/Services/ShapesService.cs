using Microsoft.AspNetCore.Rewrite;

namespace basicForms.Services
{
    public class ShapesService
    {
        private static int _IDTracker = 0;

        public static int GetNewID()
        {
            _IDTracker++;
            return _IDTracker;
        }

        private List<BaseShape> _Shapes = new List<BaseShape>();

        public GetAllShapesResponse GetAll()
        {
            return new GetAllShapesResponse("Success", _Shapes);
        }

        public BaseShape Get(int id)
        {
            return _Shapes.Find(s => s.Id == id);
        }

        public bool DeleteShape(int id)
        {
            try
            {
                _Shapes.Remove(_Shapes.Find(s => s.Id == id));
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public BaseShape EditShape(int id, CreateShape editQuery)
        {
            BaseShape? shape = _Shapes.Find(s => s.Id == id);

            if (shape is null)
                throw new ArgumentException($"Shape with ID {id} not found.");

            Type type = shape.GetType();

            if (type == typeof(RectangleShape))
            {
                RectangleShape? rect = shape as RectangleShape;

                rect!.SetWidth = (double)(editQuery.Width is not null ? editQuery.Width : rect.Width);
                rect!.SetLenght = (double)(editQuery.Lenght is not null ? editQuery.Lenght : rect.Lenght);

                shape = rect;
            }
            else if (type == typeof(RectangleShape3D))
            {
                RectangleShape3D? rect = shape as RectangleShape3D;

                rect!.SetWidth = (double)(editQuery.Width is not null ? editQuery.Width : rect.Width);
                rect!.SetLenght = (double)(editQuery.Lenght is not null ? editQuery.Lenght : rect.Lenght);
                rect!.SetHeight = (double)(editQuery.Height is not null ? editQuery.Height : rect.Height);

                shape = rect;
            }
            else if (type == typeof(CircleShape))
            {
                CircleShape? circle = shape as CircleShape;

                circle!.SetDiameter = (double)(editQuery.Diameter is not null ? editQuery.Diameter : circle.Diameter);

                shape = circle;
            }
            else if (type == typeof(CircleShape3D))
            {
                CircleShape3D? circle = shape as CircleShape3D;

                circle!.SetDiameter = (double)(editQuery.Diameter is not null ? editQuery.Diameter : circle.Diameter);
                circle!.SetHeight = (double)(editQuery.Height is not null ? editQuery.Height : circle.Height);

                shape = circle;
            }
            else if (type == typeof(TriangleShape))
            {
                TriangleShape? triangle = shape as TriangleShape;

                triangle!.SetBaseLenght = (double)(editQuery.Base is not null ? editQuery.Base : triangle.BaseLenght);
                triangle!.SetSideOne = (double)(editQuery.SideOne is not null ? editQuery.SideOne : triangle.SideOne);
                triangle!.SetSideTwo = (double)(editQuery.SideTwo is not null ? editQuery.SideTwo : triangle.SideTwo);

                shape = triangle;
            }
            else if (type == typeof(TriangleShape3D))
            {
                TriangleShape3D? triangle = shape as TriangleShape3D;

                triangle!.SetBaseLenght = (double)(editQuery.Base is not null ? editQuery.Base : triangle.BaseLenght);
                triangle!.SetSideOne = (double)(editQuery.SideOne is not null ? editQuery.SideOne : triangle.SideOne);
                triangle!.SetSideTwo = (double)(editQuery.SideTwo is not null ? editQuery.SideTwo : triangle.SideTwo);
                triangle!.SetHeight = (double)(editQuery.Height is not null ? editQuery.Height : triangle.Height);

                shape = triangle;
            }

            return shape;
        }

        public void CreateShape(CreateShape shapeQuery, ShapeDimension dimension, ShapeType type)
        {
            BaseShape newShape = null;

            if (dimension == ShapeDimension.Shape)
            {
                switch (type)
                {
                    default:
                        throw new ArgumentException("Invalide shape type for current shape dimension");
                    case ShapeType.Rectangle:
                        if (shapeQuery.Lenght is not null && shapeQuery.Width is not null)
                        {
                            newShape = new RectangleShape(shapeQuery.Name, (double)shapeQuery.Lenght, (double)shapeQuery.Width);
                            _Shapes.Add(newShape);
                            break;
                        }

                        throw new ArgumentNullException("Lenght and Width must not be null. (Rectangle)");

                    case ShapeType.Triangle:
                        if (shapeQuery.Base is not null && shapeQuery.SideOne is not null && shapeQuery.SideTwo is not null)
                        {
                            newShape = new TriangleShape(shapeQuery.Name, (double)shapeQuery.Base, (double)shapeQuery.SideOne, (double)shapeQuery.SideTwo);

                            if (!((TriangleShape)newShape).TriangleExist())
                                throw new Exception("Triangle is not possible !");

                            _Shapes.Add(newShape);
                            break;
                        }

                        throw new ArgumentNullException("Base, SideOne and SideTwo must not be null. (Triangle)");

                    case ShapeType.Circle:
                        if (shapeQuery.Diameter is not null)
                        {
                            newShape = new CircleShape(shapeQuery.Name, (double)shapeQuery.Diameter);
                            _Shapes.Add(newShape);
                            break;
                        }

                        throw new ArgumentNullException("Diameter must not be null. (Circle)");
                }
            }
            else
            {
                if (shapeQuery.Height is null)
                    throw new ArgumentNullException("Height cannot be null on 3D Shape");

                switch (type)
                {
                    default:
                        throw new ArgumentException("Invalide shape type for current shape dimension");
                    case ShapeType.Rectangle3D:
                        if (shapeQuery.Lenght is not null && shapeQuery.Width is not null)
                        {
                            newShape = new RectangleShape3D(shapeQuery.Name, (double)shapeQuery.Lenght, (double)shapeQuery.Width, (double)shapeQuery.Height);
                            _Shapes.Add(newShape);
                            break;
                        }

                        throw new ArgumentNullException("Lenght and Width must not be null. (Rectangle3D)");

                    case ShapeType.Triangle3D:
                        if (shapeQuery.Base is not null && shapeQuery.SideOne is not null && shapeQuery.SideTwo is not null)
                        {
                            newShape = new TriangleShape3D(shapeQuery.Name, (double)shapeQuery.Base, (double)shapeQuery.SideOne, (double)shapeQuery.SideTwo, (double)shapeQuery.Height);

                            if (!((TriangleShape3D)newShape).TriangleExist())
                                throw new Exception("3D Triangle is not possible !");

                            _Shapes.Add(newShape);
                            break;
                        }

                        throw new ArgumentNullException("Base, SideOne and SideTwo must not be null. (Triangle3D)");

                    case ShapeType.Cylindre:
                        if (shapeQuery.Diameter is not null)
                        {
                            newShape = new CircleShape3D(shapeQuery.Name, (double)shapeQuery.Diameter, (double)shapeQuery.Height);
                            _Shapes.Add(newShape);
                            break;
                        }

                        throw new ArgumentNullException("Diameter must not be null. (Cylinder)");
                }
            }
        }
    }
}
