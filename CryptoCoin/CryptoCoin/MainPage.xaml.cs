using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft;
using CryptoCoin.Model;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace CryptoCoin
{
    public partial class MainPage : ContentPage
    {
        private string API = "0E6679CE-2503-4765-BFEC-ADF87BAF68F0";
        private string IMG = "https://s3.eu-central-1.amazonaws.com/bbxt-static-icons/type-id/png_256/";
        public MainPage()
        {
            InitializeComponent();
            cList.ItemsSource = GetCoins();

        }

        private List<Coin> GetCoins()
        {
            List<Coin> coins;

            var client = new RestClient("http://rest.coinapi.io/v1/assets/BTC,ETH,XMR,LTC,DOGE");
            var request = new RestRequest();
            request.AddHeader("X-CoinAPI-Key", API);
            var response = client.Execute(request);

            coins = JsonConvert.DeserializeObject<List<Coin>>(response.Content);

            foreach (var c in coins)
            {
                if (!string.IsNullOrEmpty(c.Id_icon))
                    c.Icon_url = IMG + c.Id_icon.Replace("-", "") + ".png";
                else
                    c.Icon_url = "";
            }
            return coins;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            cList.ItemsSource = GetCoins();
        }
    }
}

