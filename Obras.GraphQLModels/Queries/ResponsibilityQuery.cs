﻿using GraphQL;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
using Obras.Business.Enums;
using Obras.Business.Helpers;
using Obras.Business.Models;
using Obras.Business.Services;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.InputTypes;
using Obras.GraphQLModels.Types;
using System.Linq;

namespace Obras.GraphQLModels.Queries
{
    public class ResponsibilityQuery : ObjectGraphType
    {
        public ResponsibilityQuery(IResponsibilityService responsibilityService, ObrasDBContext dBContext)
        {
            Connection<ResponsibilityType>()
                .Name("findall")
                .Unidirectional()
                .AuthorizeWith("LoggedIn")
                .Argument<PaginationDetailsType>("pagination", "Paginarion")
                .Argument<ResponsibilityByInputType>("sort", "Pass field & direction on which you want to sort data")
                .Argument<ResponsibilityFilterByInputType>("filter", "filter on which you want to sort data")
                .ResolveAsync(async context =>
                {
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);
                    var pageRequest = new PageRequest<ResponsibilityFilter, ResponsibilitySortingFields>
                    {
                        Pagination = context.GetArgument<PaginationDetails>("pagination") ?? new PaginationDetails(),
                        Filter = context.GetArgument<ResponsibilityFilter>("filter") ?? new ResponsibilityFilter(),
                        OrderBy = context.GetArgument<SortingDetails<ResponsibilitySortingFields>>("sort")
                    };

                    pageRequest.Filter.CompanyId = (int)(pageRequest.Filter.CompanyId == null ? user.CompanyId : pageRequest.Filter.CompanyId);

                    var pageResponse = await responsibilityService.GetResponsibilitiesAsync(pageRequest);

                    (string startCursor, string endCursor) = CursorHelper.GetFirstAndLastCursor(pageResponse.Nodes.Select(x => x.Id));

                    var edge = pageResponse.Nodes.Select(x => new Edge<Responsibility>
                    {
                        Cursor = CursorHelper.ToCursor(x.Id),
                        Node = x
                    }).ToList();

                    var connection = new Connection<Responsibility>()
                    {
                        Edges = edge,
                        TotalCount = pageResponse.TotalCount,
                        PageInfo = new PageInfo
                        {
                            HasNextPage = pageResponse.HasNextPage,
                            HasPreviousPage = pageResponse.HasPreviousPage,
                            StartCursor = startCursor,
                            EndCursor = endCursor
                        }
                    };

                    return connection;
                });
        }
    }
}
