
namespace ShapeAPI.Services
{
    public class ShapesService
    {
        private List<ShapeGroup> _ShapeGroups = new List<ShapeGroup>();
        private List<Shape3DGroup> _Shape3DGroups = new List<Shape3DGroup>();

        public GetAllShapesResponse GetAll()
        {
            return new GetAllShapesResponse("Success", _Shape3DGroups, _ShapeGroups);
        }
    }
}
