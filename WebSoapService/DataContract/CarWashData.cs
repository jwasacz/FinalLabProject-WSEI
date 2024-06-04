using System.Runtime.Serialization;

namespace SoapService.DataContract
{
    [DataContract]
    public class CarWashData
    {
        [DataMember]
        public string? WashDate { get; set; } // Data mycia samochodu
        [DataMember]
        public decimal Price { get; set; } // Cena mycia
        [DataMember]
        public WashType Type { get; set; } // Rodzaj mycia: Standard lub Premium
    }

    public enum WashType
    {
        Standard,
        Premium
    }
}
