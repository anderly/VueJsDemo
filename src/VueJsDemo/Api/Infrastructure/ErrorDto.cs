using Newtonsoft.Json;

namespace VueJsDemo.Api.Infrastructure
{
    public class ErrorDto
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
