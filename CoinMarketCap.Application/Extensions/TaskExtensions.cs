namespace CoinMarketCap.Application.Extensions;

public static class TaskExtensions
{
    public static void SafeFireAndForget(
        this Task task,
        in Action? onCompleted = null,
        in Action<Exception>? onException = null,
        bool continueOnCapturedContext = false)
        => HandleSafeFireAndForget(task, continueOnCapturedContext, onCompleted, onException);

#pragma warning disable S3168 // "async" methods should not return "void"
    private static async void HandleSafeFireAndForget<TException>(
#pragma warning restore S3168 // "async" methods should not return "void"
        Task task,
        bool continueOnCapturedContext,
        Action? onCompleted,
        Action<TException>? onException)
        where TException : Exception
    {
        await task.ConfigureAwait(continueOnCapturedContext);
        onCompleted?.Invoke();
    }
}