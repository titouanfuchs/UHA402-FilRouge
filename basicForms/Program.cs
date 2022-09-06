// See https://aka.ms/new-console-template for more information
using basicForms.Models;
using basicForms.Models.Shapes;

Console.WriteLine("Basic Shapes - UHA 4.0.2");

RectangleShape rectangleTest = new RectangleShape("Test Rectangle", 10.65, 5.87);
rectangleTest.Display();