using Test.Repository.Entities;
using Test.Repository.Interfaces;
using Test.Resources.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Resources.Repository.Interfaces
{
    public interface IInputRepository : IGenericRepository<InputValue>
    { 
        List<InputValue> AddValuesForCard(int cardId);
    }
}
