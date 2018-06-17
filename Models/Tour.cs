namespace Models
{
    public class Tour
    {
        public string TourName { get; set; }

        public Transfer Transfer { get; set; }

        public Hotel Hotel { get; set; }

        public int Price { get; set; }
    }
}
