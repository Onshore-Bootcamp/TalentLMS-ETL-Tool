using System.Collections.Generic;

namespace EverymanETL.Models.API
{
    public class TestQuestion
    {
        public int id { get; set; }

        public string text { get; set; }

        public string type { get; set; }

        public int weight { get; set; }

        public int correct { get; set; }

        public object Answers { get; set; }

        public object CorrectAnswers { get; set; }

        public object UserAnswers { get; set; }
    }
}
