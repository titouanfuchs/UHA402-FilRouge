using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicForms.Models
{
    public class Shape
    {
        private string _Name = "Nouvelle forme";
        
        private double _Lenght = 0;
        private double _Width = 0;

        public Shape(string name, double lenght = 1, double width = 1)
        {
            _Lenght = lenght;
            _Width = width;
            _Name = name;
        }

        public double Surface { get => _Width * _Lenght; }
        public double Width { get => _Width; }
        public double Lenght { get => _Lenght; }
        public string Name { get => _Name; }
    }
}
