using System;
using System.Collections.Generic;
using System.Linq;
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
        private static readonly HttpClient Client = new HttpClient();

        public Tour Tour { get; private set; } = new Tour { TourName = "Жопа" };

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Ring.IsActive = true;
            FindingLabel.Visibility = Visibility.Visible;
            ResultsTab.Visibility = Visibility.Hidden;
            ComparisonGrid.Visibility = Visibility.Hidden;
            ParsingSwitch();
        }

        private void ParsingSwitch()
        {
            try
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
            catch (Exception)
            {
                Ring.IsActive = false;
                FindingLabel.Visibility = Visibility.Hidden;
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

            var response = await Client.PostAsync("https://tours.tutu.ru/api/get_detailed_order/", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var json = (JObject) JsonConvert.DeserializeObject(responseString);
            
            Tour = new Tour
                {
                TourName = json["orderAccommodation"]["tourName"].Value<string>(),
                Price = json["orderPrice"]["price"]["priceRub"].Value<int>(),
                Hotel = new Hotel(json["orderAccommodation"]["mainResidence"]),
                Transfer = new Transfer(json["orderTransports"])
            };

            SourceTourCity.Content = Tour.Hotel.City;
            SourceTourHotel.Content = Tour.Hotel.Name;
            SourceTourHotelDescription.Text = Tour.Hotel.Info;
            SourceTourName.Content = Tour.TourName;
            SourceTourPrice.Content = Tour.Price + " ₽";
            //var tourTravelPoints = Tour.Transfer.Outward.Where(x => x.Departure != null && x.DepartureDateTime != null).Select(x=> new {Place = x.Departure, Time = x.DepartureDateTime.Value}).ToList();

            //tourTravelPoints.AddRange(Tour.Transfer.Outward.Where(x=>x.Arrival != null && x.ArrivalDateTime != null).Select(x => new {Place = x.Arrival, Time = x.ArrivalDateTime.Value}).ToList());
            //tourTravelPoints.AddRange(Tour.Transfer.Return.Where(x => x.Departure != null && x.DepartureDateTime != null).Select(x => new { Place = x.Departure, Time = x.DepartureDateTime.Value}).ToList());
            //tourTravelPoints.AddRange(Tour.Transfer.Return.Where(x => x.Arrival != null && x.ArrivalDateTime != null).Select(x => new { Place = x.Arrival, Time = x.ArrivalDateTime.Value}).ToList());

            //var tourTravelPointsString = tourTravelPoints.OrderBy(x => x.Time).Select(x=>x.Place + " " + x.Time.ToString("dd MMM HH mm"));
            //Tour.Transfer 
            //SourceTourTravelPoints.ItemsSource = tourTravelPointsString;

            SourceTourTravelPoints.ItemsSource = Tour.Transfer.AllTransportationsBlocks;
            var transferOptions = new List<Transfer>();

            for (int i = 0; i < 12; i++)
            {
                transferOptions.Add(new Transfer
                {
                    Outward = new List<TransportationsBlock>
                    {
                        new TransportationsBlock
                        {
                            Departure = Tour.Transfer.Outward[0].Departure,
                            DepartureDateTime = Tour.Transfer.Outward[0].DepartureDateTime?.AddHours(-i),
                        }
                    },
                    Return = new List<TransportationsBlock>
                    {
                        new TransportationsBlock
                        {
                            Departure = Tour.Transfer.Return[0].Departure,
                            DepartureDateTime = Tour.Transfer.Return[0].DepartureDateTime?.AddHours(-i),
                        }
                    },
                    Price = 12042 + i * 83
                });
            }

            TransportOptionsListView.ItemsSource = transferOptions;

            Ring.IsActive = false;
            FindingLabel.Visibility = Visibility.Hidden;
            ResultsTab.Visibility = Visibility.Visible;
            ComparisonGrid.Visibility = Visibility.Visible;
        }
    }
}
