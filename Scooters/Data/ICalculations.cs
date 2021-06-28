using System.Collections.Generic;

namespace Data
{
    public interface ICalculations
    {
        public IScooterService ScooterService { get; set; }

        decimal CalculateIncome(int? year, bool includeNotCompletedRentals, IList<RentedUnit> rentHistory);

        decimal ToInclude(bool include, RentedUnit rentedUnit);

        decimal CalculatePrice(RentedUnit t);
    }
}
