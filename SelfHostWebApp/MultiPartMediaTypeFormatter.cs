using SelfHostWebApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;

namespace SelfHostWebApp
{
    public class MultiPartMediaTypeFormatter : MediaTypeFormatter
    {
        public MultiPartMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"));
        }

        public override bool CanReadType(Type type)
        {
            return typeof(FileData) == type;
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            object result = null;
            if (content.IsMimeMultipartContent())
            {
                var multipart = await content.ReadAsMultipartAsync();
                foreach(var item in multipart.Contents)
                {
                    var data = await item.ReadAsByteArrayAsync();
                    result = new FileData(item.Headers.ContentDisposition.FileName.Replace("\"", ""), data);
                    
                }
            }

            return result;
        }
    }
}
