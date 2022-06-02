﻿using Bookstore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Interface
{
    public interface IProfile
    {
        Task<int> AddUserProfile(UserProfile profile);
        Task<int> UpdateUserProfile(UserProfile profile);
        Task<int> DeleteUserProfile(Guid profileId);
    }
}