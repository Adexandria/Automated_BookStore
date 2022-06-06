using Bookstore.Model;
using Bookstore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Repository
{
    public class DetailRepository : IDetail
    {
        readonly DbService db;
        public DetailRepository(DbService db)
        {
            this.db = db;
        }
        public async Task<int> AddDetail(BookDetail detail)
        {
            try
            {
                detail.DetailId = Guid.NewGuid();
                await db.BookDetails.AddAsync(detail);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> DeleteDetailById(Guid detailId)
        {
            try
            {
                BookDetail currentDetail = await GetDetail(detailId);
                db.BookDetails.Remove(currentDetail);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> UpdateDetail(BookDetail updatedDetail)
        {
            try
            {
                BookDetail currentDetail = await GetDetail(updatedDetail.DetailId);
                db.Entry(currentDetail).CurrentValues.SetValues(updatedDetail);
                db.Entry(currentDetail).State = EntityState.Modified;
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private async Task<BookDetail> GetDetail(Guid detailId)
        {
            return await db.BookDetails.Where(s => s.DetailId == detailId).FirstOrDefaultAsync();
        }
        private async Task<int> SaveChanges()
        {
            return await db.SaveChangesAsync();
        }
    }
}
