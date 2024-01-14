namespace FireFightingRobot.Framework;

public class Result
{
    public readonly bool Success;
    public readonly string Error;
    public bool Failure => !Success;
    public static readonly string ErrorMessagesSeparator = ", ";

    protected Result(bool success, string error)
    {
        if (success && error != null)
        {
            throw new InvalidOperationException();
        }

        if (!success && error == null)
        {
            throw new InvalidOperationException();
        }

        Success = success;
        Error = error;
    }

    public static Result Fail(string message) =>
        new Result(false, message);

    public static Result<T> Fail<T>(string message) =>
        new Result<T>(default, false, message);

    public static Result OK() => new Result(true, null);

    public static Result<T> OK<T>(T value) =>
        new Result<T>(value, true, null);

    public static Result Combine(string errorMessagesSeparator, params Result[] results)
    {
        var failedResults = results.Where(x => x.Failure).ToList();

        if (failedResults.Count == 0)
            return OK();

        var errorMessage = string.Join(errorMessagesSeparator, failedResults.Select(x => x.Error).ToArray());

        return Fail(errorMessage);
    }

    public static Result Combine(params Result[] results)
    {
        return Combine(ErrorMessagesSeparator, results);
    }

    public static Result Combine<T>(params Result<T>[] results)
    {
        return Combine(ErrorMessagesSeparator, results);
    }

    public static Result Combine<T>(string errorMessagesSeparator, params Result<T>[] results)
    {
        var untyped = results.Select(result => (Result)result).ToArray();

        return Combine(errorMessagesSeparator, untyped);
    }
}

public class Result<T> : Result
{
    private readonly T _value;

    public T Value
    {
        get
        {
            if (!Success) throw new InvalidOperationException();

            return _value;
        }
    }

    protected internal Result(T value, bool success, string error)
        : base(success, error) => this._value = value;
}