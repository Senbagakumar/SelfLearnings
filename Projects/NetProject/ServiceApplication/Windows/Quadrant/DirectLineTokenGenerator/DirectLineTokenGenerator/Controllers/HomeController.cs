using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DirectLineTokenGenerator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {           
            return View();
        }

        public async Task<DirectLineToken> GenerateToken()
        {
            var directLine = new DirectLineToken();
            var config = new ChatConfig();
            try
            {
                var secret = "cL5bN3nBfXE.7TTxeTXRyTxWV4vmcs3sNqjABBe_9u-_moH8NI-8K50";
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(
                  HttpMethod.Post,
                  $"https://directline.botframework.com/v3/directline/tokens/generate");

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", secret);

                var userId = $"dl_{Guid.NewGuid()}";
                request.Content = new StringContent(
                JsonConvert.SerializeObject(
                    new { User = new { Id = userId } }),
                    Encoding.UTF8,
                    "application/json");

                var response = await client.SendAsync(request);
                string token = String.Empty;

                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    directLine = JsonConvert.DeserializeObject<DirectLineToken>(body);  //l_11_un7HKk.j7SiuuYH6s1kEjswUyYca55tECp4qciHCtIkO3LrGqU cL5bN3nBfXE.7TTxeTXRyTxWV4vmcs3sNqjABBe_9u-_moH8NI-8K50
                }

                //https://directline.botframework.com/v3/directline/tokens/refresh
            }
            catch (Exception ex)
            {
                throw;
            }
            return directLine;
        }

        public PartialViewResult BOT()
        {
            DirectLineToken directLine = null;
            if (Session["botToken"] == null)
            {
                Task.Run(async () => { directLine = await GenerateToken(); }).GetAwaiter().GetResult();
                Session["botToken"] = directLine;
                ViewBag.firstTime = 1;
            }
            else
            {
                directLine = (DirectLineToken)Session["botToken"];
                ViewBag.firstTime = 0;
            }
            ViewBag.token = directLine.token;
            ViewBag.cid = directLine.conversationId;
            ViewBag.userId = $"dl_{Guid.NewGuid()}";
            return PartialView("BOT");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}