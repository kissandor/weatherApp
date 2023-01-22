using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace weatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String URL = "https://api.weatherapi.com/v1/";
        String ROUTE_CURRENT = "current.json";
        String KEY = "230564d5c26f40c5b8b191255223012";
        string q = ""; //name of the city

        public MainWindow()
        {
            InitializeComponent();
            buttonSearch_Click(new object(), new RoutedEventArgs());
        }


        /*
         * Todo: 
         *       5 days forecast
         *       Day / night background
         *       Locaton
         */
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            q = inputCity.Text;

            Weather weather = getWeather(q);

            cityNameDisplay.Text = weather.location.name + "\n" + weather.location.country;

            temperatureDisplay.Text = weather.current.temp_c.ToString() + " °C";

            DateTime time = weather.location.localtime;


            realFeelingTempDisplay.Text = "Real feeling " + 
                weather.current.feelslike_c.ToString() + " °C \n" +
                "Local time: " + time.ToString("HH:mm");
            
            string image = "https:" + weather.current.condition.icon;
            imgWeather.Source = new BitmapImage(new Uri(image));
            day1.Source= new BitmapImage(new Uri(image));
            day2.Source = new BitmapImage(new Uri(image));
            day3.Source = new BitmapImage(new Uri(image));
            day4.Source = new BitmapImage(new Uri(image));
            day5.Source = new BitmapImage(new Uri(image));
            
            backgroundImage(time.Hour);
        }

        private Weather getWeather(String city)
        {
            String realCity = isCityOk(city) ? city : "Budapest";

            var client = new RestClient(URL);
            string route = "current.json?key=" + KEY + "&q=" + realCity;
            var request = new RestRequest(route, Method.Get);

            RestResponse rsp = client.Execute<List<Weather>>(request);
            string jsonRespond = rsp.Content;
            Weather weather = JsonConvert.DeserializeObject<Weather>(jsonRespond);

            return weather;
        }

        
        //check if the entered city exist.
        private bool isCityOk(String city)
        {
            var client = new RestClient(URL);
            string route = "current.json?key=" + KEY + "&q=" + city;
            var request = new RestRequest(route, Method.Get);
            RestResponse rsp = client.Execute<List<Weather>>(request);
            string Code = rsp.StatusCode.ToString();

            return Code == "OK";
        }

        private void backgroundImage(int hour)
        {
            
            if (hour>19 || hour<7)
            { 
                this.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/img/backgound.PNG")));
                this.Foreground = Brushes.White;
            }
            else
            {
                this.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/img/background_day.PNG")));

                BrushConverter bc = new BrushConverter(); 
                this.Foreground = (Brush)bc.ConvertFrom("#4A3F3D");
            }
        }
    }
}
