using System.Collections.Generic;

namespace AgendamentoEventos.ViewModels
{
    public class ResultViewModel<T>
    {
        public ResultViewModel(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResultViewModel(T data)
        {
            Data = data;
            Errors = new List<string>();
        }

        public ResultViewModel(List<string> errors)
        {
            Errors = errors;
        }

        public ResultViewModel(string errors)
        {
            Errors = new List<string>();
            Errors.Add(errors);
        }

        public T Data { get; private set; }
        public List<string> Errors { get; set; }
    }
}
