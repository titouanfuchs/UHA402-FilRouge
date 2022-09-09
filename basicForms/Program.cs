// See https://aka.ms/new-console-template for more information
using basicForms.Models;
using basicForms.Models.Shapes;

Console.WriteLine("Basic Shapes - UHA 4.0.2");

int ShapeCount = 20;
double total = 0;
Random rnd = new Random();

Console.WriteLine("");

ShapeGroup shapes = new ShapeGroup("Groupe de formes");
Shape3DGroup shapes3D = new Shape3DGroup("Groupe de formes 3D");

for(int i = 0; i < ShapeCount; i++)
{
    int rand = rnd.Next(0, 3);

    switch (rand)
    {
        case 0:
            shapes.AddShape(new RectangleShape($"Rectangle_{Guid.NewGuid()}", rnd.Next(1, 1000), rnd.Next(1, 1000)));
            break;
        case 1:
            shapes.AddShape(new CircleShape($"Circle_{Guid.NewGuid()}", rnd.Next(1, 1000)));
            break;
        case 2:
            shapes.AddShape(new TriangleShape($"Triangle_{Guid.NewGuid()}", rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000)));
            break;
    }
}

for (int i = 0; i < ShapeCount; i++)
{
    int rand = rnd.Next(0, 3);

    switch (rand)
    {
        case 0:
            shapes3D.AddShape(new Shape3D(new RectangleShape($"Rectangle3D_{Guid.NewGuid()}", rnd.Next(1, 1000), rnd.Next(1, 1000)), rnd.Next(1, 1000)));
            break;
        case 1:
            shapes3D.AddShape(new Shape3D(new CircleShape($"Circle_{Guid.NewGuid()}", rnd.Next(1, 1000)), rnd.Next(1, 1000)));
            break;
        case 2:
            shapes3D.AddShape(new Shape3D(new TriangleShape($"Triangle_{Guid.NewGuid()}", rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000)), rnd.Next(1, 1000)));
            break;
    }
}

shapes.Display();

Console.WriteLine("");

shapes3D.Display();