namespace Accounting.Models
{
    public class CurrencyDto
    {
        public bool success { get; set; }
        public string terms { get; set; }
        public string privacy { get; set; }
        public Query query { get; set; }
        public Info info { get; set; }
        public double result { get; set; }

    }
    public class Query
    {
        public string from { get; set; }
        public string to { get; set; }
        public int amount { get; set; }
    }

    public class Info
    {
        public int timestamp { get; set; }
        public double quote { get; set; }
    }

}
