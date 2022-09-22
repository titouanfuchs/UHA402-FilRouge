using Microsoft.AspNetCore.Rewrite;
using ShapeAPI.Models.DTO;
using System.Text.RegularExpressions;

namespace ShapeAPI.Services
{
    public class ShapesService
    {
        private DataContext _Context;

        public ShapesService(DataContext dataContext)
        {
            _Context = dataContext;
        }

        #region ShapeGroup

        public void Init(int x, int y)
        {
            _Context.ShapesGroups.RemoveRange(_Context.ShapesGroups.ToList());
            _Context.SaveChanges();

            for (int X = 0; X < x; X++)
                for (int Y = 0; Y < y; Y++)
                    CreateGroup($"G_{X}-{Y}", X, Y);
        }

        public ShapeGroup CreateGroup(string groupName)
        {
            ShapeGroup group = new ShapeGroup(groupName);

            _Context.ShapesGroups.Add(group);

            _Context.SaveChanges();

            return group;
        }

        public ShapeGroup CreateGroup(string groupName, int x, int y)
        {
            ShapeGroup group = new ShapeGroup(new Position() { X = x, Y = y, }, groupName);

            _Context.ShapesGroups.Add(group);

            _Context.SaveChanges();

            return group;
        }

        public ShapeGroup EditGroup(ShapeGroupDTO query, int id)
        {
            ShapeGroup group = GetGroup(id);

            group.GroupName = query.GroupName;

            _Context.SaveChanges();

            return group;
        }

        public List<ShapeGroup> GetGroups()
        {
            return _Context.ShapesGroups
                .Include(groupe => groupe.Shapes)
                .Include(groupe => groupe.GroupPosition)
                .ToList();
        }

        public ShapeGroup GetGroup(int id)
        {
            List<ShapeGroup> shapeGroups = _Context.ShapesGroups
                .Where(s => s.Id == id)
                .Include(groupe => groupe.Shapes)
                .ToList();

            if (shapeGroups.Count() == 0)
                throw new ArgumentException($"ShapeGroup with ID {id} not found.");

            return shapeGroups[0];
        }

        public void DeleteShapeGroup(int id)
        {
            ShapeGroup? shape = _Context.Find<ShapeGroup>(id);

            if (shape is null)
                throw new ArgumentException($"ShapeGroup with ID {id} not found.");

            _Context.Remove<ShapeGroup>(shape);
            _Context.SaveChanges();
        }

        public void AddShapeToGroup(int groupID, int shapeID)
        {
            BaseShape shape = GetShape(shapeID);
            ShapeGroup shapeGroup = GetGroup(groupID);

            shapeGroup.AddShape(shape!);
            _Context.Update<ShapeGroup>(shapeGroup);
            _Context.SaveChanges();
        }

        #endregion

        #region Shapes

        public List<BaseShape> GetAll()
        {
            List<BaseShape> result = new List<BaseShape>();

            result.AddRange(_Context.RectangleShapes.ToList());
            result.AddRange(_Context.TriangleShapes.ToList());
            result.AddRange(_Context.CircleShapes.ToList());

            return result;
        }

        public BaseShape GetShape(int id)
        {
            BaseShape? shape = null;
            try
            {
                 shape = _Context.Find<BaseShape>(id);
            }catch(Exception e)
            {
                throw new ArgumentException($"Shape with ID {id} not found.");
            }

            return shape;
        }

        public void DeleteShape(int id)
        {
            BaseShape? shape = _Context.Find<BaseShape>(id);

            if (shape is null)
                throw new ArgumentException($"Shape with ID {id} not found.");

            _Context.Remove<BaseShape>(shape);
            _Context.SaveChanges();
        }

        public BaseShape EditShape(int id, CreateShape editQuery)
        {
            BaseShape? shape = _Context.Find<BaseShape>(id);

            if (shape is null)
                throw new ArgumentException($"Shape with ID {id} not found.");

            shape.Name = editQuery.Name;

            Type type = shape.GetType();

            if (type == typeof(RectangleShape) )
            {
                RectangleShape? rect = shape as RectangleShape;

                rect!.Width = (double)(editQuery.Width is not null ? editQuery.Width : rect.Width);
                rect!.Lenght = (double)(editQuery.Lenght is not null ? editQuery.Lenght : rect.Lenght);

                shape = rect;
            }
            else if (type == typeof(CircleShape))
            {
                CircleShape? circle = shape as CircleShape;

                circle!.Diameter = (double)(editQuery.Diameter is not null ? editQuery.Diameter : circle.Diameter);

                shape = circle;
            }
            else if (type == typeof(TriangleShape))
            {
                TriangleShape? triangle = shape as TriangleShape;

                triangle!.BaseLenght = (double)(editQuery.Base is not null ? editQuery.Base : triangle.BaseLenght);
                triangle!.SideOne = (double)(editQuery.SideOne is not null ? editQuery.SideOne : triangle.SideOne);
                triangle!.SideTwo = (double)(editQuery.SideTwo is not null ? editQuery.SideTwo : triangle.SideTwo);

                shape = triangle;
            }

            _Context.SaveChanges();

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
                        var rect = new RectangleShape(shapeQuery.Name, (double)shapeQuery.Lenght, (double)shapeQuery.Width);
                        newShape = rect;
                        _Context.RectangleShapes.Add(rect);
                        break;
                    }

                    throw new ArgumentNullException("Lenght and Width must not be null. (Rectangle)");

                case ShapeType.Triangle:
                    if (shapeQuery.Base is not null && shapeQuery.SideOne is not null && shapeQuery.SideTwo is not null)
                    {
                        var tri = new TriangleShape(shapeQuery.Name, (double)shapeQuery.Base, (double)shapeQuery.SideOne, (double)shapeQuery.SideTwo);
                        newShape = tri;

                        if (!((TriangleShape)newShape).TriangleExist())
                            throw new Exception("Triangle is not possible !");

                        _Context.TriangleShapes.Add(tri);
                        break;
                    }

                    throw new ArgumentNullException("Base, SideOne and SideTwo must not be null. (Triangle)");

                case ShapeType.Circle:
                    if (shapeQuery.Diameter is not null)
                    {
                        var circle = new CircleShape(shapeQuery.Name, (double)shapeQuery.Diameter);
                        newShape = circle;
                        _Context.CircleShapes.Add(circle);
                        break;
                    }

                    throw new ArgumentNullException("Diameter must not be null. (Circle)");
            }
            _Context.SaveChanges();

            return newShape;
        }
        #endregion
    }
}