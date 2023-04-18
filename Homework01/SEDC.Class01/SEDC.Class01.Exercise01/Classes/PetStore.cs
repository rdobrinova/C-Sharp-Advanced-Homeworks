using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Class01.Exercise01.Classes
{
    public class PetStore<T> where T : Pet
    {
        private List<T> pets;

        public PetStore()
        {
            pets = new List<T>();
        }

        public void AddPet(T pet)
        {
            pets.Add(pet);
        }

        public void PrintPets()
        {
            Console.WriteLine($"Printing {typeof(T).Name}s:");

            foreach (T pet in pets)
            {
                pet.PrintInfo();
            }
        }

        public void BuyPet(string name)
        {
            T petToRemove = pets.Find(p => p.Name == name);

            if (petToRemove != null)
            {
                pets.Remove(petToRemove);
                Console.WriteLine($"You have bought {petToRemove.Name} the {petToRemove.Type}!");
            }
            else
            {
                Console.WriteLine($"Sorry, there is no {typeof(T).Name} with the name {name} in stock.");
            }
        }
    }
}
