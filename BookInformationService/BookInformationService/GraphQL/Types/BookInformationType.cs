using BookInformationService.Models;
using GraphQL.Types;

namespace BookInformationService.GraphQL.Types;

public class BookInformationType : ObjectGraphType<BookInformation>
{
    public BookInformationType()
    {

        Field(d => d.Id, type: typeof(IdGraphType)).Description("Id property for Book Information object");
        Field(d => d.Title, type: typeof(StringGraphType)).Description("Title property for Book Information object");
        Field(d => d.Stock, type: typeof(IdGraphType)).Description("Stock property for Book Information object");
        Field(d => d.Available, type: typeof(IdGraphType)).Description("Available property for Book Information object");
    }
}

