using capp.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks.Dataflow;
namespace capp.Services;

public static class BookService{

    static HttpClient client = new HttpClient();

    public static async Task AddBooks(string query, List<Book> books, int nextId){
        string url = $"https://www.googleapis.com/books/v1/volumes?q=intitle:{query}";
        string response = await client.GetStringAsync(url);
        JObject data = JObject.Parse(response);
        JArray items = data["items"] is JArray array ? array : new JArray();

        foreach (JObject item in items){
            JObject volumeInfo = item["volumeInfo"] is JObject info ? info : new JObject();
            string title = volumeInfo["title"]?.ToString() ?? "Unknown";
            JArray authorsArray = volumeInfo["authors"] is JArray authorArray ? authorArray : new JArray();
            List<string> authorsList = authorsArray.Select(a => a.ToString()).ToList();
            string author = string.Join(", ", authorsList);

            Book book = new Book { Id = nextId++, Title = title, Author = author };
            books.Add(book);
        }
    }

    public static async Task<List<Book>> GetBooks(string query) {
        List<Book> books = new List<Book>();
        int bookId = 0;
        await AddBooks(query, books, bookId);
        return books;
    }

}