using BookInformationService.GraphQL.Mutations;
using BookInformationService.GraphQL.Queries;
using GraphQL.Types;

namespace BookInformationService.GraphQL;

public class AppSchema : Schema
{
    public AppSchema(BookInformationQuery query, BookInformationMutation mutation)
    {
        this.Query = query;
        this.Mutation = mutation;
    }
}

