﻿namespace ERP_System_Api.Helpers
{
    public class AuthResult
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
        //public IEnumerable<string> Errors { get; set; }
    }
}