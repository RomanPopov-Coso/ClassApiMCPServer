namespace ClassApiMCPServer.Services;

public class TimeService
{
    /// <summary>
    /// Converts the provided DateTime to a Unix timestamp in seconds.
    /// If the DateTime kind is Unspecified, it is treated as UTC.
    /// </summary>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <returns>Unix epoch time in seconds.</returns>
    public long ConvertToUnixTimeSeconds(DateTime dateTime)
    {
        // Ensure we have a UTC DateTime; if unspecified, assume UTC to avoid unintended local conversions
        if (dateTime.Kind == DateTimeKind.Unspecified)
        {
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }
        else if (dateTime.Kind == DateTimeKind.Local)
        {
            dateTime = dateTime.ToUniversalTime();
        }

        var dto = new DateTimeOffset(dateTime);
        return dto.ToUnixTimeSeconds();
    }
}
