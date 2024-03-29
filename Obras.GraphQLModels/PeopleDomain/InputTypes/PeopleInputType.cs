﻿using GraphQL.Types;
using Obras.Business.PeopleDomain.Models;
using Obras.GraphQLModels.PeopleDomain.Enums;

namespace Obras.GraphQLModels.PeopleDomain.InputTypes
{
    public class PeopleInputType : InputObjectGraphType<PeopleModel>
    {
        public PeopleInputType()
        {
            Name = nameof(PeopleInputType);

            Field(x => x.Cnpj, nullable: true);
            Field(x => x.CorporateName);
            Field(x => x.FantasyName, nullable: true);
            Field(x => x.Active);
            Field(x => x.Address, nullable: true);
            Field(x => x.CellPhone, nullable: true);
            Field(x => x.City, nullable: true);
            Field(x => x.Complement, nullable: true);
            Field(x => x.Constructor, nullable: true);
            Field(x => x.Client, nullable: true);
            Field(x => x.Investor, nullable: true);
            Field(x => x.EMail, nullable: true);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.Number, nullable: true);
            Field(x => x.State, nullable: true);
            Field(x => x.Telephone, nullable: true);
            Field(x => x.ZipCode, nullable: true);
            Field(x => x.Cpf, nullable: true);
            Field<TypePeopleEnumType>("typePeople");
        }
    }
}
