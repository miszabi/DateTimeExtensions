#region License

// 
// Copyright (c) 2011-2012, João Matos Silva <kappy@acydburne.com.pt>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateTimeExtensions.WorkingDays
{
    public class EasterBasedHoliday : Holiday
    {
        private int daysOffset;
        private bool isOrtodoxEaster;
        private const string DAYCACHEKEY = "{0}_{1}";

        private IDictionary<string, DateTime> dayCache;
        
        public EasterBasedHoliday(string name, int daysOffset, bool isOrtodoxEaster = false)
            : base(name)
        {
            this.daysOffset = daysOffset;
            this.isOrtodoxEaster = isOrtodoxEaster;
            dayCache = new Dictionary<string, DateTime>();
        }

        public override DateTime? GetInstance(int year)
        {
            if (dayCache.ContainsKey(string.Format(DAYCACHEKEY, year, isOrtodoxEaster)))
            {
                return dayCache[string.Format(DAYCACHEKEY, year, isOrtodoxEaster)];
            }

            DateTime easter = EasterCalculator.CalculateEasterDate(year, isOrtodoxEaster);
            
            var date = easter.AddDays(daysOffset);
            dayCache.Add(string.Format(DAYCACHEKEY, year, isOrtodoxEaster), date);
            return date;
        }

        public override bool IsInstanceOf(DateTime date)
        {
            var day = GetInstance(date.Year);
            return day.HasValue && date.Month == day.Value.Month && date.Day == day.Value.Day;
        }

        public static class EasterCalculator
        {
            private static IDictionary<string, DateTime> easterPerYear;

            static EasterCalculator()
            {
                easterPerYear = new Dictionary<string, DateTime>();
            }

            public static DateTime CalculateEasterDate(int year, bool isOrtodoxEaster)
            {
                if (easterPerYear.ContainsKey(string.Format(DAYCACHEKEY, year, isOrtodoxEaster)))
                {
                    return easterPerYear[string.Format(DAYCACHEKEY, year, isOrtodoxEaster)];
                }

                var easter = GetEasterDate(year, isOrtodoxEaster);
                
                easterPerYear.Add(string.Format(DAYCACHEKEY, year, isOrtodoxEaster), easter);
                return easter;
            }

            private static DateTime GetEasterDate(int year, bool isOrtodoxEaster)
            {
                if (isOrtodoxEaster) 
                {
                    return GetOrthodoxEasterDate(year);
                }

                return GetEasterDate(year);
            }

            //Algoritm downloaded from http://tiagoe.blogspot.com/2007/10/easter-calculation-in-c.html
            private static DateTime GetEasterDate(int year)
            {
                int temp;
                int a, b, c, d, e, f, g, h, i, k, l, m, p, q;

                if (year >= 1583)
                {
                    //Gregorian Calendar Easter 

                    Math.DivRem(year, 19, out a);
                    b = Math.DivRem(year, 100, out c);
                    d = Math.DivRem(b, 4, out e);
                    f = Math.DivRem(b + 8, 25, out temp);
                    g = Math.DivRem(b - f + 1, 3, out temp);
                    Math.DivRem(19*a + b - d - g + 15, 30, out h);
                    i = Math.DivRem(c, 4, out k);
                    Math.DivRem(32 + 2*e + 2*i - h - k, 7, out l);
                    m = Math.DivRem(a + 11*h + 22*l, 451, out temp);
                    p = Math.DivRem(h + l - 7*m + 114, 31, out q);

                    return new DateTime(year, p, q + 1);
                }
                else
                {
                    //Julian Calendar 

                    Math.DivRem(year, 4, out a);
                    Math.DivRem(year, 7, out b);
                    Math.DivRem(year, 19, out c);
                    Math.DivRem(19*c + 15, 30, out d);
                    Math.DivRem(2*a + 4*b - d + 34, 7, out e);
                    f = Math.DivRem(d + e + 114, 31, out g);

                    return new DateTime(year, f, g + 1);
                }
            }

            private static DateTime GetOrthodoxEasterDate(int year)
            {
                int a = year % 19;
                int b = year % 7;
                int c = year % 4;

                int d = (19 * a + 16) % 30;
                int e = (2 * c + 4 * b + 6 * d) % 7;
                int f = (19 * a + 16) % 30;
                int key = f + e + 3;

                int month = (key > 30) ? 5 : 4;
                int day = (key > 30) ? key - 30 : key;

                return new DateTime(year, month, day);
            }
        }
    }
}