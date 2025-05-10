using BusinessLayers.BLCommands.Abstract;
using DataAcessLayer.DALCommands.Currency;

namespace BusinessLayers.BLCommands
{
    public class BLCurrencyCommand(ICurrencyCommand currencyCommand) : IBLCurrencyCommand
    {
        public async Task<IResultMessages<dynamic>> WriteCurrency(RequestCurrency request)
        {
             return await currencyCommand.WriteCurrency(request);
        }
    }
}
