using Business.Models;
using Business.Services.User;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Calendar.Models
{
    public class HangfireEvent
    {
        private readonly IUserService userService;
        private readonly string logSeparator = "-----------------------------------------------------------------------------------";
        public HangfireEvent(IUserService userService)
        {
            this.userService = userService;
        }

        public void ScheduleNotification(Event _event, int offset)
        {
            if (_event.Notify != null &&
                !_event.Notify.TimeUnit.Equals(NotifyTimeUnit.NoNotify) &&
                _event.Notify.Value > 0)
            {
                var utcNow = DateTime.UtcNow;
                var notify = _event.Notify;
                var time = _event.Start - utcNow;

                switch (_event.Notify.TimeUnit)
                {
                    default:
                    case NotifyTimeUnit.Min:
                        time -= TimeSpan.FromMinutes(notify.Value);
                        break;
                    case NotifyTimeUnit.Hour:
                        time -= TimeSpan.FromHours(notify.Value);
                        break;
                    case NotifyTimeUnit.Day:
                        time -= TimeSpan.FromDays(notify.Value);
                        break;
                }
                var localTimeStart = _event.Start.AddMinutes(-offset);
                var localTimeFinish = _event.Finish.AddMinutes(-offset);

                if (_event.Repeat.Equals(Interval.NoRepeat) &&
                    localTimeStart.Year.Equals(localTimeFinish.Year) &&
                    localTimeStart.DayOfYear.Equals(localTimeFinish.DayOfYear) &&
                    time.TotalSeconds > 0)
                {
                    var task00 = BackgroundJob.Schedule(() => CreateJob(_event, time), time);
                }
            }
        }

        public void CreateJob(Event _event, TimeSpan time)
        {
            const int maxTitleLen = 30;
            // const int maxBodyLen = 120;
            var browsers = userService.GetBrowsers(_event.CalendarId);
            var title = _event.Title.Length < maxTitleLen ? _event.Title : _event.Title.Substring(0, maxTitleLen - 4) + "...";
            SendPush(
                title, // title
                $"The event starts on {_event.Start.ToString("MMMM", CultureInfo.InvariantCulture)} {_event.Start.Day} at {_event.Start.ToString("hh:mm tt")}.", // body
                $"https://netspasibo.space/", // redirect
                browsers.Select(b => b.BrowserId).ToArray(),
                time.TotalSeconds
                );
            Console.WriteLine($"Event {_event.Id}\n{logSeparator}");
        }

        public void CreateReccutingJob(int eventId)
        {
            //RecurringJob.AddOrUpdate(
            //    $"event{eventId}",
            //    () => ,
            //    Cron.Minutely
            //);
        }

        private async void SendPush(string title, string body, string clickRedirect, string[] browsers, double ttl)
        {
            const string host = "https://fcm.googleapis.com/fcm/send";
            const string iconUrl = "https://www.netspasibo.space/favicon.png";

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = builder.Build();
            var token = config["Authentication:Google:Push"];
            var authHeaderValue = "key=" + token;

            var data = JsonConvert.SerializeObject(
                new
                {
                    notification = new
                    {
                        title,
                        body,
                        icon = iconUrl,
                        click_action = clickRedirect,
                    },
                    registration_ids = browsers,
                }
            );
            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authHeaderValue);
            var response = await client.PostAsync(host, new StringContent(data, Encoding.UTF8, "application/json"));
            var respText = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{respText}\n{logSeparator}");

        }
    }
}
