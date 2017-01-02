using DateTimeExtensions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateTimeExtensions.WorkingDays.CultureStrategies
{
    [Locale("ro-RO")]
    [Locale("ro-RO")]
    public class RO_ROHolidayStrategy : HolidayStrategyBase, IHolidayStrategy
    {
        public RO_ROHolidayStrategy()
        {
            this.InnerHolidays.Add(GlobalHolidays.NewYear);
            this.InnerHolidays.Add(NewYearHoliday);
            this.InnerHolidays.Add(RomanianPrincipalitiesUnionDay);

            this.InnerHolidays.Add(Easter);
            this.InnerHolidays.Add(EasterMonday);

            this.InnerHolidays.Add(GlobalHolidays.InternationalWorkersDay);

            this.InnerHolidays.Add(Pentecost);
            this.InnerHolidays.Add(PentecostMonday);

            this.InnerHolidays.Add(ChristianHolidays.Assumption);

            this.InnerHolidays.Add(StAndrewsDay);

            this.InnerHolidays.Add(ChristianHolidays.Christmas);
            this.InnerHolidays.Add(SecondDayOfChristmas);
        }

        // 2nd January - New Year Holiday
        private static Holiday newYearHoliday;

        public static Holiday NewYearHoliday
        {
            get { return newYearHoliday ?? (newYearHoliday = new FixedHoliday("New Year Holiday", 1, 2)); }
        }

        // 30 November - St. Andrew's Day
        private static Holiday stAndrewsDay;

        public static Holiday StAndrewsDay
        {
            get { return stAndrewsDay ?? (stAndrewsDay = new FixedHoliday("St. Andrew's Day", 11, 30)); }
        }

        private static Holiday easter;

        public static Holiday Easter
        {
            get { return easter ?? (easter = new EasterBasedHoliday("Easter", 0, true)); }
        }

        private static Holiday easterMonday;
        public static Holiday EasterMonday
        {
            get { return easterMonday ?? (easterMonday = new EasterBasedHoliday("EasterMonday", 1, true)); }
        }

        // 24 Ianuary - Day union of the Romanian principalities
        private static Holiday romanianPrincipalitiesUnionDay;

        public static Holiday RomanianPrincipalitiesUnionDay
        {
            get { return romanianPrincipalitiesUnionDay ?? (romanianPrincipalitiesUnionDay = new FixedHoliday("Union of the Romanian principalities", 1, 24)); }
        }

        private static Holiday pentecost;

        public static Holiday Pentecost
        {
            get
            {
                if (pentecost == null)
                {
                    //count offset is 7 because we aren't counting with the easter day inclusive
                    pentecost = new NthDayOfWeekAfterDayHoliday("Pentecost", 7, DayOfWeek.Sunday, Easter);
                }
                return pentecost;
            }
        }

        private static Holiday pentecostMonday;

        public static Holiday PentecostMonday
        {
            get
            {
                if (pentecostMonday == null)
                {
                    //count offset is 7 because we aren't counting with the easter day inclusive
                    pentecostMonday = new EasterBasedHoliday("PentecostMonday", 50, true);
                }
                return pentecost;
            }
        }

        private static Holiday secondDayOfChristmas;
        public static Holiday SecondDayOfChristmas
        {
            get { return secondDayOfChristmas ?? (secondDayOfChristmas = new FixedHoliday("2nd day of Christmas", 12, 26)); }
        }
    }
}
