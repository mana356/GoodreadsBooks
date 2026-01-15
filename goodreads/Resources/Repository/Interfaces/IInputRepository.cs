using GoodreadsBooks.Repository.Entities;
using GoodreadsBooks.Repository.Interfaces;
using GoodreadsBooks.Resources.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodreadsBooks.Resources.Repository.Interfaces
{
    public interface IInputRepository : IGenericRepository<InputValue>
    { 
        List<InputValue> AddValuesForCard(int cardId);
    }
}
