namespace basicForms.Models.Shapes
{
    public class Shape3D
    {
        private BaseShape _Shape;
        public BaseShape Shape { get => _Shape; }
        public BaseShape SetShape { set => _Shape = value; }

        private double _Height = 1;
        public double SetHeight { set => _Height = value; }
        public double Height { get => _Height; }

        public Shape3D(BaseShape shape)
        {
            _Shape = shape;
        }

        public double Volume { get => _Shape.CalculateSurface() * _Height; }
    }
}
