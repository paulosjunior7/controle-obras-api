﻿using GraphQL.Types;
using Obras.Data;
using Obras.Data.Entities;
using Obras.GraphQLModels.ResponsibilityDomain.Types;
using Obras.GraphQLModels.SharedDomain.Types;

namespace Obras.GraphQLModels.EmployeeDomain.Types
{
    public class EmployeeType : ObjectGraphType<Employee>
    {
        public EmployeeType(ObrasDBContext dbContext)
        {
            Name = nameof(EmployeeType);

            Field(x => x.Id);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.Number, nullable: true);
            Field(x => x.State, nullable: true);
            Field(x => x.Telephone, nullable: true);
            Field(x => x.ZipCode, nullable: true);
            Field(x => x.Active);
            Field(x => x.Address, nullable: true);
            Field(x => x.CellPhone, nullable: true);
            Field(x => x.ChangeDate, nullable: true);
            Field(x => x.City, nullable: true);
            Field(x => x.Cpf, nullable: true);
            Field(x => x.Cnpj, nullable: true);
            Field(x => x.Employed, nullable: true);
            Field(x => x.Outsourced, nullable: true);
            Field(x => x.Complement, nullable: true);
            Field(x => x.Name, nullable: true);
            Field(x => x.CreationDate, nullable: true);
            Field(x => x.EMail, nullable: true);

            Field<StringGraphType>(
                name: "typePeople",
                resolve: context => context.Source.TypePeople.ToString());

            FieldAsync<ResponsibilityType>(
                name: "responsibility",
                resolve: async context => await dbContext.Responsibilities.FindAsync(context.Source.ResponsibilityId));

            FieldAsync<UserType>(
                name: "changeUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.ChangeUserId));

            FieldAsync<UserType>(
                name: "registrationUser",
                resolve: async context => await dbContext.User.FindAsync(context.Source.RegistrationUserId));

        }
    }
}
