using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Excalibur.AspNetCore.Business.Interfaces;
using Excalibur.AspNetCore.Extensions;
using Excalibur.AspNetCore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Excalibur.AspNetCore.Business
{
    public abstract class BaseDataTablesBusiness<TEntity, TViewModel> : BaseBusiness, IBaseDataTablesInterface where TEntity : class where TViewModel : class
    {
        protected IMapper Mapper { get; set; }

        protected BaseDataTablesBusiness(DbContext dbContext, IMapper mapper) : base(dbContext)
        {
            Mapper = mapper;
        }

        /// <summary>
        /// Generic method for retrieving all relevant entities <see cref="TEntity"/> from the database, paginated, sorted and filtered as specified in the <see cref="IDataTablesRequest"/> request.
        /// </summary>
        /// <param name="request">A <see cref="IDataTablesRequest"/> request</param>
        /// <returns>A <see cref="DataTablesResponse"/> response</returns>
        public async Task<DataTablesResponse> IndexDt(IDataTablesRequest request)
        {
            var result = new SearchResult<TViewModel>()
            {
                TotalResults = 0,
                Results = new List<TViewModel>()
            };

            if (String.IsNullOrWhiteSpace(request.Search.Value))
            {
                var query = DbContext.Set<TEntity>().GenericSort(request);

                query = await Where(query, request);
                result.TotalEntries = await query.CountAsync();

                var results = await query
                    .Skip(request.Start)
                    .Take(request.Length)
                    .ToListAsync();

                result.Results = results.Select(Mapper.Map<TViewModel>).ToList();
                result.TotalResults = result.TotalEntries;
            }
            else
            {
                var query = Search(DbContext.Set<TEntity>(), request.Search.Value).GenericSort(request);
                result.TotalResults = await query.CountAsync();

                query = await Where(query, request);
                result.TotalEntries = await query.CountAsync();

                var results = await query
                    .Skip(request.Start)
                    .Take(request.Length)
                    .ToListAsync();

                result.Results = results.Select(Mapper.Map<TViewModel>).ToList();
            }

            return DataTablesResponse.Create(request, result.TotalEntries, result.TotalResults, result.Results);
        }

        /// <summary>
        ///     Abstract method where the search logic specific for <see cref="TEntity"/> should be specified.
        /// </summary>
        /// <typeparam name="T"><see cref="TEntity"/></typeparam>
        /// <param name="query"><see cref="IQueryable"/> query</param>
        /// <param name="searchValue"><see cref="string"/> searchValue to search the database for</param>
        /// <returns></returns>
        protected abstract IQueryable<T> Search<T>(IQueryable<T> query, string searchValue) where T : TEntity;

        /// <summary>
        ///     Method that can be overridden when additional where should be used.
        /// </summary>
        /// <typeparam name="T"><see cref="TEntity"/></typeparam>
        /// <param name="query"><see cref="IQueryable{TEntity}"/> query</param>
        /// <param name="request"><see cref="IDataTablesRequest"/> </param>
        /// <returns></returns>
        protected virtual Task<IQueryable<T>> Where<T>(IQueryable<T> query, IDataTablesRequest request) where T : TEntity
        {
            return Task.FromResult(query);
        }
    }
}