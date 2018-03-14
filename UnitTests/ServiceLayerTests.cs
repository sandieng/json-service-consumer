using BusinessLayer;
using BusinessLayer.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServiceLayer;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class ServiceLayerTests
    {    
        [TestMethod]
        public void CallAGLService_And_GetTheListOfJsonObjects()
        {
            // Arrange
            var aglService = new PetService("http://agl-developer-test.azurewebsites.net/people.json");

            // Act
            var jsonObjects = aglService.GetResultsAsync();
            var result = jsonObjects.Result;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void CallInvalidAGLService_And_GetAnException()
        {
            // Arrange
            var aglService = new PetService("http://agl-developer-test-101.azurewebsites.net/people.json");

            // Act
            var jsonObjects = aglService.GetResultsAsync();
            var result = jsonObjects.Result;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void AGLService_MustContain_6PetOwners()
        {
            // Arrange
            var petService = new PetService("http://agl-developer-test.azurewebsites.net/people.json");

            // Act
            var jsonObjects = petService.GetResultsAsync();           
            List<PetOwner> ownerToPetList = JsonConvert.DeserializeObject<List<PetOwner>>(jsonObjects.Result);

            // Assert
            Assert.IsNotNull(ownerToPetList);
            Assert.AreEqual(6, ownerToPetList.Count);                
        }

        [TestMethod]
        public void Steve_DoesntHave_AnyPets()
        {
            // Arrange
            var petService = new PetService("http://agl-developer-test.azurewebsites.net/people.json");

            // Act
            var jsonObjects = petService.GetResultsAsync();
            List<PetOwner> ownerToPetList = JsonConvert.DeserializeObject<List<PetOwner>>(jsonObjects.Result);
            var stevePets = ownerToPetList.Find(x => x.Name.ToLower() == "steve");

            // Assert
            Assert.IsNotNull(ownerToPetList);
            Assert.IsNull(stevePets.Pets);
        }

        [TestMethod]
        public void Fred_Has_3Cats_And_1Dog()
        {
            // Arrange
            var petService = new PetService("http://agl-developer-test.azurewebsites.net/people.json");

            // Act
            var jsonObjects = petService.GetResultsAsync();
            List<PetOwner> ownerToPetList = JsonConvert.DeserializeObject<List<PetOwner>>(jsonObjects.Result);
            var fredPets = ownerToPetList.Find(x => x.Name.ToLower() == "fred");
            var fredCats = fredPets.Pets.FindAll(x => x.Type == PetType.Cat);
            var fredDogs = fredPets.Pets.FindAll(x => x.Type == PetType.Dog);

            // Assert
            Assert.IsNotNull(ownerToPetList);
            Assert.IsNotNull(fredPets.Pets);
            Assert.AreEqual(3, fredCats.Count);
            Assert.AreEqual(1, fredDogs.Count);
        }
    }
}
