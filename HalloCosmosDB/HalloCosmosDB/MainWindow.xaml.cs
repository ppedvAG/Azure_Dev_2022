using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace HalloCosmosDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string uri = "https://cosmoswilma.documents.azure.com:443/";
        private string key = "zDOBDE3ZojQGWk83GDIevtDCrFlcRdyyC7iDYk36b0yUe3cEMrpQYREqt3DKohKRBTjcdDQXQk8IKAA5WobHRg==";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Load(object sender, RoutedEventArgs e)
        {
            using var client = new CosmosClient(uri, key);
            var db = client.GetDatabase("PizzaBlitz");
            var container = db.GetContainer("Pizza");

            var feed = container.GetItemLinqQueryable<Pizza>().ToFeedIterator();

            var list = new ObservableCollection<Pizza>();
            myGrid.ItemsSource = list;

            while (feed.HasMoreResults)
            {
                foreach (var item in await feed.ReadNextAsync())
                {
                    list.Add(item);
                }
            }

        }

        private async void New(object sender, RoutedEventArgs e)
        {
            using var client = new CosmosClient(uri, key);
            var db = client.GetDatabase("PizzaBlitz");
            var container = db.GetContainer("Pizza");

            var newPizza = new Pizza()
            {
                Id = Guid.NewGuid().ToString(),
                Name = $"Neu_{Guid.NewGuid().ToString().Substring(0, 4)}",
                //Vegetarisch = true

            };

            await container.CreateItemAsync(newPizza);

        }

        private void Save(object sender, RoutedEventArgs e)
        {

        }
    }
}
