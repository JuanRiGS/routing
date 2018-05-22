using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using routingAgreement.Models;

namespace routingAgreement.Business
{
    public class RoutingBusiness
    {
        private HttpContext _httpContext;
        public RoutingBusiness(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }
        public ResponseAgreement AddRoutingAgreement(RequestAgreement data,string webrootpath)
        {
            var context = _httpContext.RequestServices.GetService(typeof(RoutingDao)) as RoutingDao;
            var response = context.AddRoutingAgreement(data,webrootpath);

            return (response);
        }

        public  ResponseAgreement GetRoutingAgreement(string invoiceref,string webrootpath)
        {
            //var key = invoiceref.Substring(0, 2);
            var context = _httpContext.RequestServices.GetService(typeof(RoutingDao)) as RoutingDao;
            var response = context.GetRoutingAgreement(invoiceref,webrootpath);

            return  (response);
        }

    }
}