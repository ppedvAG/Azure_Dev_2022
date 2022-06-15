// See https://aka.ms/new-console-template for more information
using LiteDB;

Console.WriteLine("Hello, World!");

using (var db = new LiteDatabase("litedb.db"))
{
    var collection = db.GetCollection<Author>("authors");
    var author = new Author
    {
        FirstName = "Fred",
        LastName = "Feuerstein",
        Address = "Hyderabad"
    };
    collection.Insert(author);
}



public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
}
