﻿using Obras.Data.Entities;
using Obras.Data.Enums;
using System;
namespace Obras.Business.ProviderDomain.Response
{
    public class ProviderResponse
    {
        public int Id { get; set; }
        public TypePeople TypePeople { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Neighbourhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Complement { get; set; }
        public string Telephone { get; set; }
        public string CellPhone { get; set; }
        public string EMail { get; set; }
        public int CompanyId { get; set; }
        public bool Active { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}

