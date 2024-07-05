﻿using AutoMapper;
using Obras.Business.BrandDomain.Models;
using Obras.Business.BrandDomain.Request;
using Obras.Business.BrandDomain.Response;
using Obras.Business.CompanyDomain.Models;
using Obras.Business.ConstructionAdvanceMoneyDomain.Models;
using Obras.Business.ConstructionAdvanceMoneyDomain.Request;
using Obras.Business.ConstructionBatchDomain.Models;
using Obras.Business.ConstructionBatchDomain.Request;
using Obras.Business.ConstructionDocumentationDomain.Models;
using Obras.Business.ConstructionDocumentationDomain.Request;
using Obras.Business.ConstructionDocumentationDomain.Response;
using Obras.Business.ConstructionDomain.Models;
using Obras.Business.ConstructionDomain.Request;
using Obras.Business.ConstructionExpenseDomain.Models;
using Obras.Business.ConstructionExpenseDomain.Request;
using Obras.Business.ConstructionHouseDomain.Models;
using Obras.Business.ConstructionHouseDomain.Request;
using Obras.Business.ConstructionHouseDomain.Response;
using Obras.Business.ConstructionInvestorDomain.Models;
using Obras.Business.ConstructionInvestorDomain.Request;
using Obras.Business.ConstructionInvestorDomain.Response;
using Obras.Business.ConstructionManpowerDomain.Models;
using Obras.Business.ConstructionManpowerDomain.Request;
using Obras.Business.ConstructionManpowerDomain.Response;
using Obras.Business.ConstructionMaterialDomain.Models;
using Obras.Business.ConstructionMaterialDomain.Request;
using Obras.Business.ConstructionMaterialDomain.Response;
using Obras.Business.DashboardDomain.Response;
using Obras.Business.DocumentationDomain.Models;
using Obras.Business.DocumentationDomain.Request;
using Obras.Business.EmployeeDomain.Models;
using Obras.Business.EmployeeDomain.Request;
using Obras.Business.ExpenseDomain.Models;
using Obras.Business.ExpenseDomain.Request;
using Obras.Business.GroupDomain.Models;
using Obras.Business.GroupDomain.Request;
using Obras.Business.OutsourcedDomain.Models;
using Obras.Business.OutsourcedDomain.Request;
using Obras.Business.PeopleDomain.Models;
using Obras.Business.PeopleDomain.Request;
using Obras.Business.PeopleDomain.Response;
using Obras.Business.ProductDomain.Models;
using Obras.Business.ProductDomain.Request;
using Obras.Business.ProductProviderDomain.Models;
using Obras.Business.ProviderDomain.Models;
using Obras.Business.ProviderDomain.Request;
using Obras.Business.ProviderDomain.Response;
using Obras.Business.ResponsibilityDomain.Models;
using Obras.Business.ResponsibilityDomain.Request;
using Obras.Business.UnitDomain.Models;
using Obras.Business.UnitDomain.Request;
using Obras.Data.Entities;

namespace Obras.Business.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<BrandModel, Brand>().ReverseMap();
            CreateMap<BrandInput, BrandModel>().ReverseMap();
            CreateMap<BrandResponse, Brand>().ReverseMap();
            CreateMap<CompanyModel, Company>().ReverseMap();
            CreateMap<DocumentationModel, Documentation>().ReverseMap();
            CreateMap<DocumentationInput, DocumentationModel>().ReverseMap();
            CreateMap<EmployeeModel, Employee>().ReverseMap();
            CreateMap<EmployeeInput, EmployeeModel>().ReverseMap();
            CreateMap<ProviderModel, Provider>().ReverseMap();
            CreateMap<ProviderResponse, Provider>().ReverseMap();
            CreateMap<ProviderInput, ProviderModel>().ReverseMap();
            CreateMap<ExpenseModel, Expense>().ReverseMap();
            CreateMap<ExpenseModel, ExpenseInput>().ReverseMap();
            CreateMap<OutsourcedModel, Outsourced>().ReverseMap();
            CreateMap<OutsourcedInput, OutsourcedModel>().ReverseMap();
            CreateMap<PeopleModel, People>().ReverseMap();
            CreateMap<PeopleResponse, People>().ReverseMap();
            CreateMap<PeopleInput, PeopleModel>().ReverseMap();
            CreateMap<ProductModel, Product>().ReverseMap();
            CreateMap<ProductInput, ProductModel>().ReverseMap();
            CreateMap<ProductProviderModel, ProductProvider>().ReverseMap();
            CreateMap<ResponsibilityModel, Responsibility>().ReverseMap();
            CreateMap<ResponsibilityInput, ResponsibilityModel>().ReverseMap();
            CreateMap<TotalExpense, TotalExpenseResponse>().ReverseMap();
            CreateMap<ConstructionModel, Construction>().ReverseMap();
            CreateMap<ConstructionInput, ConstructionModel>().ReverseMap();
            CreateMap<ConstructionInvestorModel, ConstructionInvestor>().ReverseMap();
            CreateMap<ConstructionInvestorModel, ConstructionInvestorInput>().ReverseMap();
            CreateMap<ConstructionInvestorResponse, ConstructionInvestor>().ReverseMap();
            CreateMap<ConstructionBatchModel, ConstructionBatch>().ReverseMap();
            CreateMap<ConstructionBatchInput, ConstructionBatchModel>().ReverseMap();
            CreateMap<ConstructionHouseModel, ConstructionHouse>().ReverseMap();
            CreateMap<ConstructionHouseModel, ConstructionHouseInput>().ReverseMap();
            CreateMap<ConstructionHouseResponse, ConstructionHouse>().ReverseMap();
            CreateMap<UnityModel, Unity>().ReverseMap();
            CreateMap<UnityInput, UnityModel>().ReverseMap();
            CreateMap<GroupModel, Group>().ReverseMap();
            CreateMap<GroupInput, GroupModel>().ReverseMap();
            CreateMap<ConstructionMaterialModel, ConstructionMaterial>().ReverseMap();
            CreateMap<ConstructionMaterialModel, ConstructionMaterialInput>().ReverseMap();
            CreateMap<ConstructionMaterialResponse, ConstructionMaterial>().ReverseMap();
            CreateMap<ConstructionManpowerModel, ConstructionManpower>().ReverseMap();
            CreateMap<ConstructionManpowerModel, ConstructionManpowerInput>().ReverseMap();
            CreateMap<ConstructionManpowerResponse, ConstructionManpower>().ReverseMap();
            CreateMap<ConstructionDocumentationModel, ConstructionDocumentation>().ReverseMap();
            CreateMap<ConstructionDocumentationResponse, ConstructionDocumentation>().ReverseMap();
            CreateMap<ConstructionDocumentationResponse, ConstructionDocumentationModel>().ReverseMap();
            CreateMap<ConstructionDocumentationInput, ConstructionDocumentationModel>().ReverseMap();
            CreateMap<ConstructionExpenseModel, ConstructionExpense>().ReverseMap();
            CreateMap<ConstructionExpenseModel, ConstructionExpenseInput>().ReverseMap();
            CreateMap<ConstructionExpense, ConstructionExpenseResponse>().ReverseMap();
            CreateMap<ConstructionAdvanceMoneyModel, ConstructionAdvanceMoney>().ReverseMap();
            CreateMap<ConstructionAdvanceMoneyInput, ConstructionAdvanceMoneyModel>().ReverseMap();
        }
    }
}
