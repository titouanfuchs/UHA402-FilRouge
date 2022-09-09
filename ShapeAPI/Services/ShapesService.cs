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
        private List<ShapeGroup> _ShapesGroups = new List<ShapeGroup>();

        #region ShapeGroup

        public ShapeGroup CreateGroup(string groupName)
        {
            ShapeGroup group = new ShapeGroup(groupName);
            _ShapesGroups.Add(group);

            return group;
        }

        public void AddShapeToGroup(int groupID, int shapeID)
        {
            BaseShape? shape = _Shapes.Find(s => s.Id == shapeID);
            ShapeGroup? shapeGroup = _ShapesGroups.Find(g => g.Id == groupID);

            if (shapeGroup is null)
                throw new ArgumentException($"ShapeGroup with ID {groupID} not found.");

            shapeGroup.AddShape(shape!);
            _Shapes.Remove(shape!);
        }

        #endregion

        #region Shapes

        public GetAllShapesResponse GetAll()
        {
            return new GetAllShapesResponse("Success", _Shapes);
        }

        public BaseShape? GetShape(int id)
        {
            return _Shapes.Find(s => s.Id == id);
        }

        public void DeleteShape(int id)
        {
            BaseShape? shape = _Shapes.Find(s => s.Id == id);

            if (shape is null)
                throw new ArgumentException($"Shape with ID {id} not found.");

            _Shapes.Remove(shape);
        }

        public BaseShape EditShape(int id, CreateShape editQuery)
        {
            BaseShape? shape = _Shapes.Find(s => s.Id == id);

            if (shape is null)
                throw new ArgumentException($"Shape with ID {id} not found.");

            Type type = shape.GetType();

            if (type == typeof(RectangleShape) )
            {
                RectangleShape? rect = shape as RectangleShape;

                rect!.SetWidth = (double)(editQuery.Width is not null ? editQuery.Width : rect.Width);
                rect!.SetLenght = (double)(editQuery.Lenght is not null ? editQuery.Lenght : rect.Lenght);

                shape = rect;
            }
            else if (type == typeof(CircleShape))
            {
                CircleShape? circle = shape as CircleShape;

                circle!.SetDiameter = (double)(editQuery.Diameter is not null ? editQuery.Diameter : circle.Diameter);

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

            return shape;
        }

        public BaseShape CreateShape(CreateShape shapeQuery, ShapeType type)
        {
            BaseShape? newShape = null;

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

            return newShape;
        }
        #endregion
    }
}