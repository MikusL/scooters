﻿using System.Collections.Generic;

namespace Data
{
    public interface IRentalCompany
    {
        IList<RentedUnit> RentHistory { get; set; }
        private static int TransId { get; set; }
        IScooterService ScooterService { get; set; }
        ICalculations Calculations { get; set; }

        /// <summary>
        /// Name of the company.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Start the rent of the scooter.
        /// </summary>
        /// <param name="scooterId">ID of the scooter</param>
        void StartRent(string scooterId);

        /// <summary>
        /// End the rent of the scooter.
        /// </summary>
        /// <param name="scooterId">ID of the scooter</param>
        /// <returns>The total price of rental. It has to calculated taking into account for how long time scooter was rented.
        /// If total amount per day reaches 20 EUR than timer must be stopped and restarted at beginning of next day at 0:00 am.</returns>
        void EndRent(string scooterId);

        /// <summary>
        /// Income report.
        /// </summary>
        /// <param name="year">Year of the report. Sum all years if value is not set.</param>
        /// <param name="includeNotCompletedRentals">Include income from the scooters that are rented out (rental has not ended yet) and
        /// calculate rental price as if the rental would end at the time when this report was requested.</param>
        /// <returns>The total price of all rentals filtered by year if given.</returns>
        decimal CalculateIncome(int? year, bool includeNotCompletedRentals);
    }
}