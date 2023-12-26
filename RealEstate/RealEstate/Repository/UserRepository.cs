using RealEstate.Models;
namespace RealEstate.Repository
{
    public class UserRepository
    {
        public List<Person> GetAll()//Get People which Role="user"
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var users= context.People.Where(x=>x.Role=="user").ToList();
            return users;
        }
        public Person GetUserDetails(int id)//id = personId // Get user details by person
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var user = context.People.SingleOrDefault(x => x.Id==id);
            return user;
        }
        public User GetByPersonId(int personId)//Get user
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var user = context.Users.FirstOrDefault(x=>x.PersonId==personId);
            return user;
        }
        public int GetPersonId(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var personId=context.Users.FirstOrDefault(x => x.Id == id).PersonId;
            return personId;
        }
        public void Insert(User user)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User GetById(int id)//Get user
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            return user;
        }
        public void Delete(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var user=GetById(id);
            var reservations = context.RealEstateReservations.Where(a => a.UserId == user.Id);
            context.RealEstateReservations.RemoveRange(reservations);
            var reals=context.RealEstates.Where(a=>a.UserId == user.Id);
            context.RemoveRange(reals);
            context.SaveChanges();
            var personId=user.PersonId;
            context.Users.Remove(user);
            PersonRepository personRepository = new PersonRepository();
            personRepository.Delete(personId);
        }
    }
}