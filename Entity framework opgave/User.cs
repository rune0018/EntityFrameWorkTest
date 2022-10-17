using System;
using System.Collections.Generic;

namespace Entity_framework_opgave
{
    public partial class User
    {
        public long Userid { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
