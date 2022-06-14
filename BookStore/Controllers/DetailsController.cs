using Bookstore.Model;
using Bookstore.Model.DTO.Detail;
using Bookstore.Service.Interface;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        readonly IDetail _detailDb;
        public DetailsController(IDetail _detailDb)
        {
            this._detailDb = _detailDb;
        }

        [HttpPost]
        public async Task<ActionResult> AddBookDetail(DetailCreate newDetail)
        {
            BookDetail bookDetail = newDetail.Adapt<BookDetail>();
            await _detailDb.AddDetail(bookDetail);
            return Ok("Created");
        }

        [HttpPut("{detailId}")]
        public async Task<ActionResult> UpdateBookDetail(Guid detailId,DetailUpdate updatedDetail)
        {
            BookDetail bookDetail = updatedDetail.Adapt<BookDetail>();
            BookDetail currentBookDetail = await _detailDb.GetDetail(detailId);
            if(currentBookDetail == null)
            {
                return NotFound();
            }
            await _detailDb.UpdateDetail(bookDetail);
            return Ok("Updated successfully");
        }

        [HttpDelete("{detailId}")]
        public async Task<ActionResult> DeleteBookDetail(Guid detailId)
        {
            BookDetail currentBookDetail = await _detailDb.GetDetail(detailId);
            if(currentBookDetail == null)
            {
                return NotFound();
            }
            await _detailDb.DeleteDetailById(detailId);
            return Ok("Deleted successfully");
        }
    }
}
