
using Azure.Storage.Queues;

Console.WriteLine("*** Azure Queue Receiver ***");


var sasConString = "BlobEndpoint=https://mystorageandre.blob.core.windows.net/;QueueEndpoint=https://mystorageandre.queue.core.windows.net/;FileEndpoint=https://mystorageandre.file.core.windows.net/;TableEndpoint=https://mystorageandre.table.core.windows.net/;SharedAccessSignature=sv=2021-06-08&ss=q&srt=sco&sp=rwdlacup&se=2022-06-19T17:03:43Z&st=2022-06-13T09:03:43Z&spr=https&sig=ybCShDEHlP8zOmgFXec7QvAK7et4a67AOXEvSd3b9Vk%3D";

var client = new QueueClient(sasConString, "hallowelt");


while (true)
{
    var msg = client.ReceiveMessage();

    if (msg.Value != null)
    {
        Console.WriteLine($"MSG {msg.Value.MessageId}: {msg.Value.Body}");
        client.DeleteMessage(msg.Value.MessageId, msg.Value.PopReceipt);
    }
    else
    {
        Console.WriteLine("Keine neue MSG");
        Thread.Sleep(1000);
    }
}
