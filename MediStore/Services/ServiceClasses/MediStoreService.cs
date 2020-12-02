using MediStore.Models;
using MediStore.Services.ServiceInterfaces;
using System;
using System.Net;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using System.Linq;

namespace MediStore.Services.ServiceClasses
{
    public class MediStoreService : IMediStore
    {
        // In Realtime we should store this sesitive information in Azure Keyvalut 
        private const string EndpointUrl = "https://medistore.documents.azure.com:443/";
        private const string AuthorizationKey = "EbHtIypPywPj6kyTGbjE5xrjhLphHvJF2gGAcyCSaLoYP591NmKTbmtYydyBH7UtVT7yZKWB8rUtmcF2voIccg==";
        private const string DatabaseId = "MediStoreDatabase";
        private const string ContainerId = "MedistoreContainer";


        CosmosClient cosmosClient = new CosmosClient(EndpointUrl, AuthorizationKey);

        public async Task<List<MedicineModel>> GetAllMedicinesAsync()
        {
            try
            {
                Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);
                var sqlQueryText = "SELECT * FROM c ";

                Console.WriteLine("Running query: {0}\n", sqlQueryText);

                QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                FeedIterator<MedicineModel> queryResultSetIterator = container.GetItemQueryIterator<MedicineModel>(queryDefinition);

                List<MedicineModel> medicines = new List<MedicineModel>();

                while (queryResultSetIterator.HasMoreResults)

                {
                    FeedResponse<MedicineModel> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (MedicineModel medicine in currentResultSet)
                    {
                        medicines.Add(medicine);
                        Console.WriteLine("\tRead {0}\n", medicine);
                    }
                }
                return medicines;
            }
            catch (Exception ex)
            {
                throw new CosmosException(ex.Message, System.Net.HttpStatusCode.BadRequest, 0, "", 0);
            }
        }

        public async Task<MedicineModel> GetMedicineByIdAsync(string medicineId)
        {
            try
            {
                Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);
                IQueryable<MedicineModel> queryable = container.GetItemLinqQueryable<MedicineModel>(true);
                queryable = queryable.Where<MedicineModel>(item => item.id == medicineId);
                return queryable.ToArray().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new CosmosException(ex.Message, System.Net.HttpStatusCode.BadRequest, 0, "", 0);
            }
        }

        public async Task<bool> DeleteMedicineByNameAsync(string medicineId)
        {
            try
            {
                Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);
                ItemResponse<MedicineModel> response = await container.DeleteItemAsync<MedicineModel>(medicineId, new PartitionKey(medicineId));
                return true;
            }
            catch (Exception ex)
            {
                throw new CosmosException(ex.Message, System.Net.HttpStatusCode.BadRequest, 0, "", 0);
            }
        }

        public async Task<HttpStatusCode> AddMedicineAsync(MedicineModel medicine)
        {
            try
            {
                Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);
                // Create an item in the container representing the Medicine. Note we provide the value of the partition key for this item, which is "medicinename".
                ItemResponse<MedicineModel> response = await container.CreateItemAsync<MedicineModel>(medicine, new PartitionKey(medicine.id));

                return response.StatusCode;
            }
            catch (Exception ex)
            {
                throw new CosmosException(ex.Message, System.Net.HttpStatusCode.BadRequest, 0, "", 0);
            }
        }

        public async Task<HttpStatusCode> UpsertMedicineAsync(MedicineModel medicine)
        {
            try
            {
                Container container = cosmosClient.GetContainer(DatabaseId, ContainerId);
                // Create an item in the container representing the Medicine. Note we provide the value of the partition key for this item, which is "medicinename".
                ItemResponse<MedicineModel> response = await container.UpsertItemAsync<MedicineModel>(medicine, new PartitionKey(medicine.id));

                return response.StatusCode;
            }
            catch (Exception ex)
            {
                throw new CosmosException(ex.Message, System.Net.HttpStatusCode.BadRequest, 0, "", 0);
            }
        }


    }
}
