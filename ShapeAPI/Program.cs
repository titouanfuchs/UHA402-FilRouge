using back.Swagger;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Services

builder.Services.AddTransient<ShapesService>();

#endregion

#region Database
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString($"database"));
    //options.EnableSensitiveDataLogging();
});

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.DocumentFilter<PathPrefixInsertDocumentFilter>("shapeAPI");

    //c.SchemaFilter<EnumSchemaFilter>();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    var shapeService = scope.ServiceProvider.GetRequiredService<ShapesService>();
    
    await dataContext.Database.MigrateAsync();


    //Modification de l'état initial de l'application
    if (dataContext.ShapesGroups.Count() == 0)
    {
        //var group = shapeService.CreateGroup("Default Group");

        /*var rect = shapeService.CreateShape(
            new CreateShape { 
                Name = "Rectangle",
                Lenght = 10,
                Width = 43.7
            },
            ShapeType.Rectangle
        );

        var rectb = shapeService.CreateShape(
            new CreateShape
            {
                Name="Rectangle 1",
                Lenght = 67,
                Width = 89
            },
            ShapeType.Rectangle
        );

        var circ = shapeService.CreateShape(
            new CreateShape
            {
                Name = "Circle",
                Diameter = 76.4
            },
            ShapeType.Circle
        );

        var tri = shapeService.CreateShape(
            new CreateShape
            {
                Name = "Triangle",
                Base = 10,
                SideOne = 5,
                SideTwo = 6
            },
            ShapeType.Triangle
        );

        shapeService.AddShapeToGroup(group.Id, rect.Id);
        shapeService.AddShapeToGroup(group.Id, rectb.Id);
        shapeService.AddShapeToGroup(group.Id, tri.Id);
        shapeService.AddShapeToGroup(group.Id, circ.Id);*/
    }

}


app.Run();
