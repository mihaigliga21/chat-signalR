using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Helper
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Settings Settings { get; set; }
    }

    public class ConnectionStrings
    {
        public string ChatDatabase { get; set; }
    }

    public class Settings
    {
        public Jwt Jwt { get; set; }
    }

    public class Jwt
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
