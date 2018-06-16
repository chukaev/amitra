using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AminaTravel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private static readonly HttpClient client = new HttpClient();
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ParsingSwitch();
            Ring.IsActive = true;
            FindingLabel.Visibility = Visibility.Visible;
            ResultsTab.Visibility = Visibility.Hidden;
            ComparisonGrid.Visibility = Visibility.Hidden;
        }

        private void ParsingSwitch()
        {
            var host = new Uri(TourAdressTextBox.Text).Host;
            switch (host)
            {
                case "tours.tutu.ru":

                    var match = new Regex("order/(.*)/").Match(TourAdressTextBox.Text);
                    if (match.Success)
                    
                        GetDataFromTutuRu(match.Groups[1].Value);
                    
                    break;
                default:
                    Ring.IsActive = false;
                    FindingLabel.Visibility = Visibility.Hidden;
                    ResultsTab.Visibility = Visibility.Visible;
                    ComparisonGrid.Visibility = Visibility.Visible;
                    break;
            }
        }

        private async void GetDataFromTutuRu(string order)
        {
            var values = new Dictionary<string, string>
            {
                { "order", order },
                { "force", "false" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://tours.tutu.ru/api/get_detailed_order/", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var json = (JObject) JsonConvert.DeserializeObject(responseString);

            var tour = new Tour
            {
                Price = json["orderPrice"]["price"]["priceRub"].Value<int>(),
                Hotel = new Hotel(json["orderAccommodation"]["residences"][0]["hotel"]),
                Transfer = new Transfer(json["orderTransports"])
            };
        }
    }
}
