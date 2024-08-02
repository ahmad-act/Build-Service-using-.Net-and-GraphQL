using BookInformationService.DataAccessLayer;
using BookInformationService.Models;

namespace BookInformationService.BusinessLayer
{
    public class BookInformationBL : IBookInformationBL
    {
        private readonly ILogger<object> _logger;
        private readonly IBookInformationDL _bookInformationDL;

        public BookInformationBL(ILogger<object> logger, IBookInformationDL bookInformationDL)
        {
            _logger = logger;
            _bookInformationDL = bookInformationDL;
        }

        public async Task<List<BookInformation>?> GetBookInformations()
        {
            List<BookInformation>? bookInformations = await _bookInformationDL.GetBookInformations();

            return bookInformations;
        }

        public async Task<BookInformation?> GetBookInformation(int id)
        {
            BookInformation? bookInformation = await _bookInformationDL.GetBookInformation(id);

            return bookInformation;
        }

        public async Task<BookInformation?> CreateBookInformation(BookInformation bookInformation)
        {
            bookInformation.Available = bookInformation.Stock;

            int result = await _bookInformationDL.CreateBookInformation(bookInformation);
            return bookInformation;
        }

        public async Task<BookInformation?> UpdateBookInformation(int id, BookInformation bookInformation)
        {
            if (bookInformation == null)
            {
                return null;
            }

            var existingBookInformation = await _bookInformationDL.GetBookInformation(id);

            if (existingBookInformation == null)
            {
                return null;
            }

            existingBookInformation.Title = bookInformation.Title;
            existingBookInformation.Stock = bookInformation.Stock;

            int result = await _bookInformationDL.UpdateBookInformation(existingBookInformation);

            return existingBookInformation;
        }

        public async Task<BookInformation?> DeleteBookInformation(int id)
        {
            var bookInformation = await _bookInformationDL.GetBookInformation(id);

            if (bookInformation is not null)
            {
                int result = await _bookInformationDL.DeleteBookInformation(bookInformation);
            }

            return bookInformation;
        }
    }
}
