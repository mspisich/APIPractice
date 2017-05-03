using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lab26.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        //public ActionResult WeatherData(string latitude, string longitude)
        public ActionResult WeatherData()
        {
            //Grand Rapids latitude: 42.335722      Longitude: -83.049944
            HttpWebRequest request = WebRequest.CreateHttp("http://forecast.weather.gov/MapClick.php?lat=42.335722&lon=-83.049944&FcstType=json");
            //HttpWebRequest request = WebRequest.CreateHttp("http://forecast.weather.gov/MapClick.php?lat=" + latitude + "&lon=" + longitude + "&FcstType=json");

            //Tells the user what browsers we're using
            request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";

            // request.Headers.Add("X-Mashape-Key",value);
            //actually grabs the request
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //gets a stream of text
            StreamReader rd = new StreamReader(response.GetResponseStream());
            //reads to the end of file
            string WX_Data = rd.ReadToEnd();
            //Converts that text into JSON
            JObject weatherData = JObject.Parse(WX_Data);

            ViewBag.WX_Data = "7-Day forecast:";

            for (int i = 0; i < 12; i += 2)
            {
                ViewBag.WX_Data += "\n\n" + weatherData["time"]["startPeriodName"][i];
                ViewBag.WX_Data += ": High " + weatherData["data"]["temperature"][i];
                ViewBag.WX_Data += ", Low " + weatherData["data"]["temperature"][i + 1];
            }

            return View();
        }
    }
}
