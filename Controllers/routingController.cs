using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using routingAgreement.Models;
using routingAgreement.Common;
using routingAgreement.Business;

namespace routingAgreement.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RoutingController : Controller
    {
        private string reqAgreementSchema = "";
        private string respAgreementSchema = "";
        private string webRootPath="";

       public RoutingController(IHostingEnvironment hostingEnvironment)
       {
           webRootPath = hostingEnvironment.WebRootPath;

           reqAgreementSchema = webRootPath + "/Schema/request-agreement-schema.json";
           respAgreementSchema = webRootPath + "/Schema/response-agreement-schema.json";
       }

        /// <summary>Consultarla ruta  de un convenio</summary>
        /// <remarks>Consulta la informacion de convenio de la factura ingresada</remarks>
        /// <param name="invoicereferencia"></param>
        /// <returns>Retorna la informacion del convenio a enrutar</returns>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>    
        [HttpGet]
        [Route("getAgreementrouting/{invoicereferencia}")]
        [ProducesResponseType(typeof(ResponseAgreement), 200)]
        [ProducesResponseType(500)]
        public ResponseAgreement GetRouting(string invoicereferencia)
        {
            var serv = new RoutingBusiness(HttpContext);
            var result = serv.GetRoutingAgreement(invoicereferencia,webRootPath);

            if (result.Code == 200)
            {
                var validate = SchemaEngine.Validate<ResponseAgreement>(result, respAgreementSchema);

                if (!validate)
                {
                    result.Code = 500;
                    result.Message = "Wrong Definition";
                    result.Data = null;

                    return (result);
                }
            }
            else
            {
                return (result);
            }

            return result;
        }

        /// <summary>Registra el routing de un convenio</summary>
        /// <remarks>Registra el routing para el convenio de la factura solicitada</remarks>
        /// <param name="data"></param>
        /// <returns>Retorna los datos del convenio registrado</returns>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>    
        [HttpPost]
        [Route("addroutingAgreement")]
        [ProducesResponseType(typeof(ResponseAgreement), 200)]
        [ProducesResponseType(500)]
        public ResponseAgreement AddRoutingAgreement([FromBody] RequestAgreement data)
        {
            var result = new ResponseAgreement();
            var validateRequest = SchemaEngine.Validate<RequestAgreement>(data, reqAgreementSchema);

            if (validateRequest)
            {
                var serv = new RoutingBusiness(HttpContext);
                result = serv.AddRoutingAgreement(data,webRootPath);

                if (result.Code == 200)
                {
                    var validate = SchemaEngine.Validate<ResponseAgreement>(result, respAgreementSchema);

                    if (!validate)
                    {
                        result.Code = 500;
                        result.Message = "Invalid result data schema";
                        result.Data = null;

                        return result;
                    }
                }
                else
                {
                    return result;
                }

                return result;
            }
            else
            {
                result.Code = 500;
                result.Message = "Invalid params schema";
                result.Data = null;

                return result;
            }
        }        
    }
}
