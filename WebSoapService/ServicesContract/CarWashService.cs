using SoapService.DataContract;
using SoapService.ServiceContract;


public class CarWashService : ICarWashService
{
    public string RegisterCarWashData(CarWashData carWashData)
    {
        if (Validate(carWashData))
        {
            return $"Dane mycia zarejestrowane: Data: {carWashData.WashDate}, Cena: {carWashData.Price}, Rodzaj: {carWashData.Type}";
        }
        return "Nie można zarejestrować danych mycia.";
    }

    private bool Validate(CarWashData carWashData)
    {
        if (carWashData == null)
            return false;
        else if (string.IsNullOrEmpty(carWashData.WashDate) || carWashData.Price <= 0)
            return false;
        else
            return true;
    }
}
