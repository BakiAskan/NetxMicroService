using System.Net;

namespace ErpMikroservis.ResultMessages
{
    public interface IResultMessages<T>
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public IList<string> Messages { get; set; }
        public T DataResult { get; set; }
    }
}
