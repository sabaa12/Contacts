using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestNg.Data.Models
{
    public class contact
    {
        [Key]
        public int ID { get; set; }

        public string userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateofBirth { get; set; }
        public string Phonenumber { get; set; }
        public string numbertype { get; set; }
        public string City { get; set; }
        public string? Adress { get; set; }

        [Column(TypeName = "text")]
        public string? Other { get; set; }
        public bool isfavorite { get; set; }

        public User user { get; set; }


    }
}
