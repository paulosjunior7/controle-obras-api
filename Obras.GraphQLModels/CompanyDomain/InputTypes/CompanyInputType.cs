﻿namespace Obras.GraphQLModels.CompanyDomain.InputTypes
{
    using GraphQL.Types;
    using Obras.Business.CompanyDomain.Models;

    public class CompanyInputType : InputObjectGraphType<CompanyModel>
    {
        public CompanyInputType()
        {
            Name = nameof(CompanyInputType);

            Field(x => x.Cnpj);
            Field(x => x.CorporateName);
            Field(x => x.FantasyName, nullable: true);
            Field(x => x.Active);
            Field(x => x.Address, nullable: true);
            Field(x => x.CellPhone, nullable: true);
            Field(x => x.City, nullable: true);
            Field(x => x.Complement, nullable: true);
            Field(x => x.EMail, nullable: true);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.Number, nullable: true);
            Field(x => x.State, nullable: true);
            Field(x => x.Telephone, nullable: true);
            Field(x => x.ZipCode, nullable: true);
        }
    }
}
