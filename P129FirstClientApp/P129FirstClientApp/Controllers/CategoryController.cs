using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P129FirstClientApp.Data;
using P129FirstClientApp.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P129FirstClientApp.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index(int page=1)
        {
            string listUrl = "http://localhost:39203/api/categories/getall?page=" + page;

            HttpResponseMessage httpResponseMessage = null;

            using (HttpClient httpClient = new HttpClient())
            {
                httpResponseMessage = await httpClient.GetAsync(listUrl);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                string content = await httpResponseMessage.Content.ReadAsStringAsync();

                PagenatedListVM<CategoryListVM> pagenatedListVM = JsonConvert.DeserializeObject<PagenatedListVM<CategoryListVM>>(content);

                return View(pagenatedListVM);
            }

            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM categoryCreateVM)
        {
            string postUrl = "http://localhost:39203/api/categories";

            HttpResponseMessage httpResponseMessage = null;

            using (HttpClient httpClient = new HttpClient())
            {
                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();

                StreamContent streamContent = new StreamContent(categoryCreateVM.File.OpenReadStream());
                streamContent.Headers.Add("Content-Type", categoryCreateVM.File.ContentType);

                multipartFormDataContent.Add(streamContent, "File", categoryCreateVM.File.FileName);
                multipartFormDataContent.Add(new StringContent(categoryCreateVM.Name), "Name");
                multipartFormDataContent.Add(new StringContent(categoryCreateVM.IsMain.ToString()), "IsMain");

                if (categoryCreateVM.ParentId != null)
                {
                    multipartFormDataContent.Add(new StringContent(categoryCreateVM.ParentId.ToString()), "ParentId");
                }

                httpResponseMessage = await httpClient.PostAsync(postUrl, multipartFormDataContent);
            }

            return RedirectToAction("index");
        }
    }
}
