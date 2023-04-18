using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Class01.Exercise01.Classes
{
    public class Fish : Pet
    {
        public string Color { get; set; }
        public int Size { get; set; }
        public override void PrintInfo()
        {
            Console.WriteLine($"Name: {Name}, Type: {Type}, Age: {Age}, Color: {Color}, Size: {Size}");
        }
    }
}
