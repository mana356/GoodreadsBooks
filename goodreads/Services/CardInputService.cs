using Test.Repository.Entities;
using Test.Repository;
using Test.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Test.Repository.Interfaces;
using Test.Resources.Repository.Interfaces;
using Test.Resources.Entities;
using Test.Services.Interfaces;

namespace Test.Services
{
    public class CardInputService: ICardInputService
    {
        private readonly IServiceProvider _serviceProvider;

        public CardInputService(IOptions<BookFinderOptions> options, IServiceProvider serviceProvider) {
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
