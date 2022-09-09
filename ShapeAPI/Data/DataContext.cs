namespace ShapeAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<BaseShape> Shapes { get; set; }
        public DbSet<ShapeGroup> ShapesGroups { get; set; }
    }
}
