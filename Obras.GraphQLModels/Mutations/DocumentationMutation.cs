﻿using GraphQL;
using GraphQL.Types;
using Obras.Business.Models;
using Obras.Business.Services;
using Obras.Data;
using Obras.GraphQLModels.InputTypes;
using Obras.GraphQLModels.Types;

namespace Obras.GraphQLModels.Mutations
{
    public class DocumentationMutation : ObjectGraphType
    {
        public DocumentationMutation(IDocumentationService documentationService, ObrasDBContext dBContext)
        {
            Name = nameof(DocumentationMutation);

            FieldAsync<DocumentationType>(
                name: "createDocumentation",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<DocumentationInputType>> { Name = "documentation" }),
                resolve: async context =>
                {
                    var brandModel = context.GetArgument<DocumentationModel>("documentation");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    brandModel.CompanyId = (int)(brandModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : brandModel.CompanyId);
                    brandModel.ChangeUserId = userId;
                    brandModel.RegistrationUserId = userId;

                    var brand = await documentationService.CreateAsync(brandModel);
                    return brand;
                });

            FieldAsync<DocumentationType>(
                name: "updateDocumentation",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<DocumentationInputType>> { Name = "documentation" }),
                resolve: async context =>
                {
                    int documentationId = context.GetArgument<int>("id");
                    var documentationModel = context.GetArgument<DocumentationModel>("documentation");
                    var userId = (context.UserContext as GraphQLUserContext).User.GetUserId();

                    var user = await dBContext.User.FindAsync(userId);

                    documentationModel.CompanyId = (int)(documentationModel.CompanyId == null ? user.CompanyId != null ? user.CompanyId : 0 : documentationModel.CompanyId);
                    documentationModel.ChangeUserId = userId;

                    return await documentationService.UpdateDocumentationAsync(documentationId, documentationModel);
                });
        }
    }
}