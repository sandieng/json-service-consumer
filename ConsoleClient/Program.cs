using BusinessLayer;
using ServiceLayer;
using System;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new PetService("http://agl-developer-test.azurewebsites.net/people.json");
            IPet pet = new PetRetriever(service);

            // Use fluent API to chain the actions until we get the grouping of pets by the owner's gender
            var result = pet.GetListOfPetsByPetType(PetType.Cat)
                            .GroupByGender();          

            // Print out the groups
            foreach (var group in result)
            {
                Console.WriteLine(group.OwnerGender);
                foreach (var groupPetName in group.PetName)
                {
                    Console.WriteLine($"\t{groupPetName}");
                }
            }

            Console.ReadKey();
        }
    }
}
