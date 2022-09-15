namespace ShapeAPI.Models.DTO
{
    public class GetAllShapesResponse : BaseResponse
    {
        public List<RectangleShape> Rectangles { get; set; }
        public List<TriangleShape> Triangles { get; set; }
        public List<CircleShape> Circles { get; set; }

        public GetAllShapesResponse(string message, List<BaseShape> shapes) : base(message)
        {
            Rectangles = shapes.OfType<RectangleShape>().ToList();
            Triangles = shapes.OfType<TriangleShape>().ToList();
            Circles = shapes.OfType<CircleShape>().ToList();
        }
    }
}
