using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Class01.Exercise01.Classes
{
    public class Cat : Pet
    {
        public bool Lazy { get; set; }
        public int LivesLeft { get; set; }
        public override void PrintInfo()
        {
            Console.WriteLine($"Name: {Name}, Type: {Type}, Age: {Age}, Lazy: {Lazy}, Lives Left: {LivesLeft}");
        }
    }
}
