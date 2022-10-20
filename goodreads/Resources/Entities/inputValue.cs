using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goodreads.Resources.Entities
{
    [Table("InputValue")]
    public class InputValue

    { 
        [Key]
        public int Id { get; set; }
        public int CardId { get; set; }
        public int InputTypeId { get; set; }
        public string Value { get; set; }
        public InputType InputType { get; set; }

    }
}
