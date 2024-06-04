using SoapService.DataContract;
using System.ServiceModel;
using System.Xml.Linq;


namespace SoapService.ServiceContract
{
    [ServiceContract]
    public interface ICarWashService
    {
        [OperationContract]
        public string RegisterCarWashData(CarWashData carWashData);
    }
}