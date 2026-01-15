using GoodreadsBooks.Resources.Entities;

namespace GoodreadsBooks.Services
{
    public interface ICardInputService
    {
        List<InputValue> AddInputsForCard(int cardId);
    }
}
