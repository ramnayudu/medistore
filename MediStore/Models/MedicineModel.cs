using System;

namespace MediStore.Models
{
    public class MedicineModel
    {
        public string id { get; set; }
        public string medicineName { get; set; }

        public string brand { get; set; }

        public decimal price { get; set; }

        public int quantity { get; set; }
        public DateTime expiryDate { get; set; }

        public string notes { get; set; }

    }
}
