namespace ShapeAPI.Models.DTO
{
    public class BaseResponse
    {
        protected string _Message = "";
        public string Message { get => _Message; }
        public string SetMessage { set => _Message = value; }

        public BaseResponse(string message)
        {
            _Message = message;
        }
    }
}
