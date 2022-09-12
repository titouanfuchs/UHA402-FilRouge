namespace ShapeAPI.Models.DTO
{
    public class GetAllShapesResponse : BaseResponse
    {
        private List<BaseShape> _Shapes = new List<BaseShape>();
        public List<BaseShape> Shapes { get => _Shapes; }

        public GetAllShapesResponse(string message, List<BaseShape> shapes) : base(message)
        {
            _Shapes = shapes;
        }
    }
}
