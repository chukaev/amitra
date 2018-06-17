using System;

namespace Models
{
    public class TransportationsBlock
    {
        public string Departure { get; set; }

        public string Arrival { get; set; }

        public DateTime? DepartureDateTime { get; set; }

        public DateTime? ArrivalDateTime { get; set; }
    }
}