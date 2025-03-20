using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DB.Entity
{
    public class Question
    {
        public int Id { get; set; }  // Primary Key
        public string Title { get; set; }  // Short question text
        public string SkillName { get; set; }  // Related skill (e.g., C#, SQL, Blazor)
        public string Concept { get; set; }  // Concept related to the question (e.g., LINQ, Dependency Injection)
        public int MostAskedCategory { get; set; }  // Popularity scale (1-5)

        // Navigation Property for a Single Answer
        public Answer Answer { get; set; }
    }
}
