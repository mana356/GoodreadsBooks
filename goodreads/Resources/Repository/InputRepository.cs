using goodreads.Repository;
using goodreads.Repository.DAL;
using goodreads.Repository.Entities;
using goodreads.Repository.Interfaces;
using goodreads.Resources.Entities;
using goodreads.Resources.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace goodreads.Resources.Repository
{
    public class InputRepository : GenericRepository<InputValue>, IInputRepository
    {
        private readonly BookContext _dbContext;

        public InputRepository(BookContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<InputValue> AddValuesForCard(int cardId)
        {
            var values = new List<InputValue>();

            try
            {
                var inputTypes = _dbContext.InputTypes.ToList();
                var random = new Random();

                foreach (var inputType in inputTypes)
                {
                    var inputValue = new InputValue()
                    {
                        CardId = cardId,
                        InputType = inputType,
                        Value = !inputType.Type.Equals("complex") ? random.NextDouble().ToString() : ""
                    };
                    _dbContext.InputValues.Add(inputValue);
                    values.Add(inputValue);
                }

                var constantValues = values.Where(x => !x.InputType.Type.Equals("complex")).ToList();
                var complexValues = values.Where(x => x.InputType.Type.Equals("complex")).ToList();

                foreach (var complexRecord in complexValues)
                {
                    complexRecord.Value = GetCalculatedComplexValue(constantValues, complexRecord.InputType.Formula);
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return values;
            
        }

        public string GetCalculatedComplexValue(List<InputValue> inputValues, string formula) {

            var expression = formula;
            var matches = Regex.Matches(formula, @"\((.+?)\)");

            var results = (from Match m in matches select m.Groups[1].ToString()).ToList();

            foreach (var label in results)
            {
                var matchedLabel = label.ToString()??"";
                var matchedValue = inputValues.Where(x => x.InputType.Label.Equals(matchedLabel, StringComparison.OrdinalIgnoreCase)).FirstOrDefault()?.Value;
                expression = expression.Replace(matchedLabel, matchedValue);
            }
            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(double), expression);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return loDataTable.Rows[0]["Eval"].ToString() ?? "";

        }
    }
}
