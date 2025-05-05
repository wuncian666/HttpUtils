using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtils
{
    internal class RequestParam
    {
        public string[] ids { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public RequestParam(string[] ids, string title, string description)
        {
            ids = ids;
            title = title;
            description = description;
        }
    }
}