using BusinessLayer;
using BusinessLayer.DomainModel;
using CommonLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class BusinessLayerTests
    {
        private List<PetOwner> _petOwnerList;

        [TestInitialize]
        public void InitialiseTestData()
        {
            _petOwnerList = new List<PetOwner>
            {
                new PetOwner { Age = 10,
                               Gender = "Male",
                               Name = "Alex",
                               Pets = new List<Pet>
                               {
                                    new Pet { Name = "Molly", Type = BusinessLayer.PetType.Cat }
                               }
                },
                new PetOwner { Age = 12,
                               Gender = "Female",
                               Name = "Kyara",
                               Pets = new List<Pet>
                               {
                                    new Pet { Name = "Thunder", Type = BusinessLayer.PetType.Dog },
                                    new Pet { Name = "Lightning", Type = BusinessLayer.PetType.Dog },
                               }
                },
                  new PetOwner { Age = 8,
                               Gender = "Female",
                               Name = "Grace",
                               Pets = new List<Pet>
                               {
                                    new Pet { Name = "Kitty", Type = BusinessLayer.PetType.Cat }
                               }
                },
            };
        }

        [TestMethod]
        public void ReturnAListOfPetsAndOwners_Successful()
        {
            // Arrange
            var mockHttpClient = new Mock<IHttpClient>();
            var jsonStringObject = JsonConvert.SerializeObject(_petOwnerList);
            var petRetriever = new PetRetriever(mockHttpClient.Object);
            mockHttpClient.Setup(x => x.GetResultsAsync()).Returns(Task.FromResult(jsonStringObject));

            // Act
            var catOwnerListGroupByGender = petRetriever.GetListOfPetsByPetType(PetType.Cat);

            // Assert
            Assert.IsNotNull(catOwnerListGroupByGender);
        }

        [TestMethod]
        public void ReturnAListOfPetsForKyara_Successful()
        {
            // Arrange
            var mockHttpClient = new Mock<IHttpClient>();
            var jsonStringObject = JsonConvert.SerializeObject(_petOwnerList);
            var petRetriever = new PetRetriever(mockHttpClient.Object);
            mockHttpClient.Setup(x => x.GetResultsAsync()).Returns(Task.FromResult(jsonStringObject));

            // Act
            var allPetsAndOwners = petRetriever.GetListOfPetsByPetType(PetType.All).GetPetList();
            var kyaraPets = allPetsAndOwners.FindAll(x => x.OwnerName.ToLower() == "kyara");

            // Assert
            Assert.IsNotNull(kyaraPets);
            Assert.AreEqual(2, kyaraPets.Count);
            Assert.AreEqual("Thunder", kyaraPets[0].PetName);
            Assert.AreEqual("Lightning", kyaraPets[1].PetName);
        }

        [TestMethod]
        public void ReturnAListOfCatsAndOwners_Successful()
        {
            // Arrange
            var mockHttpClient = new Mock<IHttpClient>();
            var jsonStringObject = JsonConvert.SerializeObject(_petOwnerList);
            var petRetriever = new PetRetriever(mockHttpClient.Object);
            mockHttpClient.Setup(x => x.GetResultsAsync()).Returns(Task.FromResult(jsonStringObject));

            // Act
            var catOwnerListGroupByGender = petRetriever.GetListOfPetsByPetType(PetType.Cat)
                                           .GroupByGender();

            // Assert
            Assert.IsNotNull(catOwnerListGroupByGender);
            Assert.AreEqual(2, catOwnerListGroupByGender.Count);
            Assert.AreEqual("Male", catOwnerListGroupByGender[0].OwnerGender);
            Assert.AreEqual("Molly", catOwnerListGroupByGender[0].PetName[0]);
            Assert.AreEqual("Female", catOwnerListGroupByGender[1].OwnerGender);
            Assert.AreEqual("Kitty", catOwnerListGroupByGender[1].PetName[0]);
        }

        [TestMethod]
        public void ReturnAnEmptyListOfPetsAndOwners_ForNonExistingPets()
        {
            // Arrange
            var mockHttpClient = new Mock<IHttpClient>();
            var jsonStringObject = JsonConvert.SerializeObject(_petOwnerList);
            var petRetriever = new PetRetriever(mockHttpClient.Object);
            mockHttpClient.Setup(x => x.GetResultsAsync()).Returns(Task.FromResult(jsonStringObject));

            // Act
            var birdOwnerListGroup = petRetriever.GetListOfPetsByPetType(PetType.Bird).GetPetList();

            // Assert
            Assert.IsNotNull(birdOwnerListGroup);
            Assert.AreEqual(0, birdOwnerListGroup.Count);        
        }
    }
}
