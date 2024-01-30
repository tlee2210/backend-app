namespace backend_app.DTO
{
    public class HomeDTO<T, V>
    {
        public List<T>? data { get; set; } = new List<T>();
        public List<V>? data2 { get; set; } = new List<V>();
        public List<SelectOption>? SelectOption { get; set; }

    }
}
