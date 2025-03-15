using INV.Domain.Entities.WareHouse;
using INV.Domain.Shared;

namespace INV.App.WareHouses
{
    public interface IWareHouseService
    {
        ValueTask<Result<List<WareHouse>>> GetAllReceipts();

        ValueTask<Result> CreateWareHouse(WareHouse wareHouse);
    }
}