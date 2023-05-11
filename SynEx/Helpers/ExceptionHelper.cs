using System;
using System.Threading.Tasks;

namespace SynEx.Helpers
{
    //A helper class to catch exceptions and display them to the user also utalizing the MessageHelper class.
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
        public static T TryCatch<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError($"Oops! Something went wrong: {ex.Message}");
                return default(T);
            }
        }

        public static async Task<T> TryCatchAsync<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError($"Oops! Something went wrong: {ex.Message}");
                return default(T);
            }
        }
    }
}