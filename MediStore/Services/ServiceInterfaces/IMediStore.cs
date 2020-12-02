using MediStore.Models;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediStore.Services.ServiceInterfaces
{
    public interface IMediStore
    {
        public Task<List<MedicineModel>> GetAllMedicinesAsync();

        public Task<MedicineModel> GetMedicineByIdAsync(string medicineId);

        public Task<bool> DeleteMedicineByNameAsync(string medicineId);

        public Task<HttpStatusCode> AddMedicineAsync(MedicineModel medicine);

        public Task<HttpStatusCode> UpsertMedicineAsync(MedicineModel medicine);
    }
}
