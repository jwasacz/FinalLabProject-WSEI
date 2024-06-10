using System.Runtime.Serialization;

namespace SoapService.DataContract
{
    [DataContract]
    public class CarWashData
    {
        [DataMember]
        public string? WashDate { get; set; } 
        [DataMember]
        public decimal Price { get; set; } 
        [DataMember]
        public WashType Type { get; set; } 
    }

    public enum WashType
    {
        Standard,
        Premium
    }
}
