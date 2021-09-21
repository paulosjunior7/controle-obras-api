﻿using GraphQL;
using GraphQL.Types;
using Obras.Business.Models;
using Obras.Business.Services;
using Obras.Data;
using Obras.GraphQLModels.InputTypes;
using Obras.GraphQLModels.Types;

namespace Obras.GraphQLModels.Mutations
{
    public class PeopleMutation : ObjectGraphType
    {
        public PeopleMutation(IPeopleService peopleService, ObrasDBContext dBContext)
        {
            Name = nameof(PeopleMutation);

            FieldAsync<PeopleType>(
                name: "createPeople",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<PeopleInputType>> { Name = "people" }),
                resolve: async context =>
                {
                    var peopleModel = context.GetArgument<PeopleModel>("people");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    peopleModel.CompanyId = (int)(peopleModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : peopleModel.CompanyId);
                    peopleModel.ChangeUserId = userId;
                    peopleModel.RegistrationUserId = userId;

                    var expense = await peopleService.CreateAsync(peopleModel);
                    return expense;
                });

            FieldAsync<PeopleType>(
                name: "updatePeople",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<PeopleInputType>> { Name = "people" }),
                resolve: async context =>
                {
                    int peopleId = context.GetArgument<int>("id");
                    var peopleModel = context.GetArgument<PeopleModel>("people");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    peopleModel.CompanyId = (int)(peopleModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : peopleModel.CompanyId);
                    peopleModel.ChangeUserId = userId;

                    return await peopleService.UpdatePeopleAsync(peopleId, peopleModel);
                });
        }
    }
}
