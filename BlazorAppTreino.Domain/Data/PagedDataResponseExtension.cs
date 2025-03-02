using BlazorAppTreino.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Data
{
    public static class PagedDataResponseExtension
    {
        public static async Task<PagedDataResponse<TModel>> PaginateAsync<TModel>(
        this IQueryable<TModel> query,
        PagedDataRequest pagedDataRequest
        )
        where TModel : class

        {

            var paged = new PagedDataResponse<TModel>();

            pagedDataRequest.Page = pagedDataRequest.Page < 0 ? 1 : pagedDataRequest.Page;

            paged.Page = pagedDataRequest.Page;
            paged.PageSize = pagedDataRequest.Limit;

            var totalItemsCountTask = await query.CountAsync();

            var startRow = (pagedDataRequest.Page - 1) * pagedDataRequest.Limit;

            if (startRow > 0)
                query = query.Skip(startRow);

            paged.Items = await query
                       .Take(pagedDataRequest.Limit)
                       .ToListAsync();

            paged.TotalItens = totalItemsCountTask;
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItens / (double)pagedDataRequest.Limit);

            return paged;
        }
    }
}
