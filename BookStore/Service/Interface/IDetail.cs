using System;
using System.Threading.Tasks;
using Bookstore.Model;

public interface IDetail
{
    Task<int> AddDetail(BookDetail detail);
    Task<int> UpdateDetail(BookDetail updatedDetail);
    Task<int> DeleteDetailById(Guid detailId);
}