using HttpUtils.Models;
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
                Token = "Bearer 7c0b0d1dab980d0519db59ba3c49acdc2abfe1af"
            };
            //GallerySearchModel result = await httpRequest.GetAsync<GallerySearchModel>("gallery/search/time/week/0/?q=cats", null);
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("type", Encoding.UTF8), "file");
            content.Add(new StringContent("title", Encoding.UTF8), "test_image");
            string fileName = @"D:\test_image.png";
            Stream fileStream = File.OpenRead(fileName);
            StreamContent streamContent = new StreamContent(fileStream);
            content.Add(streamContent, "image", fileName);

            UploadImageModel result = await httpRequest.PostAsync<UploadImageModel>("image", content, null);
            Console.WriteLine("result" + result.data.account_url);
            Console.ReadKey();
        }
    }
}