﻿using GraphQL.Types;
using Obras.Business.Models;
using Obras.GraphQLModels.Enums;

namespace Obras.GraphQLModels.InputTypes
{
    public class PeopleFilterByInputType : InputObjectGraphType<PeopleFilter>
    {
        public PeopleFilterByInputType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Cnpj, nullable: true);
            Field(x => x.CorporateName, nullable: true);
            Field(x => x.Active, nullable: true);
            Field(x => x.City, nullable: true);
            Field(x => x.FantasyName, nullable: true);
            Field(x => x.Neighbourhood, nullable: true);
            Field(x => x.State, nullable: true);
            Field(x => x.Cpf, nullable: true);
            Field(x => x.CompanyId, nullable: true);

            Field<TypePeopleEnumType>("typePeople");
        }
    }
}
