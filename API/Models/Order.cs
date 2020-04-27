using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace API.Models
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public long? RowCounts { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string OrderCode { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string StaffName { get; set; }
        [DataMember]
        public double TotalCost { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public string CreateDateTime => CreatedDate.HasValue ? CreatedDate.Value.ToString("dd-MM-yyyy HH:mm ") : "";
        [DataMember]
        public List<OrderDetail> OrderDetails { get; set; }
        [DataMember]
        public DateTime? CancelDate { get; set; }
        [DataMember]
        public string CancelDateTime => CancelDate.HasValue ? CancelDate.Value.ToString("dd-MM-yyyy HH:mm ") : "";
        [DataMember]
        public string CancelBy { get; set; }
        [DataMember]
        public OrderStatus OrderStatus { get; set; }
        [DataMember]
        public string OrderStatusDisplay
        {
            get
            {
                var description = "";
                switch (OrderStatus)
                {
                    case OrderStatus.PosFinished:
                        description = "Hoàn tất";
                        break;
                    case OrderStatus.PreCancel:
                        description = "Hủy";
                        break;
                    case OrderStatus.Cancel:
                        description = "Hủy";
                        break;
                    case OrderStatus.PosCancel:
                        description = "Hủy";
                        break;
                    case OrderStatus.PosPreCancel:
                        description = "Hủy";
                        break;
                }
                return description;
            }
        }
        [DataMember]
        public DiscountType DiscountType { get; set; }
        [DataMember]
        public string DiscountTypeDisplay
        {
            get
            {
                var description = "";
                switch (DiscountType)
                {
                    case DiscountType.OneGetOne:
                        description = "1 tặng 1";
                        break;
                    case DiscountType._20kDiscount:
                        description = "20.000";
                        break;
                    case DiscountType.FiveGetOne:
                        description = "Mua 5 tặng 1";
                        break;
                    case DiscountType.Delivery:
                        description = "Delivery";
                        break;
                    case DiscountType.Sharing:
                        description = "Sharing";
                        break;
                }
                return description;
            }
        }
    }

    public enum OrderStatus
    {
        New = 8,
        PosProcessing = 17,
        Processing = 10,
        PosFinished = 11,
        Finish = 2,
        PosCancel = 13,
        Cancel = 3,
        PosPreCancel = 12,
        PreCancel = 4
    }

    public enum DiscountType
    {
        None = 0,
        OneGetOne = 1,
        _20kDiscount = 2,
        FiveGetOne = 3,
        Delivery = 4,
        Sharing = 5,
        UseActiveBonus = 6
    }
}
