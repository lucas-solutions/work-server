using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    public class UserStore : EntityStore<User>, IUserStore
    {
    }

    public class UserStore<TUser> : EntityStore<TUser>, IUserStore
        where TUser : User
    {

    }
}
