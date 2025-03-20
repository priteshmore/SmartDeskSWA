using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Answer AnswerData { get; set; } = new Answer();
    }
}
