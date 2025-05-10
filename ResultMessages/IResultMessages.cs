using System.Net;

namespace ErpMikroservis.ResultMessages
{
    public interface IResultMessages<T>
    {
        public HttpStatusCode HttpStatusCode { get; init; }
        public IList<string> Messages { get; init; }
        public T DataResult { get; init; }
    }
}
