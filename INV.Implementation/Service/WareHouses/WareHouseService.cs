using INV.App.WareHouses;
using INV.Domain.Entities.WareHouses;
using INV.Domain.Shared;
using INV.Infrastructure.Storage.WareHousesStorages;

namespace INV.Implementation.Service.WareHouses;

public class WareHouseService(IWareHouseStorage wareHouseStorage) : IWareHouseService
{


    public async ValueTask<Result<List<WareHouse>>> GetAllReceipts()
    {
        try
        {
            return await wareHouseStorage.SelectAllReceipts();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async ValueTask<Result> CreateWareHouse(WareHouse wareHouse)
    {
        try
        {
            await wareHouseStorage.InsertWareHouse(wareHouse);
            return Result.Success();
        }
        catch (Exception e)
        {
            return Error.Exception(e);
        }
    }
}