namespace WebApi.Models
{
    // TODO use JSON Converter instead of Pair
    public class Pair<T> where T : new()
    {
        public string Key { get; set; }

        public T Value { get; set; }
    }
}
