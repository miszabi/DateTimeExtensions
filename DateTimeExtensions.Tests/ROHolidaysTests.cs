namespace DateTimeExtensions.Tests
{
    using System;
    using System.Linq;
    using DateTimeExtensions.WorkingDays;
    using NUnit.Framework;

    [TestFixture]
    public class ROHolidaysTests
    {
        private readonly WorkingDayCultureInfo dateTimeCulture = new WorkingDayCultureInfo("ro-RO");

        [Test]
        public void The_Romania_has_12_main_holidays()
        {
            var holidays = dateTimeCulture.Holidays;
            Assert.AreEqual(12, holidays.Count());
        }

        [Test]
        public void The_Romania_has_easter_on_2016_mai_1()
        {
            var holidays = dateTimeCulture.Holidays;
            Assert.IsTrue(dateTimeCulture.IsHoliday(new DateTime(2016,5,1)));
        }

        [Test]
        public void get_holiday_of_years_2016()
        {
            var holidays = dateTimeCulture.GetHolidaysOfYear(2016);
            Assert.IsTrue(holidays.Any());
        }
        
        [Test]
        public void get_pentecost_of_2016()
        {
            var holidays = dateTimeCulture.GetHolidaysOfYear(2016);
            var pentectost = holidays.Where(c => c.Name.Equals("Pentecost")).FirstOrDefault();

            Assert.IsTrue(pentectost.IsInstanceOf(new DateTime(2016, 5, 1).AddDays(49)));
        }
    }
}