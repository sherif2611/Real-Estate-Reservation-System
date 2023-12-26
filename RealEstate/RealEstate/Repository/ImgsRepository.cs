using RealEstate.Models;

namespace RealEstate.Repository
{
    public class ImgsRepository
    {
        public List<Imgs> GetImgs(int id)//id => realEstateId
        {
            RealEstateReservationDbContext context= new RealEstateReservationDbContext();
            var imgs=context.Imgs.Where(x=>x.RealEstateId==id).ToList();
            return imgs;
        }
        public void Insert(Imgs img)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            context.Imgs.Add(img);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var img =context.Imgs.FirstOrDefault(x=>x.Id==id);
            context.Imgs.Remove(img);
            context.SaveChanges();
        }
        public void DeleteAllById(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var all = context.Imgs.Where(a => a.RealEstateId == id);
            context.Imgs.RemoveRange(all);
            context.SaveChanges();
        }
    }
}
