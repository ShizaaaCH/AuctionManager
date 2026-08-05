namespace AuctionManager.Api.UseCases.Common;

public class Result
{
    public bool Success { get; }

    public ErrorType? Type { get; }

    public string? ErrorMessage { get; }

    private Result(
        bool success,
        ErrorType? type,
        string? errorMessage)
    {
        Success = success;
        Type = type;
        ErrorMessage = errorMessage;
    }

    public static Result Ok()
        => new(true, null, null);

    public static Result NotFound(
        string message)
        => new(false, ErrorType.NotFound, message);

    public static Result Validation(
        string message)
        => new(false, ErrorType.Validation, message);

    public static Result Conflict(
        string message)
        => new(false, ErrorType.Conflict, message);
}