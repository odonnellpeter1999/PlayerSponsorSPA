using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace PlayerSponsor.UnitTests.Common;

internal class AssertionHelper
{
    public static T GetValueFromActionResult<T>(IActionResult actionResult)
    {
        if (actionResult is ObjectResult objectResult)
        {
            return (T)objectResult.Value;
        }

        throw new InvalidOperationException("The IActionResult does not contain a value of the expected type.");
    }

    public static void AssertModelStateError(IActionResult result, string expectedKey, string expectedMessage)
    {
        var resultObject = GetValueFromActionResult<SerializableError>(result);
        Assert.That(resultObject, Is.Not.Null);
        var errorEntry = resultObject.FirstOrDefault(e => e.Key == expectedKey);
        Assert.That(errorEntry.Key, Is.EqualTo(expectedKey));
        Assert.That((errorEntry.Value as string[]).FirstOrDefault(), Is.EqualTo(expectedMessage));
    }
}
