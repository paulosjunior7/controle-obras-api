﻿namespace Obras.Business.SharedDomain.Helpers
{

    public static class Constants
    {
        public static class Roles
        {
            public static string Customer = "Customer";
            public static string Engineer = "Engineer";
            public static string Admin = "Admin";
        }

        public static class AuthPolicy
        {
            public static string CustomerPolicy = "CustomerPolicy";
            public static string EngineerPolicy = "EngineerPolicy";
            public static string AdminPolicy = "AdminPolicy";
        }
    }
}
