// See https://aka.ms/new-console-template for more information
using basicForms.Models;
using basicForms.Models.Shapes;

Console.WriteLine("Basic Shapes - UHA 4.0.2");

Console.WriteLine("");

RectangleShape rectangleTest = new RectangleShape("Test Rectangle", 10.65, 5.87);
rectangleTest.Display();

Console.WriteLine("");

CircleShape circleTest = new CircleShape("Test Cercle", 5.65);
circleTest.Display();

Console.WriteLine("");

TriangleShape triangleTest = new TriangleShape("Test Triangle", false,10, 6, 5);
triangleTest.Display();
