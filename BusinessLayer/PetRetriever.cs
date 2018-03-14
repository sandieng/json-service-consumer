using BusinessLayer.DomainModel;
using BusinessLayer.ViewModel;
using CommonLib;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class PetRetriever : IPet
    {
        private readonly IHttpClient _httpClient;
        private readonly List<PetOwnerVM> _petOwnerList = new List<PetOwnerVM>();

        public PetRetriever(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public PetRetriever GetListOfPetsByPetType(PetType petType)
        {           
            // Get the raw data from AGL service
            var jsonResponse = _httpClient.GetResultsAsync();
            List<PetOwner> ownerToPetList = JsonConvert.DeserializeObject<List<PetOwner>>(jsonResponse.Result);

            ownerToPetList
                .Where(x => x.Pets != null && x.Pets.Any(y => petType == PetType.All || y.Type == petType))
                .AsParallel()
                .ToList()
                .ForEach(x =>
                {
                    // Get the pet which is of 'petType', eg: Cat only, Dog only, Fish only, etc
                    _petOwnerList
                      .AddRange(x.Pets.Where(y => petType == PetType.All || y.Type == petType)
                      .Select(y => new PetOwnerVM
                      {
                          OwnerName = x.Name,
                          OwnerGender = x.Gender,
                          OwnerAge = x.Age,
                          PetName = y.Name,
                          PetType = y.Type
                      }));
                });

            return this;
        }

        public List<PetOwnerGroupVM> GroupByGender()
        {
            var groupResult = _petOwnerList.GroupBy(
             p => p.OwnerGender,
             p => p.PetName,
             (key, g) => new PetOwnerGroupVM { OwnerGender = key, PetName = g.ToList() });

            return groupResult.ToList();
        }

        public int Count()
        {
            return _petOwnerList.Count();
        }

        public List<PetOwnerVM> GetPetList()
        {
            return _petOwnerList;
        }
    }
}
