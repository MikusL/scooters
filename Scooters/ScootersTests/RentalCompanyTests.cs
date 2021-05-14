
using System;
using Data;
using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scooters;

namespace ScootersTests
{
    [TestClass]
    public class RentalCompanyTests
    {
        private IRentalCompany _rentalCompany;

        private IRentalCompany GetRentalCompany(string name)
        {
            IScooterService scooterService = new ScooterService();
            ICalculations calculations = new Calculations(20, scooterService);
            _rentalCompany = new RentalCompany(name, scooterService, calculations);

            return _rentalCompany;
        }

        [TestInitialize]
        public void Initialize()
        {
            _rentalCompany = GetRentalCompany("Company");
        }

        [TestMethod]
        public void StartRent_ValidScooter_ReturnsIsRentedTrue()
        {
            // Arrange
            string scooterId = "Scooter";
            // Act
            _rentalCompany.ScooterService.AddScooter(scooterId,1);
            _rentalCompany.StartRent(scooterId);
            // Assert
            Assert.AreEqual(true, _rentalCompany.ScooterService.GetScooterById(scooterId).IsRented);
        }

        [TestMethod]
        public void StartRent_ValidScooter_ReturnsRentHistoryCountIs1()
        {
            // Arrange
            string scooterId = "Scooter";
            int target = 1;
            // Act
            _rentalCompany.ScooterService.AddScooter(scooterId, 1);
            _rentalCompany.StartRent(scooterId);
            // Assert
            Assert.AreEqual(target, _rentalCompany.RentHistory.Count);
        }

        [TestMethod]
        public void StartRent_RentedScooter_ThrowsExceptionScooterIsRented()
        {
            // Arrange
            string scooterId = "Scooter";
            // Act
            _rentalCompany.ScooterService.AddScooter(scooterId, 1);
            _rentalCompany.StartRent(scooterId);
            // Assert
            Assert.ThrowsException<ScooterIsRented>(() => _rentalCompany.StartRent(scooterId));
        }

        [TestMethod]
        public void EndRent_NotRentedScooter_ThrowsExceptionScooterIsNotRented()
        {
            // Arrange
            string scooterId = "Scooter";
            // Act
            _rentalCompany.ScooterService.AddScooter(scooterId ,1);
            _rentalCompany.StartRent(scooterId);
            _rentalCompany.EndRent(scooterId);
            // Assert
            Assert.ThrowsException<ScooterIsNotRented>(() => _rentalCompany.EndRent(scooterId));
        }

        [TestMethod]
        public void EndRent_ValidScooter_ScooterIsRentedReturnsFalse()
        {
            // Arrange
            string scooterId = "Scooter";
            // Act
            _rentalCompany.ScooterService.AddScooter(scooterId,1);
            _rentalCompany.StartRent(scooterId);
            _rentalCompany.EndRent(scooterId);
            // Assert
            Assert.AreEqual(false, _rentalCompany.ScooterService.GetScooterById(scooterId).IsRented);
        }

        [TestMethod]
        public void CalculateIncome_EmptyRentHistory_ThrowsExceptionEmptyList()
        {
            // Assert
            Assert.ThrowsException<EmptyList>(() => _rentalCompany.CalculateIncome(null,true));
        }

        [TestMethod]
        public void CalculateIncomeTest()
        {
            // Arrange
            string scooterId = "Scooter";
            decimal target = 20;
            // Act
            _rentalCompany.ScooterService.AddScooter(scooterId,1);
            _rentalCompany.StartRent(scooterId);
            _rentalCompany.EndRent(scooterId);
            _rentalCompany.RentHistory[0].StartTime = DateTime.Now.AddHours(-1);
            _rentalCompany.RentHistory[0].Price = 0;
            _rentalCompany.RentHistory[0].Price =
                _rentalCompany.Calculations.CalculatePrice(_rentalCompany.RentHistory[0]);
            // Assert
            Assert.AreEqual(target, _rentalCompany.CalculateIncome(null,true));
        }

        [TestMethod]
        public void NamePropertyTest()
        {
            // Arrange
            string companyName = "Company";
            // Assert
            Assert.AreEqual(companyName, _rentalCompany.Name);
        }
    }
}
