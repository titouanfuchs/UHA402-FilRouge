﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicForms.Models.Shapes
{
    public class BaseShape
    {
        #region Fields
        protected string _Name = "Nouvelle forme";
        public string Name { get => _Name; }
        public string SetName { set => _Name = value; }
        #endregion

        public BaseShape(string name)
        {
            _Name = name;
        }

        #region
        public virtual double Surface()
        {
            throw new NotImplementedException();
        }

        public virtual double Perimeter()
        {
            throw new NotImplementedException();
        }

        public virtual void Display()
        {
            Console.WriteLine($"Nom de la forme : {_Name}");
            Console.WriteLine($"Aire : {Surface()}");
            Console.WriteLine($"Périmètre : {Perimeter()}");
        }
        #endregion
    }
}
