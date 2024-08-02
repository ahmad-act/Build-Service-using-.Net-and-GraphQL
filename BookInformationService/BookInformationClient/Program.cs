using GraphQL.Client.Http;
using GraphQL;
using GraphQL.Client.Serializer.Newtonsoft;
using BookInformationClient;


var apiUrl = "http://localhost:6100/graphql";
var client = new GraphQLHttpClient(apiUrl, new NewtonsoftJsonSerializer());

await GetBookInformations(client);
string id = await AddBookInformation(client, new BookInput { Title = "Book Title", Stock = 100 });
await GetBookInformation(client, id);
await UpdateBookInformation(client, id, new BookInput { Title = "Book Title Updated", Stock = 50 });
await DeleteBookInformation(client, id);

Console.ReadKey();


static async Task GetBookInformations(GraphQLHttpClient client)
{
    var query = @"query MyQuery {
  bookinformations {
    id,
    title,
    stock,
    available
  }
}";

    var request = new GraphQLRequest
    {
        Query = query
    };

    try
    {
        var response = await client.SendQueryAsync<BookInformationsResponse>(request);

        if (response?.Data?.BookInformations == null)
        {
            Console.WriteLine("No data received or data is null.");
            return;
        }

        var bookInformations = response.Data.BookInformations;

        foreach (var book in bookInformations)
        {
            Console.WriteLine($"ID: {book.Id}");
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Stock: {book.Stock}");
            Console.WriteLine($"Available: {book.Available}");
            Console.WriteLine();
        }
    }
    catch (GraphQLHttpRequestException ex)
    {
        Console.WriteLine($"Request Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"General Error: {ex.Message}");
    }
}

static async Task GetBookInformation(GraphQLHttpClient client, string id)
{
    var query = @"query MyQuery {
  __typename
  bookinformation(id: " + id + @") {
    id
    title
    stock
    available
  }
}";

    var request = new GraphQLRequest
    {
        Query = query
    };

    try
    {
        var response = await client.SendQueryAsync<BookInformationResponse>(request);

        if (response?.Data?.BookInformation == null)
        {
            Console.WriteLine("No data received or data is null.");
            return;
        }

        var bookInformation = response.Data.BookInformation;

        Console.WriteLine($"ID: {bookInformation.Id}");
        Console.WriteLine($"Title: {bookInformation.Title}");
        Console.WriteLine($"Stock: {bookInformation.Stock}");
        Console.WriteLine($"Available: {bookInformation.Available}");
        Console.WriteLine();
    }
    catch (GraphQLHttpRequestException ex)
    {
        Console.WriteLine($"Request Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"General Error: {ex.Message}");
    }
}

static async Task<string> AddBookInformation(GraphQLHttpClient client, BookInput bookInput)
{
    var query = $@"
        mutation MyMutation {{
            addBookInformation(bookInformation: {{ title: ""{bookInput.Title}"", stock: {bookInput.Stock} }}) {{
                id
                title
                stock
                available
            }}
        }}";

    var request = new GraphQLRequest
    {
        Query = query,
        //Variables = new { bookInformation = bookInput }
    };

    try
    {
        var response = await client.SendMutationAsync<AddBookInformationResponse>(request);

        if (response?.Data == null)
        {
            Console.WriteLine("No data received or data is null.");
            return string.Empty;
        }

        var bookInformation = response.Data.addBookInformation;

        Console.WriteLine($"ID: {bookInformation.Id}");
        Console.WriteLine($"Title: {bookInformation.Title}");
        Console.WriteLine($"Stock: {bookInformation.Stock}");
        Console.WriteLine($"Available: {bookInformation.Available}");
        Console.WriteLine();

        return bookInformation.Id;
    }
    catch (GraphQLHttpRequestException ex)
    {
        Console.WriteLine($"Request Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"General Error: {ex.Message}");
    }

    return string.Empty;
}

static async Task UpdateBookInformation(GraphQLHttpClient client, string id, BookInput bookInput)
{
    var query = $@"
       mutation MyMutation {{
          updateBookInformation(bookInformation: {{title: ""{bookInput.Title}"", stock: {bookInput.Stock}}}, id: {id}) {{
            id
            title
            stock
            available
          }}
        }}";

    var request = new GraphQLRequest
    {
        Query = query,
    };

    try
    {
        var response = await client.SendMutationAsync<UpdateBookInformationResponse>(request);

        if (response?.Data?.updateBookInformation == null)
        {
            Console.WriteLine("No data received or data is null.");
            return;
        }

        var bookInformation = response.Data.updateBookInformation;

        Console.WriteLine($"ID: {bookInformation.Id}");
        Console.WriteLine($"Title: {bookInformation.Title}");
        Console.WriteLine($"Stock: {bookInformation.Stock}");
        Console.WriteLine($"Available: {bookInformation.Available}");
        Console.WriteLine();
    }
    catch (GraphQLHttpRequestException ex)
    {
        Console.WriteLine($"Request Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"General Error: {ex.Message}");
    }
}

static async Task DeleteBookInformation(GraphQLHttpClient client, string id)
{
    var query = $@"mutation MyMutation {{
                  deleteBookInformation(id: {id}) {{
                    id
                    title
                    stock
                    available
                  }}
                }}";

    var request = new GraphQLRequest
    {
        Query = query
    };

    try
    {
        var response = await client.SendQueryAsync<DeleteBookInformationResponse>(request);

        if (response?.Data?.deleteBookInformation == null)
        {
            Console.WriteLine("No data received or data is null.");
            return;
        }

        var bookInformation = response.Data.deleteBookInformation;

        Console.WriteLine($"ID: {bookInformation.Id}");
        Console.WriteLine($"Title: {bookInformation.Title}");
        Console.WriteLine($"Stock: {bookInformation.Stock}");
        Console.WriteLine($"Available: {bookInformation.Available}");
        Console.WriteLine();
    }
    catch (GraphQLHttpRequestException ex)
    {
        Console.WriteLine($"Request Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"General Error: {ex.Message}");
    }
}
