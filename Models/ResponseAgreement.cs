using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace routingAgreement.Models
{
    [DataContract]
    [Serializable]
    public class Agreement
    {
        [DataMember(Name = "id")]
        [Description("")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        [Description("")]
        public string Name { get; set; }

        [DataMember(Name = "invoiceid")]
        [Description("")]
        public int InvoiceId { get; set; }

        [DataMember(Name = "operationservice")]
        [Description("")]
        public string OperationService { get; set; }

        [DataMember(Name = "url")]
        [Description("")]
        public string Url { get; set; }

        [DataMember(Name = "operation")]
        [Description("")]
        public string Operation { get; set; }

        [DataMember(Name = "type")]
        [Description("tipo de servicio")]
        public string Type { get; set; }
    }

    [DataContract]
    [Serializable]
    public class ResponseAgreement : ResponseBase
    {
        [DataMember(Name = "date")]
        public Agreement Data { get; set; }
    }
}