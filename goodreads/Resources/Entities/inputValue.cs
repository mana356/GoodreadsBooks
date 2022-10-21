using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Resources.Entities
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
