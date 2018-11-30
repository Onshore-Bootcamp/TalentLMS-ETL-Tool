using System.Collections.Generic;

namespace EverymanETL.Models.API
{
    public class TestCompletionStatus
    {
        public int test_id { get; set; }

        public string test_name { get; set; }

        public int user_id { get; set; }

        public string user_name { get; set; }

        public string score { get; set; }

        public string completion_status { get; set; }

        public string completed_on { get; set; }

        public int completed_on_timestamp { get; set; }

        public string total_time { get; set; }

        public List<TestQuestion> questions { get; set; }
    }
}
