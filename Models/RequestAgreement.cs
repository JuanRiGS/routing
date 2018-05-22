using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace routingAgreement.Models
{
    [DataContract]
    [Serializable]
    public class RequestAgreement
    {
        [Required]
        [DataMember(Name = "id")]
        [Description("Id del convenio")]
        public string Id { get; set; }

        [Required]
        [DataMember(Name = "name")]
        [Description("Nombre del convenio")]
        public string Name { get; set; }

        [Required]
        [DataMember(Name = "type")]
        [Description("tipo de servicio")]
        public string Type { get; set; }

        [Required]
        [DataMember(Name = "operationservice")]
        [Description("tipo de servicio")]
        public string OperationService { get; set; }

        [Required]
        [DataMember(Name = "url")]
        [Description("endpoint del convenio")]
        public string Url { get; set; }

        [Required]
        [DataMember(Name = "operation")]
        [Description("operacion")]
        public string Operation { get; set; }
    }
}