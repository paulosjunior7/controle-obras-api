﻿namespace Obras.GraphQLModels.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.Enums;
    using Obras.Business.Models;
    using Obras.GraphQLModels.Enums;

    public class ProductByInputType : InputObjectGraphType<SortingDetails<ProductSortingFields>>
    {
        public ProductByInputType()
        {
            Field<ProductSortingFieldsEnumType>("field", resolve: context => context.Source.Field);
            Field<SortingDirectionEnumType>("direction", resolve: context => context.Source.Direction);
        }
    }
}
