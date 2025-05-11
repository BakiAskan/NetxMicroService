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
