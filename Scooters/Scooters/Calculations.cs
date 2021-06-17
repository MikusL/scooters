using System;
using System.Collections.Generic;
using Data;

namespace Scooters
{
    public class Calculations : ICalculations
    {
        public IScooterService ScooterService { get; set; }
        private decimal _priceCap;

        public Calculations(decimal priceCap, IScooterService scooterService)
        {
            _priceCap = priceCap;
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
                if (rentedUnit.TotalPrice != 0)
                {
                    result += rentedUnit.TotalPrice;
                }
            }
            else
            {
                if (rentedUnit.TotalPrice != 0)
                {
                    result += rentedUnit.TotalPrice;
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
            var minutesUntilCap = _priceCap / t.PricePerMinute;
            var resetTime = new DateTime(t.StartTime.Year, t.StartTime.Month, t.StartTime.Day+1,0,0,0);
            var tempTime = t.StartTime;
            var totalTimeUsed = (decimal)t.EndTime.Subtract(t.StartTime).TotalMinutes;
            //If time until reset is less than time until priceCap and totalTimeUsed goes over reset time
            if (TimeUntilReset(resetTime,tempTime) < minutesUntilCap &&
                TimeUntilReset(resetTime,tempTime) < totalTimeUsed)
            {
                result += TimeUntilReset(resetTime,tempTime) *
                          t.PricePerMinute;
                totalTimeUsed -= TimeUntilReset(resetTime,tempTime);
                tempTime = resetTime;
                resetTime = resetTime.AddDays(1);
            }
            //if totalTimeUsed is larger than time until reset (goes over reset time)
            while (TimeUntilReset(resetTime,tempTime) < totalTimeUsed)
            {
                result += _priceCap;
                totalTimeUsed -= TimeUntilReset(resetTime,tempTime);
                tempTime = resetTime;
                resetTime = resetTime.AddDays(1);
            }
            //if totalTimeUsed is larger than minutesUntilCap (at this point, it's smaller than reset time, only equal)
            if (totalTimeUsed >= minutesUntilCap)
            {
                result += _priceCap;
            }
            //else it doesn't reach minutesUntilCap and needs to be calculated
            else
            {
                result += totalTimeUsed *
                          t.PricePerMinute;
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
