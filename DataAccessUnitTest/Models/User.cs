using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vic.Data;

namespace Expression2SqlTest
{
    [Table("userinfo1")]
    class UserInfo
    {
        [Identity]
        public int Id { get; set; }
        public int Sex { get; set; }

        [Primarykey]
        public string Name { get; set; }

        [Field("email1")]
        public string Email { get; set; }
    }
}
