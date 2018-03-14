using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;

namespace UnitTests
{
    [TestClass]
    public class IntegrationTests
    {    
        [TestMethod]
        public void CallAGLService_And_GetTheListOfJsonObjects()
        {
            // Arrange
            var petService = new PetService("http://agl-developer-test.azurewebsites.net/people.json");

            // Act
            var jsonObjects = petService.GetResultsAsync();
            var result = jsonObjects.Result;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CallAGLService_And_ReturnAListOfCatsAndTheirOwners()
        {
            // Arrange
            var petService = new PetService("http://agl-developer-test.azurewebsites.net/people.json");
            var petRetriever = new PetRetriever(petService);

            // Act          
            var catOwners = petRetriever.GetListOfPetsByPetType(PetType.Cat);
            var numberOfCats = catOwners.Count();

            // Assert
            Assert.IsNotNull(catOwners);
            Assert.AreEqual(7, numberOfCats);
        }

        [TestMethod]
        public void CallAGLService_And_ReturnAListOfFishAndTheirOwners()
        {
            // Arrange
            var petService = new PetService("http://agl-developer-test.azurewebsites.net/people.json");
            var petRetriever = new PetRetriever(petService);

            // Act          
            var fishOwners = petRetriever.GetListOfPetsByPetType(PetType.Fish);
            var numberOfFish = fishOwners.Count();

            // Assert
            Assert.IsNotNull(fishOwners);
            Assert.AreEqual(1, numberOfFish);
        }

        [TestMethod]
        public void CallAGLService_And_ReturnAListOfDogshAndTheirOwners()
        {
            // Arrange
            var petService = new PetService("http://agl-developer-test.azurewebsites.net/people.json");
            var petRetriever = new PetRetriever(petService);

            // Act          
            var dogOwners = petRetriever.GetListOfPetsByPetType(PetType.Dog);
            var numberOfDogs = dogOwners.Count();

            // Assert
            Assert.IsNotNull(dogOwners);
            Assert.AreEqual(2, numberOfDogs);
        }

        [TestMethod]
        public void CallAGLService_And_ReturnAnEmptyListForBirdOwners()
        {
            // Arrange
            var petService = new PetService("http://agl-developer-test.azurewebsites.net/people.json");
            var petRetriever = new PetRetriever(petService);

            // Act          
            var birdOwners = petRetriever.GetListOfPetsByPetType(PetType.Bird);
            var numberOfBirds = birdOwners.Count();

            // Assert
            Assert.IsNotNull(birdOwners);
            Assert.AreEqual(0, numberOfBirds);
        }
    }
}
