using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DB.Entity
{
    public class Answer
    {
        public int Id { get; set; }  // Primary Key
        public string Text { get; set; }  // Answer text

        // Foreign Key (One-to-One)
        public int QuestionId { get; set; }
        public Question Question { get; set; }  // Navigation Property
    }
}
