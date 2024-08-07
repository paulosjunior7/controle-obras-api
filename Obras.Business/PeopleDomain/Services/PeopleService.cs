﻿using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using Obras.Business.ConstructionMaterialDomain.Response;
using Obras.Business.PeopleDomain.Enums;
using Obras.Business.PeopleDomain.Models;
using Obras.Business.PeopleDomain.Response;
using Obras.Business.SharedDomain.Enums;
using Obras.Business.SharedDomain.Models;
using Obras.Data;
using Obras.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obras.Business.PeopleDomain.Services
{
    public interface IPeopleService
    {
        Task<People> CreateAsync(PeopleModel model);
        Task<People> UpdatePeopleAsync(int id, PeopleModel model);
        Task<PageResponse<PeopleResponse>> GetPeoplesAsync(PageRequest<PeopleFilter, PeopleSortingFields> pageRequest);
        Task<PeopleResponse> GetPeopleId(int id);
    }

    public class PeopleService : IPeopleService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public PeopleService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<People> CreateAsync(PeopleModel model)
        {
            var peo = _mapper.Map<People>(model);
            peo.CreationDate = DateTime.Now;
            peo.ChangeDate = DateTime.Now;
            peo.CompanyId = (int)(model.CompanyId == null ? 0 : model.CompanyId);

            _dbContext.Peoples.Add(peo);
            try
            {
                var retorno = await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return peo;
        }

        public async Task<People> UpdatePeopleAsync(int id, PeopleModel model)
        {
            var peo = await _dbContext.Peoples.FindAsync(id);

            if (peo != null)
            {
                peo.TypePeople = model.TypePeople;
                peo.Active = model.Active;
                peo.Address = model.Address;
                peo.CellPhone = model.CellPhone;
                peo.EMail = model.EMail;
                peo.Cpf = model.Cpf;
                peo.Cnpj = model.Cnpj;
                peo.Constructor = model.Constructor;
                peo.Client = model.Client;
                peo.Investor = model.Investor;
                peo.FantasyName = model.FantasyName;
                peo.CorporateName = model.CorporateName;
                peo.Neighbourhood = model.Neighbourhood;
                peo.Number = model.Number;
                peo.Complement = model.Complement;
                peo.City = model.City;
                peo.State = model.State;
                peo.Telephone = model.Telephone;
                peo.ZipCode = model.ZipCode;
                peo.ChangeDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

            return peo;
        }

        public async Task<PeopleResponse> GetPeopleId(int id)
        {
            var response = await _dbContext.Peoples.FindAsync(id);

            return _mapper.Map<PeopleResponse>(response);
        }

        public async Task<PageResponse<PeopleResponse>> GetPeoplesAsync(PageRequest<PeopleFilter, PeopleSortingFields> pageRequest)
        {
            var filterQuery = _dbContext.Peoples.Where(x => x.CompanyId == pageRequest.Filter.CompanyId);
            filterQuery = LoadFilterQuery(pageRequest.Filter, filterQuery);
            #region Obtain Nodes

            var dataQuery = filterQuery;
            dataQuery = LoadOrder(pageRequest, dataQuery);

            int totalCount = await dataQuery.CountAsync();

            List<People> nodes = await dataQuery.Skip((pageRequest.Pagination.PageNumber - 1) * pageRequest.Pagination.PageSize)
                   .Take(pageRequest.Pagination.PageSize).AsNoTracking().ToListAsync();

            #endregion

            #region Obtain Flags

            int maxId = nodes.Count > 0 ? nodes.Max(x => x.Id) : 0;
            int minId = nodes.Count > 0 ? nodes.Min(x => x.Id) : 0;
            bool hasNextPage = (totalCount - 1) >= ((pageRequest.Pagination.PageNumber) * pageRequest.Pagination.PageSize);
            bool hasPrevPage = pageRequest.Pagination.PageNumber > 1;

            #endregion

            var itens = _mapper.Map<List<PeopleResponse>>(nodes);

            return new PageResponse<PeopleResponse>
            {
                Nodes = itens,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPrevPage,
                TotalCount = totalCount
            };
        }

        private static IQueryable<People> LoadOrder(PageRequest<PeopleFilter, PeopleSortingFields> pageRequest, IQueryable<People> dataQuery)
        {
            if (pageRequest.OrderBy?.Field == Enums.PeopleSortingFields.Cnpj)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Cnpj)
                    : dataQuery.OrderBy(x => x.Cnpj);
            }
            else if (pageRequest.OrderBy?.Field == Enums.PeopleSortingFields.CorporateName)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.CorporateName)
                    : dataQuery.OrderBy(x => x.CorporateName);
            }
            else if (pageRequest.OrderBy?.Field == Enums.PeopleSortingFields.FantasyName)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.FantasyName)
                    : dataQuery.OrderBy(x => x.FantasyName);
            }
            else if (pageRequest.OrderBy?.Field == Enums.PeopleSortingFields.Neighbourhood)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Neighbourhood)
                    : dataQuery.OrderBy(x => x.Neighbourhood);
            }
            else if (pageRequest.OrderBy?.Field == Enums.PeopleSortingFields.State)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.State)
                    : dataQuery.OrderBy(x => x.State);
            }
            else if (pageRequest.OrderBy?.Field == Enums.PeopleSortingFields.City)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.City)
                    : dataQuery.OrderBy(x => x.City);
            }
            else if (pageRequest.OrderBy?.Field == Enums.PeopleSortingFields.Active)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Active)
                    : dataQuery.OrderBy(x => x.Active);
            }
            else if (pageRequest.OrderBy?.Field == Enums.PeopleSortingFields.Constructor)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Constructor)
                    : dataQuery.OrderBy(x => x.Constructor);
            }
            else if (pageRequest.OrderBy?.Field == Enums.PeopleSortingFields.Investor)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Investor)
                    : dataQuery.OrderBy(x => x.Investor);
            }
            else if (pageRequest.OrderBy?.Field == Enums.PeopleSortingFields.Client)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Client)
                    : dataQuery.OrderBy(x => x.Client);
            }
            else if (pageRequest.OrderBy?.Field == Enums.PeopleSortingFields.Cpf)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.Cpf)
                    : dataQuery.OrderBy(x => x.Cpf);
            }
            else if (pageRequest.OrderBy?.Field == Enums.PeopleSortingFields.TypePeople)
            {
                dataQuery = (pageRequest.OrderBy.Direction == SortingDirection.DESC)
                    ? dataQuery.OrderByDescending(x => x.TypePeople)
                    : dataQuery.OrderBy(x => x.TypePeople);
            }

            return dataQuery;
        }

        private static IQueryable<People> LoadFilterQuery(PeopleFilter filter, IQueryable<People> filterQuery)
        {
            if (filter == null)
            {
                return filterQuery;
            }

            if (filter.Id != null)
            {
                filterQuery = filterQuery.Where(x => x.Id == filter.Id);
            }
            if (!string.IsNullOrEmpty(filter.CorporateName))
            {
                filterQuery = filterQuery.Where(x => x.CorporateName.ToLower().Contains(filter.CorporateName.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.FantasyName))
            {
                filterQuery = filterQuery.Where(x => x.FantasyName.ToLower().Contains(filter.FantasyName.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.Cnpj))
            {
                filterQuery = filterQuery.Where(x => x.Cnpj.Contains(filter.Cnpj));
            }
            if (!string.IsNullOrEmpty(filter.City))
            {
                filterQuery = filterQuery.Where(x => x.City.ToLower().Contains(filter.City.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.Neighbourhood))
            {
                filterQuery = filterQuery.Where(x => x.Neighbourhood.ToLower().Contains(filter.Neighbourhood.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter.State))
            {
                filterQuery = filterQuery.Where(x => x.State.ToLower().Contains(filter.State.ToLower()));
            }
            if (filter.Active != null)
            {
                filterQuery = filterQuery.Where(x => x.Active == filter.Active);
            }
            if (filter.isClient != null)
            {
                filterQuery = filterQuery.Where(x => x.Client == filter.isClient);
            }
            if (filter.isInvestor != null)
            {
                filterQuery = filterQuery.Where(x => x.Investor == filter.isInvestor);
            }
            if (filter.isConstructor != null)
            {
                filterQuery = filterQuery.Where(x => x.Constructor == filter.isConstructor);
            }
            if (!string.IsNullOrEmpty(filter.Cpf))
            {
                filterQuery = filterQuery.Where(x => x.Cpf.ToLower().Contains(filter.Cpf.ToLower()));
            }
            if (filter.TypePeople != null)
            {
                filterQuery = filterQuery.Where(x => x.TypePeople == filter.TypePeople);
            }

            return filterQuery;
        }
    }
}
