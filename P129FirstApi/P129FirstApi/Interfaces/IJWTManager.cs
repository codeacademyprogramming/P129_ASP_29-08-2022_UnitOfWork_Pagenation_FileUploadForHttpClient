using Microsoft.Extensions.Configuration;
using P129FirstApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P129FirstApi.Interfaces
{
    public interface IJWTManager
    {
        string GenerateToken(AppUser appUser, IList<string> roles);

        string GetUserNameByToken(string token);
    }
}
