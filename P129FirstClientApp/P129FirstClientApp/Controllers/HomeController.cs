using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using P129FirstClientApp.Data.ViewModel;
using P129FirstClientApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace P129FirstClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public async Task<IActionResult> Login()
        //{
        //    string loginUrl = "http://localhost:40543/api/accounts/login";

        //    HttpResponseMessage httpResponseMessage = null;

        //    LoginVM loginVM = new LoginVM
        //    {
        //        UserName = "SuperAdmin",
        //        Password = "SuperAdmin123"
        //    };

        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        string content = JsonConvert.SerializeObject(loginVM);

        //        StringContent stringContent = new StringContent(content,System.Text.Encoding.UTF8,"application/json");

        //        httpResponseMessage = await httpClient.PostAsync(loginUrl, stringContent);
        //    }

        //    if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        string content = await httpResponseMessage.Content.ReadAsStringAsync();

        //        LoginResponseVM loginResponseVM = JsonConvert.DeserializeObject<LoginResponseVM>(content);

        //        Response.Cookies.Append("AuthToken", loginResponseVM.Token);
        //    }

        //    return Content("");
        //}

        //public async Task<IActionResult> GetCategories()
        //{
        //    string url = "http://localhost:40543/api/categories";

        //    HttpResponseMessage httpResponseMessage = null;

        //    using(HttpClient httpClient = new HttpClient())
        //    {
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["AuthToken"]);

        //        httpResponseMessage = await httpClient.GetAsync(url);
        //    }

        //    if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        string content = await httpResponseMessage.Content.ReadAsStringAsync();

        //        List<CategoryListVM> categoryListVMs = JsonConvert.DeserializeObject<List<CategoryListVM>>(content);

        //        return Json(categoryListVMs);
        //    }

        //    return View();
        //}

        //public async Task<IActionResult> CreateCategory()
        //{
        //    CategoryCreateVM categoryCreateVM = new CategoryCreateVM
        //    {
        //        Ad = "TestCategory22",
        //        Esasdirmi = true,
        //        AidOlduguUstCategoryId = null,
        //        Sekli = "test1.jpg"
        //    };

        //    string url = "http://localhost:40543/api/categories";

        //    HttpResponseMessage httpResponseMessage = null;

        //    using(HttpClient httpClient = new HttpClient())
        //    {
        //        string content = JsonConvert.SerializeObject(categoryCreateVM);

        //        StringContent stringContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");

        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["AuthToken"]);

        //        httpResponseMessage =await httpClient.PostAsync(url, stringContent);
        //    }

        //    if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Created)
        //    {
        //        string content =await httpResponseMessage.Content.ReadAsStringAsync();

        //        CategoryGetVM categoryGetVM = JsonConvert.DeserializeObject<CategoryGetVM>(content);

        //        return Json(categoryGetVM);
        //    }
        //    else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
        //    {
        //        return Json(await httpResponseMessage.Content.ReadAsStringAsync());
        //    }

        //    return View();
        //}
    }
}
