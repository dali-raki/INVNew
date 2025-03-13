using INV.App.Products;
using INV.App.Receipts;
using System.Data;
using INV.Domain.Entities.Products;
using INV.Domain.Entities.Receipts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace INV.Infrastructure.Storage.Products
{
    public class ProductStorage : IProductStorage
    {
        private readonly string _connectionString;

        public ProductStorage(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("INV");
        }

        private const string insertProductCommand = @"
            INSERT INTO [dbo].[PRODUCTS] ( Id, Designation,UnitMeasure,Quantity, UnitPrice, TVA)
            VALUES (@aId, @aDesignation,@aUnitMeasure, @aQuantity, @aUnitPrice, @aTVA)";

        private const string updateProductCommand = @"
            UPDATE [dbo].[PRODUCTS] SET Designation = @aDesignation, UnitMeasure = @aUnitMeasure,Quantity = @aQuantity,
            UnitPrice = @aUnitPrice, TVA = @aTVA  WHERE Id = @aId";

        private const string deleteProductCommand = @"
            Delete from [dbo].[PRODUCTS] where Id=@aId";

        private const string selectProductsQuery = @"
            SELECT * FROM [dbo].[PRODUCTS] ";

        private const string selectProductCountByIdQuery = @"
            SELECT count(*) FROM Products WHERE Designation = @aDesignation";

        private static Product getProductData(SqlDataReader reader)
        {
            return new Product
            {
                Id = (Guid)reader["Id"],
                Designation = (string)reader["Designation"],
                UnitMeasure = (string)reader["UnitMeasure"],
                Quantity = (int)reader["Quantity"],
                UnitPrice = (decimal)reader["UnitPrice"],
                TVA = (int)reader["TVA"],
            };
        }

        public async Task<int> InsertProduct(Product product)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(insertProductCommand, sqlConnection);
            await sqlConnection.OpenAsync();

            cmd.Parameters.AddWithValue("@aId", product.Id);
            cmd.Parameters.AddWithValue("@aDesignation", product.Designation);
            cmd.Parameters.AddWithValue("@aUnitMeasure", product.UnitMeasure);
            cmd.Parameters.AddWithValue("@aQuantity", product.Quantity);
            cmd.Parameters.AddWithValue("@aUnitPrice", product.UnitPrice);
            cmd.Parameters.AddWithValue("@aTVA", product.TVA);
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> UpdateProduct(Product product)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(updateProductCommand, sqlConnection);
            await sqlConnection.OpenAsync();

            cmd.Parameters.AddWithValue("@aId", product.Id);
            cmd.Parameters.AddWithValue("@aDesignation", product.Designation);
            cmd.Parameters.AddWithValue("@aUnitMeasure", product.UnitMeasure);
            cmd.Parameters.AddWithValue("@aQuantity", product.Quantity);
            cmd.Parameters.AddWithValue("@aUnitPrice", product.UnitPrice);
            cmd.Parameters.AddWithValue("@aTVA", product.TVA);

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteProduct(Guid id)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(deleteProductCommand, sqlConnection);
            await sqlConnection.OpenAsync();
            cmd.Parameters.AddWithValue("@aId", id);
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Product>> SelectProducts()
        {
            var products = new List<Product>();

            using var sqlConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(selectProductsQuery, sqlConnection);
            await sqlConnection.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var product = getProductData(reader);
                products.Add(product);
            }

            return products;
        }

        public async Task<bool> ProductExistsByaDesignation(string designation)
        {
            using var connection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(selectProductCountByIdQuery, connection);
            cmd.Parameters.AddWithValue("@aDesignation", designation);
            connection.Open();

            int count = (int)(await cmd.ExecuteScalarAsync() ?? 0);
            return count > 0;
        }

        public async ValueTask<ProductInfo> GetProductById(Guid productId)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync();

            using var cmd = new SqlCommand("[dbo].[GetProductById]", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@ProductId", productId);

            var dataSet = new DataSet();
            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataSet);

            if (dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                return null; // Aucun produit trouvé

            // Mapping du produit
            var productRow = dataSet.Tables[0].Rows[0];
            var product = new ProductInfo
            {
                Id = (Guid)productRow["Id"],
                Designation = productRow["Designation"].ToString(),
                UnitMeasure = productRow["UnitMeasure"].ToString(),
                Quantity = Convert.ToInt32(productRow["Quantity"]),
                UnitPrice = Convert.ToDecimal(productRow["UnitPrice"]),
                TVA = Convert.ToInt32(productRow["TVA"]),
                DefaultWareHouseId = (Guid)productRow["DefaultWareHouseID"],
                Rest = Convert.ToInt32(productRow["Rest"]),
                WareHouse = productRow["WareHouse"].ToString(),
                ReceiptInfos = new List<ReceiptInfo>()
            };

            // Mapping des réceptions
            if (dataSet.Tables.Count > 1)
            {
                foreach (DataRow row in dataSet.Tables[1].Rows)
                {
                    var receipt = new ReceiptInfo
                    {
                        Id = (Guid)row["Id"],
                        Number = row.IsNull("Number") ? null : row["Number"].ToString(),
                        PurchaseId = (Guid)row["PurchaseId"],
                        Date = row.IsNull("Date") ? default : DateOnly.FromDateTime((DateTime)row["Date"]),
                        DeliveryNumber = row.IsNull("DeliveryNumber") ? string.Empty : row["DeliveryNumber"].ToString(),
                        DeliveryDate = row.IsNull("DeliveryDate") ? default : DateOnly.FromDateTime((DateTime)row["DeliveryDate"]),
                        Status = (ReceiptStatus)row["Status"]
                    };
                    product.ReceiptInfos.Add(receipt);
                }
            }

            return product;
        }
    }
}