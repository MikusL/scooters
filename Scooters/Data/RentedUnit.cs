using System;

namespace Data
{
    public class RentedUnit
    {
        public string ScooterId;
        public decimal Price;
        public DateTime StartTime;
        public DateTime EndTime;
        public int TransId;

        public RentedUnit(int transId, string id, DateTime startTime)
        {
            ScooterId = id;
            Price = 0;
            StartTime = startTime;
            EndTime = startTime;
            TransId = transId;
        }
    }
}
