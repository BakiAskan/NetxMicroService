using Models.RequestModel;

namespace DataAcessLayer.DALCommands.Currency
{
    public class CurrencyCommand(ICommands commands) : ICurrencyCommand
    {
        public async Task<IResultMessages<dynamic>> WriteCurrency(RequestCurrency request)
        {
            return await commands.CommandProcExecuteReturnValue(@"DECLARE @AutoID integer EXEC @AutoID=dbo.CurrencyWrite @dovizid="+request.dovizid+",@UID= '"+request.UID+"',@dovizkodu='"+request.dovizkodu+"',@aciklama =N'"+request.aciklama+"',@RU=N'"+request.RU+"' SELECT @AutoID");
        }
    }
}
