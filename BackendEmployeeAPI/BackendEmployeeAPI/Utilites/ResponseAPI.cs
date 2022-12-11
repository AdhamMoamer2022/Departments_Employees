namespace BackendEmployeeAPI.Utilites
{
    public class ResponseAPI<T>
    {
        public bool Status { get; set; }
        public string Msg { get; set; }

        public T? Value { get; set; }

    }
}
