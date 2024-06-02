using LoginMVC_Database.Models;


namespace LoginMVC_Database.Interfaces
{
    public interface ILoginMVCRepository
    {

        Task<IEnumerable<FakePersonData>> GetAllFakePeople();
        Task<FakePersonData> GetIdAsync(int id);
        Task<IEnumerable<FakePersonData>> GetPersonByFirstName(string firstName);
        Task<IEnumerable<FakePersonData>> GetPersonByLastName(string lastName);
        bool AddFakePerson(FakePersonData fakePerson);
        bool UpdateFakePerson(FakePersonData fakePerson);
        bool DeleteFakePerson(int id);
        bool Save();


    }
}
