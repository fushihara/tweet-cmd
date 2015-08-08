using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace sleep {
    class Program {
        static void Main(string[] args) {
            if (args.Length == 0) {
                return;
            }
            DateTime now=DateTime.Now;
            DateTime sleepTime = getTime(args[0]);
            if (sleepTime == null) {
                return;
            }
            Console.WriteLine("wait for " + sleepTime.ToLocalTime());
            Thread.Sleep(sleepTime - now);
        }
        private static DateTime getTime(String val) {
            {
                var regex1 = new Regex(@"(\d+)/(\d+)/(\d+) (\d+):(\d+):(\d+)");
                var match = regex1.Match(val);
                if (match.Success) {
                    int year, month, date, hour, minute, second;
                    year = int.Parse(match.Groups[1].Value);
                    month = int.Parse(match.Groups[2].Value);
                    date = int.Parse(match.Groups[3].Value);
                    hour = int.Parse(match.Groups[4].Value);
                    minute = int.Parse(match.Groups[5].Value);
                    second = int.Parse(match.Groups[6].Value);
                    return new DateTime(year, month, date, hour, minute, second);
                }
            }
            {
                var regex = new Regex(@"(\d+):(\d+):(\d+)");
                var match = regex.Match(val);
                if (match.Success) {
                    int hour, minute, second;
                    hour = int.Parse(match.Groups[1].Value);
                    minute = int.Parse(match.Groups[2].Value);
                    second = int.Parse(match.Groups[3].Value);
                    if (24 <= hour) {
                        hour -= 24;
                    }
                    var now = DateTime.Now;
                    var set = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);
                    if (set < now) {
                        //もし、setが過去だったら
                        set += new TimeSpan(24, 0, 0);
                        return set;
                    } else {
                        return set;
                    }
                }
            }
            {
                //"1d1h1m1s"
                var now = DateTime.Now;
                var hasMatch = false;
                var reg1 = new Regex(@"(\d+)d");
                var reg2 = new Regex(@"(\d+)h");
                var reg3 = new Regex(@"(\d+)m");
                var reg4 = new Regex(@"(\d+)s");
                var match1 = reg1.Match(val);
                var match2 = reg2.Match(val);
                var match3 = reg3.Match(val);
                var match4 = reg4.Match(val);
                if (match1.Success) {
                    now += new TimeSpan(int.Parse(match1.Groups[1].Value), 0, 0, 0);
                    hasMatch = true;
                }
                if (match2.Success) {
                    now += new TimeSpan(int.Parse(match2.Groups[1].Value), 0, 0);
                    hasMatch = true;
                }
                if (match3.Success) {
                    now += new TimeSpan(0, int.Parse(match3.Groups[1].Value), 0);
                    hasMatch = true;
                }
                if (match4.Success) {
                    now += new TimeSpan(0, 0, int.Parse(match4.Groups[1].Value));
                    hasMatch = true;
                }
                if (hasMatch) {
                    return now;
                }
            }
            return DateTime.Now;
        }
    }
}
