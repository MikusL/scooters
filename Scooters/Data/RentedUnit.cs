using System;

namespace Data
{
    public class RentedUnit
    {
        public string ScooterId;
        public decimal TotalPrice;
        public decimal PricePerMinute;
        public DateTime StartTime;
        public DateTime EndTime;
        public int TransId;

        public RentedUnit(int transId, string id, DateTime startTime, decimal pricePerMinute)
        {
            ScooterId = id;
            TotalPrice = 0;
            PricePerMinute = pricePerMinute;
            StartTime = startTime;
            EndTime = startTime;
            TransId = transId;
        }
    }
}
