using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Models
{
    interface IUsers
    {
        bool Verify(string Email, string Password);

        bool Register(Users u);

        bool FindDuplicate(string Email);
    }
}
