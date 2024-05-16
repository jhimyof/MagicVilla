using System.Net;

namespace MagicVilla_API.Models
{
    public class APIResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public bool IsSuccess { get; set; } = true;                     
        public List<String> ErrrorMessages { get; set;}

        public object Resultado { get; set; }
    }
}
