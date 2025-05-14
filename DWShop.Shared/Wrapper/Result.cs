namespace DWShop.Shared.Wrapper
{
    public class Result : IResult
    {
        public List<string> Messages { get; set; }
        public bool Succeded { get; set; }

        public static IResult Fail()
        {
            return new Result { Succeded = false };
        }

        public static IResult Fail(string message)
        {
            return new Result { Succeded = false, Messages = new() { message } };
        }

        public static IResult Fail(List<string> messages)
        {
            return new Result { Succeded = false, Messages = messages };
        }

        public static Task<IResult> FailAsync()
        {
            return Task.FromResult(Fail()); ;
        }

        public static Task<IResult> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static Task<IResult> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        public static IResult Success()
        {
            return new Result { Succeded = true };

        }

        public static IResult Success(string message)
        {
            return new Result { Succeded = true, Messages = { message } };
        }

        public static IResult Success(List<string> messages)
        {
            return new Result { Succeded = true, Messages = messages };
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        public T Data { get; set; }

        public new static Result<T> Fail()
        {
            return new Result<T> { Succeded = false };
        }

        public new static Result<T> Fail(string message)
        {
            return new Result<T> { Succeded = false, Messages = { message } };
        }

        public new static Result<T> Fail(List<string> messages)
        {
            return new Result<T> { Succeded = false, Messages = messages };
        }

        public new static Result<T> Success()
        {
            return new Result<T> { Succeded = true };
        }

        public new static Result<T> Success(T data)
        {
            return new Result<T> { Succeded = true, Data = data };
        }

        public new static Result<T> Success(T data, string message)
        {
            return new Result<T> { Succeded = true, Data = data, Messages = { message } };
        }

        public new static Task<Result<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }
    }

}
