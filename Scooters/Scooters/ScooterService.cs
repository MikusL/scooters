using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Exceptions;

namespace Scooters
{
    public class ScooterService : IScooterService
    {
        public IList<Scooter> ScooterList { get; set; }

        public ScooterService()
        {
            ScooterList = new List<Scooter>();
        }

        public void AddScooter(string scooterId, decimal pricePerMinute)
        {
            if (String.IsNullOrWhiteSpace(scooterId))
            {
                throw new InvalidInput("Invalid scooter id.");
            }

            if (pricePerMinute <= 0)
            {
                throw new InvalidInput("Invalid price.");
            }

            ScooterList.Add(new Scooter(scooterId, pricePerMinute));
        }

        public void RemoveScooter(string scooterId)
        {
            var removableScooter = GetScooterById(scooterId);

            if (removableScooter.IsRented)
            {
                throw new ScooterIsRented("The scooter is rented.");
            }

            ScooterList.Remove(removableScooter);
        }

        public IList<Scooter> GetScooters()
        {
            if (ScooterList.Count < 1)
            {
                throw new EmptyHistoryException("The scooter list is empty."); 
            }

            return ScooterList;
        }

        public Scooter GetScooterById(string scooterId)
        {
            if (ScooterList.FirstOrDefault(t => t.Id == scooterId) != null)
            {
                return ScooterList.FirstOrDefault(t => t.Id == scooterId);
            }

            throw new ScooterDoesNotExist("There is no scooter with that id.");
        }
    }
}
