using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace VisitorRegistration_ASP.Helpers
{
    public static class ExtensionMethods
    {
        public static string GetDaySuffix(this DateTime date)
        {
            switch (date.Day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }

        public static string WithMaxLength(this string value, int maxLength)
        {
            return value?.Substring(0, Math.Min(value.Length, maxLength));
        }

        public static string TruncateWithEllipsis(this string value, int maxLength)
        {
            if (value.Length > maxLength)
            {
                return value.Substring(0, maxLength - 2) + HttpUtility.HtmlDecode(" &hellip;");
            }
            else
            {
                return value;
            }
        }

        // http://www.schwammysays.net/extension-method-for-datetime-timeago/
        public static string TimeAgo(this DateTime date)
        {
            TimeSpan timeSince = DateTime.Now.Subtract(date);

            if (timeSince.TotalMinutes < 1)
                return "just now";
            if (timeSince.TotalMinutes < 2)
                return "1 minute ago";
            if (timeSince.TotalMinutes < 60)
                return string.Format("{0} minutes ago", timeSince.Minutes);
            if (timeSince.TotalMinutes < 120)
                return "1 hour ago";
            if (timeSince.TotalHours < 24)
                return string.Format("{0} hours ago", timeSince.Hours);
            if (timeSince.TotalDays < 2)
                //return "yesterday";
                return "1 day ago";
            if (timeSince.TotalDays < 7)
                return string.Format("{0} days ago", timeSince.Days);
            if (timeSince.TotalDays < 14)
                return "last week";
            if (timeSince.TotalDays < 21)
                return "2 weeks ago";
            if (timeSince.TotalDays < 28)
                return "3 weeks ago";
            if (timeSince.TotalDays < 60)
                return "last month";
            if (timeSince.TotalDays < 365)
                return string.Format("{0} months ago", Math.Round(timeSince.TotalDays / 30));
            if (timeSince.TotalDays < 730)
                return "last year";
            //last but not least...
            return string.Format("{0} years ago", Math.Round(timeSince.TotalDays / 365));
        }
    }
}
