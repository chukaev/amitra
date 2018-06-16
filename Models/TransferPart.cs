using System;

namespace Models
{
    public class TransferPart
    {
        public String Departure { get; set; }

        public String Arrival { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public DateTime ArrivalDateTime { get; set; }

        public TransferType Type { get; set; }
    }
}