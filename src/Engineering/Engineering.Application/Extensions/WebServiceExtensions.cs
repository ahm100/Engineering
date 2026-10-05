using Financial.Application.WebServices.MetaDataServices.Companies.Models;
using Financial.Application.WebServices.MetaDataServices.Companies.Queries.GetCompaniesByIds;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Models;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetsUserById;
using Gita.Backend.Shared.Application.WebServices.MetaDataServices.Currencies.Queries.GetsCurrencyById;
using System.Linq.Dynamic.Core;

namespace Financial.Application.Extensions;

public static class WebServiceExtensions
{
    public static IEnumerable<TInput>? SetCurrencyNames<TInput>(this IEnumerable<TInput>? input, IMediator mediator) where TInput : class
    {
        if (input is not null && input.Count() > 0)
        {
            var result = input?.AsQueryable().Select("CurrencyId").ToDynamicList();
            var currencyIds = result.Adapt<List<long>>();

            var currencyQuery = mediator.Send(new GetsCurrencyByIdQuery(1, currencyIds.Count, currencyIds)).ConfigureAwait(false).GetAwaiter().GetResult();
            var currencies = currencyQuery.Value?.Data!;

            foreach (var item in input!)
            {
                var currencyId = item.GetPropertyValue<long>("CurrencyId");
                var value = currencies.ExtractValue(currencyId)?.Name;
                item.SetPropertyValue("CurrencyName", value);
            }

            return input;
        }

        return null;
    }

    public static IEnumerable<TInput>? SetCompanyNames<TInput>(this IEnumerable<TInput>? input, IMediator mediator) where TInput : class
    {
        if (input is not null && input.Count() > 0)
        {
            var result = input?.AsQueryable().Select("CompanyId").ToDynamicList();
            var companyIds = result.Adapt<List<long>>();

            var currencyQuery = mediator.Send(new GetCompaniesByIdsQuery(companyIds, 1, companyIds.Count)).ConfigureAwait(false).GetAwaiter().GetResult();
            var currencies = currencyQuery.Value?.Data!;

            foreach (var item in input!)
            {
                var currencyId = item.GetPropertyValue<long>("CompanyId");
                var value = currencies.ExtractValue(currencyId)?.NameFa;
                item.SetPropertyValue("CompanyName", value);
            }

            return input;
        }

        return null;
    }

    public static IEnumerable<CompanyModel>? GetCompaniesByIds(this List<long>? companyIds, IMediator mediator)
    {
        if (companyIds is not null && companyIds.Count() > 0)
        {

            var getCompaniesByIdsQuery = mediator.Send(new GetCompaniesByIdsQuery(companyIds, 1, companyIds.Count)).ConfigureAwait(false).GetAwaiter().GetResult();
            var companies = getCompaniesByIdsQuery.Value?.Data!;

            return companies;
        }

        return null;
    }

    public static User? GetUser(long? id, IMediator mediator)
    {
        if (id is not null)
        {
            var query = mediator.Send(new GetsUserByIdQuery(new List<long>() { id!.Value }), default).ConfigureAwait(false).GetAwaiter().GetResult();
            var response = query.Value?.Data?.FirstOrDefault();
            return response;
        }
        return null;
    }
}
