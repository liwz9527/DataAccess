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

        [Field("xb")]
        public int Sex { get; set; }

        [Primarykey]
        [Field("xm")]
        public string Name { get; set; }

        [Field("yx")]
        public string Email { get; set; }
    }
}
