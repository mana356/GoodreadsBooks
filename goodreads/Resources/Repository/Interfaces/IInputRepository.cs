using goodreads.Repository.Entities;
using goodreads.Repository.Interfaces;
using goodreads.Resources.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goodreads.Resources.Repository.Interfaces
{
    public interface IInputRepository : IGenericRepository<InputValue>
    { 
        List<InputValue> AddValuesForCard(int cardId);
    }
}
