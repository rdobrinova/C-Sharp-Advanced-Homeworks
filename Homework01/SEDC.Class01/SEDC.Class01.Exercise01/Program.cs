using SEDC.Class01.Exercise01.Classes;
using static System.Formats.Asn1.AsnWriter;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Xml.Linq;

//EXERCISE01
//▸ Create 4 classes:
//▹ Pet( abstract ) with Name, Type, Age and abstract PrintInfo()
//▹ Dog(from Pet ) with FavoriteFood
//▹ Cat( from Pet ) with Lazy and LivesLeft
//▹ Fish( from Pet ) with Color, Size
//▸ Create a PetStore generic class with :
//▹ Generic list of pets - Dogs, Cats or Fish depending on what is passed as T
//▹ Generic method printsPets() - Prints Dogs, Cats or Fish depending on what is passed as T
//▹ BuyPet() - Method that takes ‘name’ as parameter, find the first pet by that name, removes
//it from the list and gives a success message.If there is no pet by that name, inform the
//customer
//▸ Create a Dog, Cat and Fish store with 2 pets each
//▹ Buy a dog and a cat from the Dog and Cat store
//▹ Call PrintPets() method on all stores


PetStore<Dog> dogStore = new PetStore<Dog>();
dogStore.AddPet(new Dog { Name = "Bak", Type = "Akita", Age = 5, FavoriteFood = "Chicken" });
dogStore.AddPet(new Dog { Name = "Bert", Type = "Terrier", Age = 3, FavoriteFood = "fish" });


PetStore<Cat> catStore = new PetStore<Cat>();
catStore.AddPet(new Cat { Name = "Luna", Type = "Persian", Age = 4, Lazy = true, LivesLeft = 5});
catStore.AddPet(new Cat { Name = "Paco", Type = "Ragdoll.", Age = 7, Lazy = true, LivesLeft = 3});

PetStore<Fish> fishStore = new PetStore<Fish>();
fishStore.AddPet(new Fish { Name = "Mery", Type = "Saltwater Fish", Age = 9, Color = "Grey", Size = 37 });
fishStore.AddPet(new Fish { Name = "Aba", Type = "Caviar", Age = 14, Color = "Blue and white", Size = 52 });

dogStore.BuyPet("Bert");
catStore.BuyPet("Luna");

dogStore.PrintPets();
catStore.PrintPets();
fishStore.PrintPets();