using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;

namespace Service.Implementations;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _regionRepository;
    public LocationService(ILocationRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public List<Country> GetCountries()
        {
            return _regionRepository.GetCountries();
        }

        public List<State> GetStates(int countryId)
        {
            return _regionRepository.GetStates(countryId);
        }

        public List<City> GetCities(int stateId)
        {
            return _regionRepository.GetCities(stateId);
        }
}
