using Bookstore.Model;
using Bookstore.Service.Interface;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Repository
{
    public class ProfileRepository : IProfile
    {
        readonly DbService db;
        public ProfileRepository(DbService db)
        {
            this.db = db;
        }

        public async Task<UserProfile> GetProfile(string userId)
        {
            return await db.UserProfiles.Where(s => s.Id == userId).FirstOrDefaultAsync();
        }
        public async Task<int> AddUserProfile(UserProfile profile)
        {
            try
            {
                profile.ProfileId = Guid.NewGuid();
                await db.UserProfiles.AddAsync(profile);
                return await SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> UpdateUserProfile(UserProfile profile)
        {
            try
            {
                UserProfile currentUser = await GetProfileById(profile.Id);
                db.Entry(currentUser).CurrentValues.SetValues(profile);
                db.Entry(currentUser).State = EntityState.Modified;
                return await SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> DeleteUserProfile(string userId)
        {
            try
            {
                UserProfile currentProfile = await GetProfileById(userId);
                db.UserProfiles.Remove(currentProfile);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private async Task<UserProfile> GetProfileById(string userId)
        {
            return await db.UserProfiles.Where(s => s.Id == userId).FirstOrDefaultAsync();
        }

        
        private async Task<int> SaveChanges()
        {
            return await db.SaveChangesAsync();
        }
    }
}
