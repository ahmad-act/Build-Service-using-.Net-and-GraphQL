namespace BookInformationClient;

public class BookInformation
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Stock { get; set; }
    public string Available { get; set; }
}

public class BookInformationsResponse
{
    public List<BookInformation> BookInformations { get; set; }
}

public class BookInformationResponse
{
    public BookInformation BookInformation { get; set; }
}

public class AddBookInformationResponse
{
    public BookInformation addBookInformation { get; set; }
}

public class UpdateBookInformationResponse
{
    public BookInformation updateBookInformation { get; set; }
}

public class DeleteBookInformationResponse
{
    public BookInformation deleteBookInformation { get; set; }
}
