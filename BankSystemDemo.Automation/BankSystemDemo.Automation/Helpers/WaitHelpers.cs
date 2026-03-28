using System;
using FlaUI.Core.Tools;

namespace BankSystemDemo.Automation.Helpers
{
    public static class WaitHelpers
    {
        public static T RetryWhileNull<T>(Func<T?> factory, TimeSpan timeout) where T : class
        {
            var result = Retry.WhileNull(factory, timeout);
            if (!result.Success || result.Result == null) throw new TimeoutException("Element not found within timeout.");
            return result.Result;
        }
    }
}
