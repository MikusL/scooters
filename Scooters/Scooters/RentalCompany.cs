using System;
using System.Collections.Generic;
using Data;
using Exceptions;

namespace Scooters
{
    public class RentalCompany : IRentalCompany
    {
        public IList<RentedUnit> RentHistory { get; set; }
        public IScooterService ScooterService { get; set; }
        public ICalculations Calculations { get; set; }
        public string Name { get; }
        private int _transId;

        public RentalCompany(string name, IScooterService scooterService,ICalculations calculations)
        {
            Name = name;
            ScooterService = scooterService;
            Calculations = calculations;
            RentHistory = new List<RentedUnit>();
            _transId = 0;
        }

        public void StartRent(string scooterId)
        {
            if (ScooterService.GetScooterById(scooterId).IsRented)
            {
                throw new ScooterIsRented("The scooter is being rented.");
            }

            ScooterService.GetScooterById(scooterId).IsRented = true;
            RentHistory.Add(new RentedUnit(_transId++, scooterId, DateTime.Now));
        }

        public void EndRent(string scooterId)
        {
            foreach (var t in RentHistory)
            {
                if (t.StartTime == t.EndTime && t.ScooterId == scooterId)
                {
                    ScooterService.GetScooterById(scooterId).IsRented = false;
                    t.EndTime = DateTime.Now;
                    t.Price = Calculations.CalculatePrice(t);
                    return;
                }
            }

            throw new ScooterIsNotRented("That scooter has not been rented.");
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            if (RentHistory.Count < 1)
            {
                throw new EmptyList("The rent history is empty.");
            }

            var result = Calculations.CalculateIncome(year, includeNotCompletedRentals, RentHistory);

            return result;
        }
    }
}
