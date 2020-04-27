using System.Runtime.Serialization;

namespace API.Models
{
    [DataContract]
    public class OrderDetail
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public string Size { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public decimal Total => Cost * Quantity;
        [DataMember]
        public string Description { get; set; }
    }
}
