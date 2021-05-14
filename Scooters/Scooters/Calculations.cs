using System;
using System.Collections.Generic;
using Data;

namespace Scooters
{
    public class Calculations : ICalculations
    {
        public IScooterService ScooterService { get; set; }
        public decimal PriceCap { get; set; }

        public Calculations(decimal priceCap, IScooterService scooterService)
        {
            PriceCap = priceCap;
            ScooterService = scooterService;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals, IList<RentedUnit> rentHistory)
        {
            decimal result = 0;

            foreach (var rentedUnit in rentHistory)
            {
                if (year == null)
                {
                    result += ToInclude(includeNotCompletedRentals, rentedUnit);
                }
                else
                {
                    if (rentedUnit.StartTime.Year == year)
                    {
                        result += ToInclude(includeNotCompletedRentals, rentedUnit); 
                    }
                } 
            }

            return result;
        }

        public decimal ToInclude(bool include, RentedUnit rentedUnit)
        {
            decimal result = 0;

            if (!include)
            {
                if (rentedUnit.Price != 0)
                {
                    result += rentedUnit.Price;
                }
            }
            else
            {
                if (rentedUnit.Price != 0)
                {
                    result += rentedUnit.Price;
                }
                else
                {
                    result += CalculatePrice(rentedUnit);
                }
            }

            return result;
        }

        public decimal CalculatePrice(RentedUnit t)
        {
            decimal result = 0;
            var minutesUntilCap = PriceCap / ScooterService.GetScooterById(t.ScooterId).PricePerMinute;
            var resetTime = new DateTime(t.StartTime.Year, t.StartTime.Month, t.StartTime.Day+1,0,0,0);
            var tempTime = t.StartTime;
            var totalTimeUsed = (decimal)t.EndTime.Subtract(t.StartTime).TotalMinutes;
            //If time until reset is less than time until priceCap and totalTimeUsed goes over reset time
            if (TimeUntilReset(resetTime,tempTime) < minutesUntilCap &&
                TimeUntilReset(resetTime,tempTime) < totalTimeUsed)
            {
                result += TimeUntilReset(resetTime,tempTime) *
                          ScooterService.GetScooterById(t.ScooterId).PricePerMinute;
                totalTimeUsed -= TimeUntilReset(resetTime,tempTime);
                tempTime = resetTime;
                resetTime = resetTime.AddDays(1);
            }
            //if totalTimeUsed is larger than time until reset (goes over reset time)
            while (TimeUntilReset(resetTime,tempTime) < totalTimeUsed)
            {
                result += PriceCap;
                totalTimeUsed -= TimeUntilReset(resetTime,tempTime);
                tempTime = resetTime;
                resetTime = resetTime.AddDays(1);
            }
            //if totalTimeUsed is larger than minutesUntilCap (at this point, it's smaller than reset time, only equal)
            if (totalTimeUsed >= minutesUntilCap)
            {
                result += PriceCap;
            }
            //else it doesn't reach minutesUntilCap and needs to be calculated
            else
            {
                result += totalTimeUsed *
                          ScooterService.GetScooterById(t.ScooterId).PricePerMinute;
            }

            return Math.Round(result,2);
        }

        private decimal TimeUntilReset(DateTime resetTime, DateTime tempTime)
        {
            var result = (decimal)resetTime.Subtract(tempTime).TotalMinutes;

            return result;
        }
    }
}
