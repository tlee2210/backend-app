namespace backend_app.DTO
{
    public class GetEditSelectOption<T>
    {
        public T model { get; set; }

        public List<SelectOption> SelectOption { get; set; }

    }
}
