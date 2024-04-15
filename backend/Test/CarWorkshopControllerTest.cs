using Microsoft.Identity.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Entities.Authorization;
using Model.Entities.Authorization.Request;
using Model.Entities.CarWorkshop;
using Model.Entities.Client;
using Model.Entities.Constants;
using Model.Repositories;
using Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities;

namespace Test 
{
    [TestClass]
    public class CarWorkshopControllerTest
    {
        private readonly ICarWorkshopService CarWorkshopService;
        private readonly ICarWorkshopRepository CarWorkshopRepository;
            
        public CarWorkshopControllerTest()
        {
            var container = ConfigurationMock.GetContainer();

            CarWorkshopService = container.GetInstance<ICarWorkshopService>();
            CarWorkshopRepository = container.GetInstance<ICarWorkshopRepository>();
        }


        [TestMethod]
        public void SetWorkingHoursTest()
        {
            // Arrange
            var request = new CarWorkshopWorkingHoursCreateArgs()
            {
      workshopId = 1,
        Monday = "08:00-17:00",
        Tuesday = "08:00-17:00",
        Wednesday = "08:00-17:00",
        Thursday  ="08:00-17:00",
        Friday = "08:00-17:00",
        Saturday = "08:00-17:00",
        Sunday = "08:00-17:00",
    
            };

            // Act
            var result = CarWorkshopService.SetWorkingHours(request);

            // Assert
            Assert.IsTrue(result.Success);
            
        }
        [TestMethod]
        public void GetCarWorkshopDetailsTest()
        {
            // Arrange
            var id = 1;

            // Act
            var result = CarWorkshopService.GetCarWorkshopDetails(id);

            // Assert
            Assert.IsTrue(result.Success);
            
        }

        [TestMethod]
        public void GetCarWorkshopWorkingHoursTest()
        {
            // Arrange
            var id = 1;

            // Act
            var result = CarWorkshopService.GetCarWorkshopWorkingHours(id); 

            // Assert
            Assert.IsTrue(result.Success);
            
        }
       
    }
}

    