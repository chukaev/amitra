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
            Info = HttpUtility.HtmlDecode($"{jToken["room"]["description"].Value<string>()} {jToken["place"]["description"]} {jToken["dateRange"]}. {jToken["meal"]}");
        }

        public string Name { get; set; }

        public string City { get; set; }

        public string Info { get; set; }
    }
}