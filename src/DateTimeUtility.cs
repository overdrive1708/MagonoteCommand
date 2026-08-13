using System;
using System.Globalization;

namespace MagonoteCommand
{
    /// <summary>
    /// 日付時刻ユーティリティクラス
    /// </summary>
    internal class DateTimeUtility
    {
        /// <summary>
        /// カレンダーの種類
        /// </summary>
        public enum CalendarType
        {
            Gregorian,  // 西暦
            Japanese    // 和暦
        }

        /// <summary>
        /// 今日の日付の取得処理
        /// </summary>
        /// <param name="calendarType">カレンダーの種類</param>
        /// <param name="dateFormat">日付の書式</param>
        /// <returns>今日の日付</returns>
        public static string GetDateToday(CalendarType calendarType, string dateFormat)
        {
            string dateToday = string.Empty;

            switch (calendarType)
            {
                case CalendarType.Gregorian:
                    dateToday = DateTime.Now.ToString(dateFormat);
                    break;
                case CalendarType.Japanese:
                    CultureInfo cultureJp = new("ja-jp", false);
                    cultureJp.DateTimeFormat.Calendar = new JapaneseCalendar();
                    dateToday = DateTime.Now.ToString(dateFormat, cultureJp);
                    break;
                default:
                    break;
            }

            return dateToday;
        }

        /// <summary>
        /// オフセット後の日付の取得処理
        /// </summary>
        /// <param name="calendarType">カレンダーの種類</param>
        /// <param name="dateFormat">日付の書式</param>
        /// <param name="offsetDays">オフセット日数</param>
        /// <returns></returns>
        public static string GetDateOffsetDays(CalendarType calendarType, string dateFormat, double offsetDays)
        {
            string dateOffset = string.Empty;

            switch (calendarType)
            {
                case CalendarType.Gregorian:
                    dateOffset = DateTime.Now.AddDays(offsetDays).ToString(dateFormat);
                    break;
                case CalendarType.Japanese:
                    CultureInfo cultureJp = new("ja-jp", false);
                    cultureJp.DateTimeFormat.Calendar = new JapaneseCalendar();
                    dateOffset = DateTime.Now.AddDays(offsetDays).ToString(dateFormat, cultureJp);
                    break;
                default:
                    break;
            }

            return dateOffset;
        }
    }
}
