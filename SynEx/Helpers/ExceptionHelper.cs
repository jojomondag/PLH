namespace SynEx.Helpers
{
    internal static class ExceptionHelper
    {
        public static async Task TryCatchAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError($"Oops! Something went wrong: {ex.Message}");
            }
        }
    }
}