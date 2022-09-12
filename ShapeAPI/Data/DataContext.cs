namespace ShapeAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<TriangleShape> TriangleShapes { get; set; }
        public DbSet<RectangleShape> RectangleShapes { get; set; }
        public DbSet<CircleShape> CircleShapes { get; set; }
        public DbSet<ShapeGroup> ShapesGroups { get; set; }
    }
}
