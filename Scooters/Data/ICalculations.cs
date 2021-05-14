using System.Collections.Generic;

namespace Data
{
    public interface ICalculations
    {
        IScooterService ScooterService { get; set; }
        public decimal PriceCap { get; set; }

        decimal CalculateIncome(int? year, bool includeNotCompletedRentals, IList<RentedUnit> rentHistory);

        decimal ToInclude(bool include, RentedUnit rentedUnit);

        decimal CalculatePrice(RentedUnit t);
    }
}
