using Test.Models;
using Test.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Resources.Entities;

namespace Test.Services.Interfaces
{
    public interface ICardInputService
    {
        List<InputValue> AddInputsForCard(int cardId);
    }
}
