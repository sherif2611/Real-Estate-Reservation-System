using RealEstate.Models;

namespace RealEstate.ViewModels
{
    public class GetRealEstateViewModel
    {
        public int Id { get; set; }
        public int userLoginId { get; set; }
        public int userId { get; set; }
        public int? userReserveId {  get; set; }
        public int? ownerId { get; set; }
        public int? customerId { get; set; }
        public int NumberOfDays {  get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Address { get; set; }
        public decimal Area { get; set; }
        public byte[] MainImg { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public virtual List<Imgs> Imgs { get; set; }
        public byte[] Credintial {  get; set; }
        public bool Confirmed {  get; set; }
    }
}
