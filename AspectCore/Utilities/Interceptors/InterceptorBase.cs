using Castle.DynamicProxy;
using ErpMikroservis.ResultMessages;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace ErpMikroservis.AspectCore
{
    public abstract class InterceptorBase<TAttribute> : IInterceptor where TAttribute : AttributeBase
    {
        protected virtual void OnBefore(IInvocation invocation, TAttribute attribute) { }
        protected virtual void OnAfter(IInvocation invocation, TAttribute attribute) { }
        protected virtual void OnException(IInvocation invocation, Exception ex, TAttribute attribute) { }
        protected virtual void OnSuccess(IInvocation invocation, TAttribute attribute) { }
        protected virtual void OnCacheRemove(IInvocation invocation, TAttribute attribute) { }

        public virtual void Intercept(IInvocation invocation)
        {
            var attribute = GetAttributes(invocation.MethodInvocationTarget, invocation.TargetType);
            if (attribute is null)
            {
                invocation.Proceed();
            }
            else
            {
                var isSuccess = true;
                OnBefore(invocation, attribute);

                try
                {
                    invocation.Proceed();
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                    OnException(invocation, ex, attribute);
                    var errorMessage = ResultMessages<dynamic>.ErrorMessage(
                    new List<string> { "Beklenmedik Bir Hata İle Karşılaşıldı, Lütfen Sistem Yöneticisi İle İletişime Geçiniz..!" },
                    HttpStatusCode.InternalServerError
                );
                    var returnType = invocation.Method.ReturnType;

                    // Task<T> olup olmadığını kontrol et
                    if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                    {
                        var innerType = returnType.GetGenericArguments()[0]; // örn. IResultMessages<Foo>

                        // IResultMessages<T>'ye uygun bir örnek oluştur
                        var resultMessagesType = typeof(ResultMessages<>).MakeGenericType(innerType.GetGenericArguments()[0]);
                        var errorMethod = resultMessagesType.GetMethod("ErrorMessage", BindingFlags.Public | BindingFlags.Static);

                        var errorInstance = errorMethod.Invoke(null, new object[]
                        {
        new List<string> { "Beklenmedik Bir Hata İle Karşılaşıldı, Lütfen Sistem Yöneticisi İle İletişime Geçiniz..!" },
        HttpStatusCode.InternalServerError
                        });

                        var taskFromResultMethod = typeof(Task)
                            .GetMethod(nameof(Task.FromResult))
                            .MakeGenericMethod(innerType);

                        invocation.ReturnValue = taskFromResultMethod.Invoke(null, new object[] { errorInstance });
                    }
                    else
                    {
                        // Task olmayan senaryo (örn. IResultMessages<T> direkt dönülüyorsa)
                        // Bu nadir olur ama kontrol faydalı olabilir
                        invocation.ReturnValue = errorMessage;
                    }

                }
                finally
                {
                    if (isSuccess)
                    {
                        OnSuccess(invocation, attribute);
                    }
                }

                OnAfter(invocation, attribute);
                OnCacheRemove(invocation, attribute);
            }
        }

        private TAttribute? GetAttributes(MethodInfo methodInfo, Type type)
        {
            var attribute = methodInfo.GetCustomAttribute<TAttribute>(true);
            if (attribute is not null) return attribute;

            return type.GetTypeInfo().GetCustomAttribute<TAttribute>(true);
        }
    }
}
