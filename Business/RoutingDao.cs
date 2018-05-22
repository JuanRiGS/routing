using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using routingAgreement.Models;
using System.IO;

namespace routingAgreement.Business
{
    public class RoutingDao
    {
        public string ConnectionString { get; set; }

        public RoutingDao(string Connection)
        {
            ConnectionString = Connection;
        }

        private MySqlConnection Get()
        {
            return new MySqlConnection(ConnectionString);
        }


        public ResponseAgreement AddRoutingAgreement(RequestAgreement data, string webrootpath)
        {
            var model = new Agreement();
            var result = new ResponseAgreement();

            try
            {
                 var routesfile = File.AppendText(webrootpath + "/agreements");
                 var routeline = "|"+data.Id+","+data.Name+","+data.Url+","+data.OperationService+","+data.Type+","+data.Operation;
                    routesfile.Write(routeline);
                    routesfile.Close();

                    result.Code = 200;
                    result.Data = new Agreement()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Type = data.Type,
                        OperationService = data.OperationService,
                        Operation = data.Operation,
                        Url = data.Url,
                    };

                   
                    result.Message = "OK";                
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
                result.Data = null;
            }

            return (result);
        }
        public ResponseAgreement  GetRoutingAgreement(string invoiceid, string webrootpath)
        {
            var model = new Agreement();
            var result = new ResponseAgreement();

            try
            {
                var routesfile = File.ReadAllText(webrootpath + "/agreements");

                var routes  = routesfile.Split('|');

                for (int i=0; i<routes.Length; i++)
                {
                   bool service =  routes[i].StartsWith(invoiceid.Substring(0,2));
                   bool operation = routes[i].EndsWith(invoiceid.Substring(2,2));
                    if(service & operation)
                    {                        
                        var lstroute = routes[i].Split(',');
                        model.Id = invoiceid;
                        model.InvoiceId = Convert.ToInt32(invoiceid.Substring(4,5));
                        model.Name = lstroute[1];
                        model.Url = lstroute[2];
                        model.OperationService = lstroute[3];
                        model.Type = lstroute[4];
                        model.Operation = lstroute[5];
                        break;
                    }
                }              
                          
                    result.Code = 200;
                    result.Data = model;
                    result.Message = "OK";
               
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
                result.Data = null;
            }

            return (result);
        }
    }
}