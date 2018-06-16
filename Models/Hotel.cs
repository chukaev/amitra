using System;
using Newtonsoft.Json.Linq;

namespace Models
{
    public class Hotel
    {
        public Hotel(JToken jToken)
        {
            Name = jToken["title"].Value<String>();
            City = jToken["place"].Value<String>();
            //Info = jToken[""]
        }

        public String Name { get; set; }

        public String City { get; set; }

        public String Info { get; set; }
    }
}