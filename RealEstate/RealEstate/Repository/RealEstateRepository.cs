using RealEstate.Models;
using System;
using System.Xml;
namespace RealEstate.Repository
{
    public class RealEstateRepository
    {
        public List<Models.RealEstate> GetAll()
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var realEstate = context.RealEstates.Where(a=>a.Confirmed==true).ToList();
            return realEstate;
        }
        public List<Models.RealEstate> GetWaitingPosts()
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var realEstate = context.RealEstates.Where(a => a.Confirmed == false).ToList();
            return realEstate;
        }
        public Models.RealEstate GetById(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var realEstate= context.RealEstates.FirstOrDefault(x => x.Id == id);
            return realEstate;
        }
        public List<Models.RealEstate> GetUserRealEstate(int userId)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var realEstate = context.RealEstates.Where(a => a.UserId==userId).ToList();
            return realEstate;
        }
        public List<Models.RealEstate> GetCustomerReservations(int customerId)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var IDs=context.RealEstateReservations.Where(a=>a.UserId==customerId).Select(a=>a.RealEstateId).ToList();
            List<Models.RealEstate> realEstates =new List<Models.RealEstate>();
            for (int i = 0;i<IDs.Count; i++)
            {
                var realestate = context.RealEstates.FirstOrDefault(a => a.Id == IDs[i]);
                realEstates.Add(realestate);
            }
            return realEstates;
        }
        public void Insert(Models.RealEstate record)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            context.RealEstates.Add(record);
            context.SaveChanges();
        }
        public void Update(int id,Models.RealEstate record)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var realEstate=GetById(id);
            realEstate.Desc=record.Desc;
            realEstate.Credentail=record.Credentail;
            realEstate.MainImg=record.MainImg;
            realEstate.Price=record.Price;
            realEstate.Address=record.Address;
            realEstate.Category=record.Category;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var realEstate = GetById(id);
            context.RealEstates.Remove(realEstate);
            context.SaveChanges();
        }
        public int IdOfLastRealEstateInserted()
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var lastRealEstate = context.RealEstates.OrderByDescending(r => r.Id).FirstOrDefault();
            var id = lastRealEstate.Id;
            return id;
        }
        public List<Models.RealEstate> SearchByArea(decimal area)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var list=context.RealEstates.Where(a=> a.Area== area && a.Confirmed == true).ToList();
            return list;
        }
        public List<Models.RealEstate> SeaechByAddress(string address)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var list = context.RealEstates.Where(a => a.Address.Contains(address)&&a.Confirmed==true).ToList();
            return list;
        }
        public void ChangeAvailabilty(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var obj=context.RealEstates.FirstOrDefault(a=>a.Id==id);
            obj.Available=!obj.Available;   
            context.SaveChanges();
        }
        public int GetOwnerIdByRealEstateId(int RealEstateId)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            int id = context.RealEstates.FirstOrDefault(a => a.Id == RealEstateId).UserId;
            return id;
        }
        public void Confirm(int id)
        {
            RealEstateReservationDbContext context = new RealEstateReservationDbContext();
            var real=context.RealEstates.FirstOrDefault(a=>a.Id== id);
            real.Confirmed = true;
            context.SaveChanges();
        }
    }
}