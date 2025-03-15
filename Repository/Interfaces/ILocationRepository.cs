using Repository.Models;

namespace Repository.Interfaces;

public interface ILocationRepository
{
    List<Country> GetCountries();
    List<State> GetStates(int countryId);
    List<City> GetCities(int stateId);
}
