using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using System;

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
    }
}
