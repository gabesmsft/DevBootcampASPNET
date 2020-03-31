using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Configuration;

using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System.Diagnostics;

namespace DevBootCampASPNETLogging.Controllers
{
    public class HomeController : Controller
    {
        private static string key = TelemetryConfiguration.Active.InstrumentationKey = System.Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY", EnvironmentVariableTarget.Process) ?? "";
        private static TelemetryClient telemetry = new TelemetryClient() { InstrumentationKey = key };

        public ActionResult Index()
        {
            //throw new Exception("fake exception");

            //Using built-in .NET trace logger. Note: Can Leave out the System.Diagnostics part if you have
            //using System.Diagnostics statement. Am writing this out for illustration purposes
            System.Diagnostics.Trace.WriteLine("In Index method");

            string connectionstring = ConfigurationManager.ConnectionStrings["FakeConnectionString"].ToString();
            System.Diagnostics.Trace.WriteLine("FakeConnectionString value: " + connectionstring);

            string appsetting = ConfigurationManager.AppSettings["FakeAppSetting"];
            System.Diagnostics.Trace.WriteLine("FakeAppSetting value: " + appsetting);

            Trace.Flush();

            var fakeValue = "fake string";
            //example of using App Insights SDK to write trace statements.
            telemetry.TrackTrace("fake label", new Dictionary<string, string> { { "name of value you are tracking", fakeValue } });

            return View();
        }
    }
}