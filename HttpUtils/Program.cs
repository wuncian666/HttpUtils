using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtils
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IHttpRequest httpRequest = new HttpRequest
            {
                BaseUrl = "https://api.imgur.com/3",
                Token = Config.TOKEN,
            };
            Model result = await httpRequest.GetAsync<Model>("gallery/search/time/week/0/?q=cats", null);

            var ids = new string[0];
            var title = "test_image";
            var description = "test_image";
            MultipartFormDataContent content = new MultipartFormDataContent();
            if (ids != null)
            {
                foreach (string id in ids)
                {
                    content.Add(new StringContent(id, Encoding.UTF8), "ids[]");
                }
            }

            if (title != null)
            {
                content.Add(new StringContent(title, Encoding.UTF8), "title");
            }

            if (description != null)
            {
                content.Add(new StringContent(description, Encoding.UTF8), "description");
            }

            var param = new RequestParam(null, title, description);

            var res = await httpRequest.PutAsync<BasicModel>("album/" + "DE2ktrV", param, null);

            //var content = new MultipartFormDataContent();
            //content.Add(new StringContent("type", Encoding.UTF8), "file");
            //content.Add(new StringContent("title", Encoding.UTF8), "test_image");
            //string fileName = @"D:\test_image.png";
            //Stream fileStream = File.OpenRead(fileName);
            //StreamContent streamContent = new StreamContent(fileStream);
            //content.Add(streamContent, "image", fileName);

            //UploadModel result = await httpRequest.PostAsync<UploadModel>("image", content, null);
            //Console.WriteLine("result" + result.data.account_url);
            Console.ReadKey();
        }
    }
}