namespace ARS.Tools;

public class Result {
    public bool Success { get; init; }
    public string Message { get; init; }


    protected Result(bool success, string error = "") {
        Success = success;
        Message = error;
    }

    public bool Failure => !Success;

    public static Result Fail(string message) {
        return new Result(false, message);
    }

    public static Result Ok() {
        return new Result(true);
    }

    public static Result<T> Ok<T>(T value) {
        return Result<T>.Ok(value);
    }

    public static Result Combine(params Result[] results) {
        foreach (var result in results) {
            if (result.Failure) return result;
        }
        return Ok();
    }
}

public class Result<T> : Result {
    public T? Value { get; init; }

    private Result(T? value, bool success, string error) : base(success, error) {
        if (Success) {
            Value = value;
        }
    }

    public new static Result<T> Fail(string message) {
        return new Result<T>(default, false, message);
    }

    public static Result<T> Ok(T value) {
        return new Result<T>(value, true, string.Empty);
    }
}