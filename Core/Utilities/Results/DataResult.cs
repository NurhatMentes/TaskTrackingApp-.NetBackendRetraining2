namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool isSuccess, string message) : base(isSuccess, message)
        {
            this.Data = data;
        }

        public DataResult(T data, bool isSuccess) : base(isSuccess)
        {
            this.Data = data;
        }

        public T Data { get; }
    }
}
