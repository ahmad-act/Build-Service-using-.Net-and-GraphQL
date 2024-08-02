using BookInformationService.BusinessLayer;
using BookInformationService.GraphQL.Types;
using BookInformationService.Models;
using GraphQL;
using GraphQL.Types;

namespace BookInformationService.GraphQL.Mutations;

public class BookInformationMutation : ObjectGraphType
{
    public BookInformationMutation(IBookInformationBL bookInformationBL)
    {
        Field<BookInformationType>(
            "addBookInformation",
            "Is used to add a new bookInformation to the database",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<BookInformationInputType>> { Name = "bookInformation", Description = "BookInformation input parameter." }
                ),
            resolve: context =>
            {
                var bookInformation = context.GetArgument<BookInformation>("bookInformation");
                if (bookInformation != null)
                {
                    return bookInformationBL.CreateBookInformation(bookInformation);
                }
                return null;
            });

        Field<BookInformationType>(
           "updateBookInformation",
           "Is used to update a existing bookInformation to the database",
           arguments: new QueryArguments(
               new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "Id of the bookInformation that need to be updated" },
               new QueryArgument<NonNullGraphType<BookInformationInputType>> { Name = "bookInformation", Description = "BookInformation input parameter." }
               ),
           resolve: context =>
           {
               var id = context.GetArgument<int>("id");
               var bookInformation = context.GetArgument<BookInformation>("bookInformation");
               if (bookInformation != null)
               {
                   return bookInformationBL.UpdateBookInformation(id, bookInformation);
               }
               return null;
           });

        Field<BookInformationType>(
          "deleteBookInformation",
          "Is used to delete a existing bookInformation to the database",
          arguments: new QueryArguments(
              new QueryArgument<NonNullGraphType<IdGraphType>>
              {
                  Name = "id",
                  Description = "Id of the bookInformation that need to be updated"
              }
              ),
          resolve: context =>
          {
              var id = context.GetArgument<int>("id");

              return bookInformationBL.DeleteBookInformation(id);
          });
    }
}

