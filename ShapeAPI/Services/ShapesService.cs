
namespace ShapeAPI.Services
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
