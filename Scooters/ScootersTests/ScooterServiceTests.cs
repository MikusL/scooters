using System.Collections;
using System.Collections.Generic;
using Data;
using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scooters;

namespace ScootersTests
{
    [TestClass]
    public class ScooterServiceTests
    {
        private IScooterService _scooterService;

        [TestInitialize]
        public void Initialize()
        {
            _scooterService = new ScooterService();
        }

        [TestMethod]
        public void AddScooter_InvalidName1_ThrowsExceptionInvalidInput()
        {
            // Assert
            Assert.ThrowsException<InvalidInput>(() => _scooterService.AddScooter(null, 1));
        }

        [TestMethod]
        public void AddScooter_InvalidName2_ThrowsExceptionInvalidInput()
        {
            // Assert
            Assert.ThrowsException<InvalidInput>(() => _scooterService.AddScooter("", 1));
        }

        [TestMethod]
        public void AddScooter_InvalidName3_ThrowsExceptionInvalidInput()
        {
            // Assert
            Assert.ThrowsException<InvalidInput>(() => _scooterService.AddScooter(" ", 1));
        }

        [TestMethod]
        public void AddScooter_InvalidPrice1_ThrowsExceptionInvalidInput()
        {
            // Assert
            Assert.ThrowsException<InvalidInput>(() => _scooterService.AddScooter("Scooter", 0));
        }

        [TestMethod]
        public void AddScooter_InvalidPrice2_ThrowsExceptionInvalidInput()
        {
            // Assert
            Assert.ThrowsException<InvalidInput>(() => _scooterService.AddScooter("Scooter", -1));
        }

        [TestMethod]
        public void AddScooter_CorrectParameters_ScooterListCountEquals1()
        {
            // Arrange
            var target = 1;
            // Act
            _scooterService.AddScooter("Scooter",0.5m);
            // Assert
            Assert.AreEqual(target, _scooterService.ScooterList.Count);
        }

        [TestMethod]
        public void RemoveScooter_RentedScooter_ThrowsExceptionScooterIsRented()
        {
            // Arrange
            string scooterId = "Scooter";
            // Act
            _scooterService.ScooterList.Add(new Scooter(scooterId, 0.5m));
            _scooterService.GetScooterById(scooterId).IsRented = true;
            // Assert
            Assert.ThrowsException<ScooterIsRented>(() => _scooterService.RemoveScooter(scooterId));
        }

        [TestMethod]
        public void RemoveScooter_ValidScooter_ScooterListCountDecresesBy1()
        {
            // Arrange
            string scooterId = "Scooter";
            // Act
            _scooterService.ScooterList.Add(new Scooter(scooterId, 0.5m));
            var target = _scooterService.ScooterList.Count - 1;
            _scooterService.RemoveScooter(scooterId);
            // Assert
            Assert.AreEqual(target, _scooterService.ScooterList.Count);
        }

        [TestMethod]
        public void GetScooters_EmptyList_ThrowsExceptionEmptyList()
        {
            // Assert
            Assert.ThrowsException<EmptyHistoryException>(() => _scooterService.GetScooters());
        }

        [TestMethod]
        public void GetScooters_ListOf1Scooter_ReturnsListWithCountOf1()
        {
            // Arrange
            string scooterId = "Scooter";
            decimal price = 1;
            Scooter scooter = new Scooter(scooterId, price);
            IList<Scooter> target = new List<Scooter> {scooter};
            // Act
            _scooterService.ScooterList.Add(scooter);
            // Assert
            CollectionAssert.AreEquivalent((ICollection) target, (ICollection) _scooterService.GetScooters());
        }

        [TestMethod]
        public void GetScooterById_InvalidID_ThrowsExceptionScooterDoesNotExist()
        {
            // Assert
            Assert.ThrowsException<ScooterDoesNotExist>(() => _scooterService.GetScooterById("Invalid"));
        }
    }
}
