namespace EverymanETL.Models.API
{
    public class RateLimit
    {
        public int Limit { get; set; }

        public int Remaining { get; set; }

        public string Reset { get; set; }

        public string formatted_reset { get; set; }
    }
}
