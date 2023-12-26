using RealEstate.Models;

namespace RealEstate.Repository
{
    public class PersonRepository
    {
        public List<Person> GetAllPeople()
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var people=context.People.ToList();
            return people;
        }
        public Person GetPersonById(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var person = context.People.Where(x =>x.Id == id).FirstOrDefault();
            return person;
        }
        public Person GetPersonByEmail(string email)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var person = context.People.Where(x => x.Email == email).FirstOrDefault();
            return person;
        }
        public string GetPersonRole(string email)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var personRole = context.People.Where(x => x.Email == email).FirstOrDefault().Role;
            return personRole;
        }
        public void Insert(Person person) 
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            context.People.Add(person);
            context.SaveChanges();
        }
        public void Update(int id,Person record)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var person=context.People.FirstOrDefault(x => x.Id == id);
            person.FName= record.FName;
            person.LName= record.LName;
            person.Email= record.Email;
            person.Password= record.Password;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var person=context.People.FirstOrDefault(x=>x.Id == id);
            context.People.Remove(person);
            context.SaveChanges();
        }
        public List<Person> GetAllUsersInfo()
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var users=context.People.Where(a=>a.Role=="user").ToList();
            return users;
        }
    }
}