namespace BusinessLayers.BLCommands.Abstract
{
    public interface IBLCurrencyCommand
    {
        public Task<IResultMessages<dynamic>> WriteCurrency(RequestCurrency request);
    }
}
