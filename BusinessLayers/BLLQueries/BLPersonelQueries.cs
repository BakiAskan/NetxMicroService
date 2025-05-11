using BusinessLayers.BLLQueries.Abstract;
using DataAcessLayer.Queries.Personels;

namespace BusinessLayers.BLLQueries
{
    [AOPLog]
    public class BLPersonelQueries(IPersonelQueries personel) : IBLPersonelQueries
    {
        public async Task<IResultMessages<dynamic>> Deneme(RequestLogin model)
        {
            return ResultMessages<dynamic>.SuccessMessage(new List<string>() { "Kullanıcı Adı veya Şifre Hatalı" }, HttpStatusCode.OK);
        }

        public async Task<IResultMessages<dynamic>> PersonelLogin(RequestLogin model)
        {
            var result = await new ValidPersonelLogin().ValidateAsync(model);
            if (result.IsValid)
            {
                var data = personel.PersonelLogin(model.Username, model.Password).Result;
                if (data.DataResult is null)
                {
                    return ResultMessages<dynamic>.SuccessMessage(null, HttpStatusCode.OK, new List<string>() { "Kullanıcı Adı veya Şifre Hatalı" });
                }
                else
                {
                    return await personel.PersonelLogin(model.Username, model.Password);
                }
            }
            else
            {
                return ResultMessages<dynamic>.ErrorMessage(ValidateToArrayList.Convert(result.Errors.ToList()), HttpStatusCode.BadRequest);
            }
        }
        public async Task<IResultMessages<dynamic>> PersonelInfo(int PersonelId)
        {
            return await personel.PersonelInfo(PersonelId);
        }
              
    }
}
