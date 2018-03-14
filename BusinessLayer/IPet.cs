using BusinessLayer.ViewModel;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IPet
    {
        PetRetriever GetListOfPetsByPetType(PetType petType);
        List<PetOwnerGroupVM> GroupByGender();
        int Count();
        List<PetOwnerVM> GetPetList();
    }
}
