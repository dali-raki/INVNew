using INV.Domain.Entities.WareHouses;

namespace INV.Infrastructure.Storage.WareHousesStorages;

public interface IWareHouseStorage
{
    ValueTask<List<WareHouse>> SelectAllReceipts();

    ValueTask<int> InsertWareHouse(WareHouse wareHouse);
}