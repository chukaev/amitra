using System;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Models
{
    public class Hotel
    {
        public Hotel(JToken jToken)
        {
            Name = HttpUtility.HtmlDecode(jToken["hotel"]["title"].Value<string>());
            City = HttpUtility.HtmlDecode(jToken["hotel"]["place"].Value<string>());
            Room = HttpUtility.HtmlDecode(jToken["room"]["description"].Value<string>());
            Place = HttpUtility.HtmlDecode(jToken["place"]["description"].Value<string>());
            DateRange = HttpUtility.HtmlDecode(jToken["dateRange"].Value<string>());
            Meal = HttpUtility.HtmlDecode(jToken["meal"].Value<string>());
        }

        public Hotel()
        {
        }

        public string Meal { get; set; }

        public string DateRange { get; set; }

        public string Place { get; set; }

        public string Room { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Info => HttpUtility.HtmlDecode($"{Room} {Place} {DateRange}. {Meal}");
        public int Price { get; set; }
    }
}