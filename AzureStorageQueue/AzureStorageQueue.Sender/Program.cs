using Azure.Storage.Queues;
using AzureStorageQueue.Model;
using Bogus;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzureStorageQueue.Sender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Queue Sender ***");

            var sasConString = "BlobEndpoint=https://mystorageandre.blob.core.windows.net/;QueueEndpoint=https://mystorageandre.queue.core.windows.net/;FileEndpoint=https://mystorageandre.file.core.windows.net/;TableEndpoint=https://mystorageandre.table.core.windows.net/;SharedAccessSignature=sv=2021-06-08&ss=q&srt=sco&sp=rwdlacup&se=2022-06-19T17:03:43Z&st=2022-06-13T09:03:43Z&spr=https&sig=ybCShDEHlP8zOmgFXec7QvAK7et4a67AOXEvSd3b9Vk%3D";

            var client = new QueueClient(sasConString, "hallowelt");
            var pizzaClient = new QueueClient(sasConString, "pizza");

            var beläge = new[] { "Käse", "Salami", "Mais", "Ananas", "Schinken", "Oliven", "Fisch" };
            var faker = new Faker<Pizza>();
            faker.RuleFor(x => x.KCal, x => x.Random.Int(100, 5000));
            faker.RuleFor(x => x.Name, x => $"{x.Commerce.ProductMaterial()} {x.Commerce.ProductName()}");
            faker.RuleFor(x => x.Preis, x => x.Random.Decimal(5, 19));
            faker.RuleFor(x => x.Belag, x => x.PickRandom(beläge, 3));

            int pCount = 0;
            while (true)
            {
                string messageText = $"Es ist nun {DateTime.Now:G}:{DateTime.Now:ffff}";
                var resp = client.SendMessage(messageText);
                Console.WriteLine($"MSG {resp.Value.MessageId} {messageText} wurde versendet");

                var p = faker.Generate();
                p.Id = pCount++;
                pizzaClient.SendMessage(JsonSerializer.Serialize<Pizza>(p));

            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}