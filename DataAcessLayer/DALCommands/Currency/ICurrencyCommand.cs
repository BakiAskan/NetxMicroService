using Models.RequestModel;

namespace DataAcessLayer.DALCommands.Currency
{
    public interface ICurrencyCommand
    {
        public Task<IResultMessages<dynamic>> WriteCurrency(RequestCurrency request);


    }
}
