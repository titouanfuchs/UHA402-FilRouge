namespace ShapeAPI.Models.DTO
{
    public class GetAllShapesResponse : BaseResponse
    {
        private List<Shape3DGroup> _Shapes3D;
        public List<Shape3DGroup> Shapes3D { get => _Shapes3D; }

        private List<ShapeGroup> _Shapes;
        public List<ShapeGroup> Shapes { get => _Shapes; }

        public GetAllShapesResponse(string message, List<Shape3DGroup> shapes3D, List<ShapeGroup> shapes) : base(message)
        {
            _Shapes3D = shapes3D;
            _Shapes = shapes;
        }
    }
}
