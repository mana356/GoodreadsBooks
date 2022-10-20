using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goodreads.Resources.Entities
{
    [Table("InputType")]
    public class InputType
    { 

        [Key]
        public int Id { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }

        public string Formula { get; set; }
        public ICollection<InputValue> InputValues { get; set; }
    }
}
