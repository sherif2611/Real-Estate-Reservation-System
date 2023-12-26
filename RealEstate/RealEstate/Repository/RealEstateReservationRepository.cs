using RealEstate.Models;

namespace RealEstate.Repository
{
    public class RealEstateReservationRepository
    {
        public void Insert(RealEstateReservations obj)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            context.RealEstateReservations.Add(obj);
            context.SaveChanges();
        }
        public RealEstateReservations GetByRealEstateId(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var realReservation=context.RealEstateReservations.FirstOrDefault(a=>a.RealEstateId==id);
            return realReservation;
        }
        public List<RealEstateReservations> GetAll()
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var reservations=context.RealEstateReservations.ToList();
            return reservations;
        }
        public void Delete(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var reservation = context.RealEstateReservations.FirstOrDefault(a => a.RealEstateId == id);
            context.RealEstateReservations.Remove(reservation);
        }
    }
}