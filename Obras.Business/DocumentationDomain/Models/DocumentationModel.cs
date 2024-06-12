﻿namespace Obras.Business.DocumentationDomain.Models
{
    public class DocumentationModel
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int? CompanyId { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
    }
}
