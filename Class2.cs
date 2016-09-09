using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class DomainServiceBase
    {
        public IServiceType GetService<IServiceType>()
        {
            return ServiceLocator.Current.GetInstance<IServiceType>();
        }
        protected ILoggingService LoggingSvc { get { return ServiceLocator.Current.GetInstance<ILoggingService>(); } }

        protected ICachingService CachingSvc
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ICachingService>();
            }
        }



        //Common method relevant to Domain Service and WCF service will be moved here
        protected TResult InvokeDomainService<TAppService, TResult>(Expression<Func<TAppService, IList<BtmuApplicationMessage>, TResult>> appServiceExpression)
        {
            TResult result = default(TResult);
            IList<BtmuApplicationMessage> messages = new List<BtmuApplicationMessage>();

            var appService = ServiceLocator.Current.GetInstance<TAppService>();
            result = appServiceExpression.Compile().Invoke(appService, messages);

            return result;
        }

    }

    public abstract class ExternalServiceBase
    {
        //Add common headers for BPM service. Common header will have service Id needed by BPM. this service id is a part of BPM service request

        protected ILoggingService LoggingSvc
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ILoggingService>();
            }
        }

        protected ICachingService CachingSvc
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ICachingService>();
            }
        }

        protected static string CESPassword { get; set; }

        protected static string SvcPassword { get; set; }

        static ExternalServiceBase()
        {
            string cesEncryptedPwd = ConfigurationManager.AppSettings.Get("CESServiceAccountPassword");
            if (!string.IsNullOrEmpty(cesEncryptedPwd))
                CESPassword = DecryptionHelper.DecryptString(cesEncryptedPwd);

            string svcEncryptedPwd = ConfigurationManager.AppSettings.Get("ServiceAccountPassword");
            if (!string.IsNullOrEmpty(svcEncryptedPwd))
                SvcPassword = DecryptionHelper.DecryptString(svcEncryptedPwd);

            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                (se, cert, chain, sslerror) => { return true; };

        }

        #region WS Service Invocation functions

        /// <summary>
        /// Adaptor function (action with no return value) for invoking the Web Service layer
        /// </summary>
        /// <typeparam name="IWebService"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="wsCallExpression"></param>
        protected void InvokeWebService<TWebService, TChannel, TResult>(Expression<Action<TChannel>> wsCallExpression, Action<Exception> exceptionHandler = null, bool isNtmlCreditalRequire = false)
            where TWebService : ClientBase<TChannel>, TChannel, new()
            where TChannel : class
        {

            try
            {
                var tClient = CreateServiceInstance<TWebService, TChannel>(isNtmlCreditalRequire);
                wsCallExpression.Compile().Invoke(tClient);
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null)
                {
                    exceptionHandler.Invoke(ex);
                }
                else
                {
                    var msg = string.Format("Exception in Service Gateway -> {0} -> {1}: {2}",
                        typeof(TChannel).ToString(), wsCallExpression.ToString(), ex.Message);
                    LoggingSvc.Log(new Exception(msg, ex), category: Category.Web);

                    throw;
                }
            }
        }

        protected TResult InvokeWebService<TWebService, TChannel, TResult>(Expression<Func<TChannel, TResult>> wsCallExpression, Action<Exception> exceptionHandler = null, bool isNtmlCreditalRequire = false)
            where TWebService : ClientBase<TChannel>, TChannel, new()
            where TChannel : class
        {
            TResult result = default(TResult);
            try
            {
                var tClient = CreateServiceInstance<TWebService, TChannel>(isNtmlCreditalRequire);

                // SP:  Comment Service Gateway performance diagnostics since we are using TPL
                //var timer = new System.Diagnostics.Stopwatch();
                //timer.Start();  
                result = wsCallExpression.Compile().Invoke(tClient);
                ////timer.Stop();

                //var msg = string.Format("PerformanceDiagnostics | {0} | {1} | {2}", "Service Invocation", wsCallExpression.ToString(), timer.ElapsedMilliseconds);
                //LoggingSvc.Log(msg, category: Category.BPM, severity: System.Diagnostics.TraceEventType.Verbose);
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null)
                {
                    exceptionHandler.Invoke(ex);
                }
                else
                {
                    var msg = string.Format("Exception in Service Gateway -> {0} -> {1}: {2}",
                        typeof(TChannel).ToString(), wsCallExpression.ToString(), ex.Message);
                    LoggingSvc.Log(new Exception(msg, ex), category: Category.Web);

                    throw;
                }
            }
            return result;
        }

        private TWebService CreateServiceInstance<TWebService, TChannel>(bool isNtmlCreditalRequire = false)
            where TWebService : ClientBase<TChannel>, TChannel, new()
            where TChannel : class
        {
            TWebService tClient = Activator.CreateInstance<TWebService>() as TWebService;
            ClientBase<TChannel> client = tClient as ClientBase<TChannel>;


            client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings.Get("ServiceAccountId");
            client.ClientCredentials.UserName.Password = SvcPassword;

            /* digest approach
            client.ClientCredentials.HttpDigest.ClientCredential = new NetworkCredential(ConfigurationManager.AppSettings.Get("ServiceAccountId"), svcPassword, "AD");
            client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            */

            if (isNtmlCreditalRequire)
            {
                client.ClientCredentials.Windows.ClientCredential =
                    new NetworkCredential(ConfigurationManager.AppSettings.Get("ServiceAccountId"), SvcPassword,
                        "AD");
                client.ClientCredentials.Windows.AllowedImpersonationLevel =
                    System.Security.Principal.TokenImpersonationLevel.Impersonation;
            }

            if (typeof(TChannel) ==
                typeof(Btmu.LoansApp.Common.CommonEntitlementServiceReference.IEntitlementService))
            {
                client.ClientCredentials.UserName.UserName =
                    ConfigurationManager.AppSettings.Get("CESServiceAccountId") ??
                    ConfigurationManager.AppSettings.Get("ServiceAccountId");
                client.ClientCredentials.UserName.Password = CESPassword ?? SvcPassword;
            }


            return tClient;
        }


        #endregion
    }
}