﻿namespace Obras.GraphQLModels.SharedDomain.Mutations
{
    using GraphQL.Types;
    using Obras.GraphQLModels.BrandDomain.Mutations;
    using Obras.GraphQLModels.CompanyDomain.Mutations;
    using Obras.GraphQLModels.DocumentationDomain.Mutations;
    using Obras.GraphQLModels.ExpenseDomain.Mutations;
    using Obras.GraphQLModels.PeopleDomain.Mutations;
    using Obras.GraphQLModels.ProductDomain.Mutations;
    using Obras.GraphQLModels.ProviderDomain.Mutations;
    using Obras.GraphQLModels.ResponsibilityDomain.Mutations;

    public class ObrasMutation : ObjectGraphType
    {
        public ObrasMutation()
        {
            Name = nameof(ObrasMutation);
            Field<CompanyMutation>("companies", resolve: context => new { });
            Field<ProductMutation>("products", resolve: context => new { });
            Field<ProviderMutation>("providers", resolve: context => new { });
            Field<BrandMutation>("brands", resolve: context => new { });
            Field<DocumentationMutation>("documentations", resolve: context => new { });
            Field<ResponsibilityMutation>("responsibilities", resolve: context => new { });
            Field<ExpenseMutation>("expenses", resolve: context => new { });
            Field<PeopleMutation>("peoples", resolve: context => new { });
        }
    }
}