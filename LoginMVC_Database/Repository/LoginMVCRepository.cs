using LoginMVC_Database.Data;
using LoginMVC_Database.Interfaces;
using LoginMVC_Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginMVC_Database.Repository
{
    public class LoginMVCRepository : ILoginMVCRepository
    {
        private readonly LoginMVC_DbContext _context;

        public LoginMVCRepository(LoginMVC_DbContext context)
        {
            _context = context;
        }

        public bool AddFakePerson(FakePersonData fakePerson)
        {
            _context.Add(fakePerson);
            return Save();
        }

        public bool DeleteFakePerson(int id)
        {
            _context.Remove(id);
            return Save();
        }

        public bool UpdateFakePerson(FakePersonData fakePerson)
        {
            _context.FakePeople.Update(fakePerson);
            return Save();
        }

        public async Task<IEnumerable<FakePersonData>> GetAllFakePeople()
        {
            return await _context.FakePeople.ToListAsync();
        }

        public async Task<FakePersonData> GetIdAsync(int id)
        {
            return await _context.FakePeople.FirstOrDefaultAsync(i => i.Id == id); //Use .Include() before FirstOrDefaultAsync() if the entity has any related entities.
        }

        public async Task<IEnumerable<FakePersonData>> GetPersonByFirstName(string firstName)
        {
            return await _context.FakePeople.Where(f => f.FirstName.Contains(firstName)).ToListAsync();
        }

        public async Task<IEnumerable<FakePersonData>> GetPersonByLastName(string lastName)
        {
            return await _context.FakePeople.Where(l => l.LastName.Contains(lastName)).ToListAsync();
        }

        public bool Save()
        {
           var saved = _context.SaveChanges();
           return saved > 0 ? true : false;    
        }

       
    }
}
