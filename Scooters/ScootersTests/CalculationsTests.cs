using System;
using System.Collections.Generic;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scooters;

namespace ScootersTests
{
    [TestClass]
    public class CalculationsTests
    {
        private ICalculations _calculations;

        private ICalculations GetCalculations()
        {
            IScooterService scooterService = new ScooterService();
            _calculations = new Calculations(20, scooterService);

            return _calculations;
        }

        [TestInitialize]
        public void Initialize()
        {
            _calculations = GetCalculations();
        }

        [TestMethod]
        public void CalculateIncome_YearIsNullAndDoNotInclude_Returns35()
        {
            // Arrange
            decimal target = 15 + 20;
            string firstId = "ScooterOne";
            string secondId = "ScooterTwo";
            decimal price = 0.5m;
            IList<RentedUnit> rentHistory = new List<RentedUnit>
            {
                new RentedUnit(1,firstId,DateTime.Now,price), 
                new RentedUnit(2,secondId,DateTime.Now,price)
            };
            // Act
            _calculations.ScooterService.AddScooter(firstId,price);
            _calculations.ScooterService.AddScooter(secondId,price);
            _calculations.ScooterService.GetScooterById(secondId).IsRented = true;
            rentHistory[0].StartTime = new DateTime(2021, 5, 12, 23, 30, 0);
            rentHistory[0].EndTime = new DateTime(2021, 5, 13, 1, 0, 0);
            rentHistory[0].TotalPrice = _calculations.CalculatePrice(rentHistory[0]);
            // Assert
            Assert.AreEqual(target, _calculations.CalculateIncome(null,false,rentHistory));
        }

        [TestMethod]
        public void CalculateIncome_YearIsGivenAndDoNotInclude_Returns45()
        {
            // Arrange
            decimal target = 10 + 20 + 15;
            string firstId = "ScooterOne";
            string secondId = "ScooterTwo";
            decimal price = 0.5m;
            IList<RentedUnit> rentHistory = new List<RentedUnit>
            {
                new RentedUnit(1,firstId,DateTime.Now.AddYears(-1),price),
                new RentedUnit(2,firstId,DateTime.Now,price),
                new RentedUnit(3,secondId,DateTime.Now,price)
            };
            // Act
            _calculations.ScooterService.AddScooter(firstId,price);
            _calculations.ScooterService.AddScooter(secondId,price);
            _calculations.ScooterService.GetScooterById(secondId).IsRented = true;
            rentHistory[1].StartTime = new DateTime(2021, 5, 12, 23, 40, 0);
            rentHistory[1].EndTime = new DateTime(2021, 5, 14, 0, 30, 0);
            rentHistory[1].TotalPrice = _calculations.CalculatePrice(rentHistory[1]);
            rentHistory[0].TotalPrice = 1000;
            // Assert
            Assert.AreEqual(target, _calculations.CalculateIncome(2021,false,rentHistory));
        }

        [TestMethod]
        public void CalculateIncome_YearIsNullAndInclude()
        {
            // Arrange
            decimal target = 15 + (15 + 20 + 20) + 10;
            string firstId = "ScooterOne";
            string secondId = "ScooterTwo";
            decimal price = 0.5m;
            IList<RentedUnit> rentHistory = new List<RentedUnit>
            {
                new RentedUnit(1,firstId,DateTime.Now,price),
                new RentedUnit(2,firstId,DateTime.Now,price),
                new RentedUnit(3,secondId,DateTime.Now,price)
            };
            // Act
            _calculations.ScooterService.AddScooter(firstId, price);
            _calculations.ScooterService.AddScooter(secondId, price);
            _calculations.ScooterService.GetScooterById(secondId).IsRented = true;
            rentHistory[0].StartTime = new DateTime(2020, 5, 12, 15, 0, 0);
            rentHistory[0].EndTime = new DateTime(2020, 5, 12, 15, 30, 0);
            rentHistory[0].TotalPrice = _calculations.CalculatePrice(rentHistory[0]);
            rentHistory[1].StartTime = new DateTime(2021, 5, 12, 23, 30, 0);
            rentHistory[1].EndTime = new DateTime(2021, 5, 14, 15, 0, 0);
            rentHistory[1].TotalPrice = _calculations.CalculatePrice(rentHistory[1]);
            rentHistory[2].StartTime = DateTime.Now.AddMinutes(-20);
            // Assert
            Assert.AreEqual(target, _calculations.CalculateIncome(null,true,rentHistory));
        }

        [TestMethod]
        public void CalculateIncome_YearIsGivenAndInclude()
        {
            // Arrange
            decimal target = (15 + 20 + 20) + 10;
            string firstId = "ScooterOne";
            string secondId = "ScooterTwo";
            decimal price = 0.5m;
            IList<RentedUnit> rentHistory = new List<RentedUnit>
            {
                new RentedUnit(1,firstId,DateTime.Now,price),
                new RentedUnit(2,firstId,DateTime.Now,price),
                new RentedUnit(3,secondId,DateTime.Now,price)
            };
            // Act
            _calculations.ScooterService.AddScooter(firstId, 0.5m);
            _calculations.ScooterService.AddScooter(secondId, 0.5m);
            _calculations.ScooterService.GetScooterById(secondId).IsRented = true;
            rentHistory[0].StartTime = new DateTime(2020, 5, 12, 15, 0, 0);
            rentHistory[0].EndTime = new DateTime(2020, 5, 12, 15, 30, 0);
            rentHistory[0].TotalPrice = _calculations.CalculatePrice(rentHistory[0]);
            rentHistory[1].StartTime = new DateTime(2021, 5, 12, 23, 30, 0);
            rentHistory[1].EndTime = new DateTime(2021, 5, 14, 15, 0, 0);
            rentHistory[1].TotalPrice = _calculations.CalculatePrice(rentHistory[1]);
            rentHistory[2].StartTime = DateTime.Now.AddMinutes(-20);
            // Assert
            Assert.AreEqual(target, _calculations.CalculateIncome(2021, true, rentHistory));
        }
    }
}
