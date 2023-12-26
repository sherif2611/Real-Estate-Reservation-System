using RealEstate.Models;

namespace RealEstate.Repository
{
    public class SupportRepository
    {
        public List<Support> GetAll()
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var supports= context.Supports.ToList();
            return supports;
        }
        public Support GetById(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var support = context.Supports.FirstOrDefault(x=>x.Id==id);
            return support;
        }
        public void Insert(Support support)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            context.Supports.Add(support);
            context.SaveChanges();
        }
        public void UpdateSupport(int id,Support record)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var support=GetById(id);
            support.Phone=record.Phone;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var support = GetById(id);
            context.Supports.Remove(support);
            context.SaveChanges();
        }
    }
}