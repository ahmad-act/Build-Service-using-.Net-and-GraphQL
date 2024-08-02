using BookInformationService.Models;

namespace BookInformationService.BusinessLayer
{
    public interface IBookInformationBL
    {
        Task<BookInformation?> CreateBookInformation(BookInformation bookInformation);
        Task<BookInformation?> DeleteBookInformation(int id);
        Task<BookInformation?> GetBookInformation(int id);
        Task<List<BookInformation>?> GetBookInformations();
        Task<BookInformation?> UpdateBookInformation(int id, BookInformation bookInformation);
    }
}