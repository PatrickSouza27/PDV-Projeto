namespace ServerAPI.ViewModels
{
    public class ViewModelResult<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new();

        public ViewModelResult(T data, List<string> error)
        {
            Data = data;
            Errors = error;
        }
        public ViewModelResult(T data)=> Data = data;
        public ViewModelResult(string erro) => Errors.Add(erro);
        public ViewModelResult(List<string> erro) => Errors = erro;
    }
}
