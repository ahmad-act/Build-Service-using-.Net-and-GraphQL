using BookInformationService.GraphQL.Types;
using BookInformationService.BusinessLayer;
using GraphQL;
using GraphQL.Types;

namespace BookInformationService.GraphQL.Queries;

public class BookInformationQuery : ObjectGraphType
{
    public BookInformationQuery(IBookInformationBL bookInformation)
    {
        Field<ListGraphType<BookInformationType>>(
            "bookinformations",
            "Return all the book information",
            resolve: context => bookInformation.GetBookInformations());

        Field<BookInformationType>(
            "bookinformation",
            "Return a single book information by id",
            new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "BookInformation Id" }),
            resolve: context => bookInformation.GetBookInformation(context.GetArgument("id", int.MinValue)));
    }
}

/*
 {
  bookinformations {
    title
  }
}
 */
