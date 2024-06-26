﻿using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.UnityDomain.Types
{
    public class UnityType : ObjectGraphType<Unity>
    {
        public UnityType(ObrasDBContext dbContext)
        {
            Name = nameof(UnityType);

            Field(x => x.Id);
            Field(x => x.Description, nullable: true);
            Field(x => x.Multiplier, nullable: true);
            Field(x => x.CreationDate, nullable: true);
            Field(x => x.ChangeDate, nullable: true);
            Field(x => x.Active);

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));
        }
    }
}
