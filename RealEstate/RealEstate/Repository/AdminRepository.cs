using RealEstate.Models;
namespace RealEstate.Repository
{
    public class AdminRepository
    {
        public Admin GetByPersonId(int personId)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var admin=context.Admins.FirstOrDefault(x=>x.PersonId==personId);
            return admin;
        }
        public void Insert(Admin admin)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            context.Admins.Add(admin);
            context.SaveChanges();
        }
    }
}