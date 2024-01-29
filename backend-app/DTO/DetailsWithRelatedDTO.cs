namespace backend_app.DTO
{
    public class DetailsWithRelatedDTO<T, V>
    {
        public T data { get; set; }
        public List<V> listData { get; set; } = new List<V>();
    }
}
