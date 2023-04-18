using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Class01.Exercise01.Classes
{
    public class Dog : Pet
    {
        public string FavoriteFood { get; set; }    

        public override void PrintInfo()
        {
            Console.WriteLine($"Name: {Name}, Type: {Type}, Age: {Age}, Favorite Food: {FavoriteFood}");
        }
    }
}
