using System.Net;

namespace ErpMikroservis.ResultMessages
{
    public class ResultMessages<T> : IResultMessages<T>
    {
        public HttpStatusCode HttpStatusCode { get; init; }
        public IList<string> Messages { get; init; }
        public T DataResult { get; init; }

        public ResultMessages(IList<string> Messages, HttpStatusCode Status)
        {
            HttpStatusCode = Status;
            this.Messages = Messages;
        }
        public ResultMessages(T dataResult, HttpStatusCode Status, IList<string> Messages)
        {
            HttpStatusCode = Status;
            DataResult = dataResult;
            this.Messages = Messages;
        }
        public static ResultMessages<T> ErrorMessage(IList<string> Messages, HttpStatusCode status)
        {
            return new ResultMessages<T>(Messages, status);
        }
        public static ResultMessages<T> SuccessMessage(T dataResult, HttpStatusCode status, IList<string> Message = null)
        {
            return new ResultMessages<T>(dataResult, status, Message);
        }
        public static ResultMessages<T> SuccessMessage(IList<string> Messages, HttpStatusCode status)
        {
            return new ResultMessages<T>(Messages, status);
        }
    }
}
