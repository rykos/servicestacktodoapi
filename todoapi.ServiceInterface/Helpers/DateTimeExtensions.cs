using System;

namespace todoapi.ServiceInterface.Helpers;

public static class DateTimeExtensions
{
    public static DateTime StartOfDay(this DateTime theDate) => theDate.Date;

    public static DateTime EndOfDay(this DateTime theDate) => theDate.Date.AddDays(1).AddTicks(-1);
}