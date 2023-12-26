namespace RealEstate.ViewModels
{
    public class BillViewModel
    {
        public string OwnerName {  get; set; }
        public string CustomerName {  get; set; }
        public string OwnerEmail {  get; set; }
        public string CustomerEmail { get; set; }
        public string RealEstateTitle {  get; set; }
        public string RealEstateDescription { get; set; }
        public DateTime StartDate {  get; set; }
        public DateTime EndDate { get; set; }
        public byte[] MainImg { get; set; }
        public decimal totalPrice {  get; set; }
    }
}