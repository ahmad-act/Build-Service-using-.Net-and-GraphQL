using GraphQL.Types;

namespace BookInformationService.GraphQL.Types;

public class BookInformationInputType : InputObjectGraphType
{
    public BookInformationInputType()
    {
        Name = "BookInformationInputType";
        Field<StringGraphType>("Title");
        Field<IntGraphType>("Stock");
    }
}

