// See https://aka.ms/new-console-template for more information
using basicForms.Models;
using basicForms.Models.Shapes;

Console.WriteLine("Basic Shapes - UHA 4.0.2");

int ShapeCount = 100;
double total = 0;
Random rnd = new Random();

Console.WriteLine("");

ShapeGroup shapes = new ShapeGroup("Mon super groupe");

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

shapes.Display();