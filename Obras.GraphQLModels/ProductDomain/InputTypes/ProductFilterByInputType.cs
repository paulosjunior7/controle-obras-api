﻿namespace Obras.GraphQLModels.ProductDomain.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.ProductDomain.Models;

    public class ProductFilterByInputType : InputObjectGraphType<ProductFilter>
    {
        public ProductFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Detail, nullable: true);
            Field(x => x.Description, nullable: true);
            Field(x => x.CompanyId, nullable: true);
            Field(x => x.Active, nullable: true);
        }
    }
}
