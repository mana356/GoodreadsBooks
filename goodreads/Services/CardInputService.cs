using GoodreadsBooks.Models;
using GoodreadsBooks.Resources.Entities;
using GoodreadsBooks.Resources.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GoodreadsBooks.Services
{
    public class CardInputService : ICardInputService
    {
        private readonly IServiceProvider _serviceProvider;

        public CardInputService(IOptions<BookFinderOptions> options, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public List<InputValue> AddInputsForCard(int cardId)
        {
            var scope = _serviceProvider.CreateScope();
            var inputRepo = scope.ServiceProvider.GetRequiredService<IInputRepository>();
            var addedRows = inputRepo.AddValuesForCard(cardId);
            return addedRows;
        }
    }
}
