﻿using Bookstore.Model;
using Bookstore.Model.DTO.Author;
using Bookstore.Service.Interface;
using Mapster;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class AuthorController : ControllerBase
    {
        readonly IAuthor _authorDb;
        public AuthorController(IAuthor _authorDb)
        {
            this._authorDb = _authorDb;
        }

        [HttpPost]
        public async Task<ActionResult> AddAuthor(AuthorCreate newAuthor)
        {
            Author author = newAuthor.Adapt<Author>();
            await _authorDb.AddAuthor(author);
            return Ok("Successful");
        }

        [HttpPut("{authorId}")]
        public async Task<ActionResult> UpdateAuthor(Guid authorId,AuthorUpdate updatedAuthor)
        {
            Author author = updatedAuthor.Adapt<Author>();
            Author currentAuthor = await _authorDb.GetAuthor(authorId);
            if(currentAuthor == null)
            {
                return NotFound();
            }
            await _authorDb.UpdateAuthor(author);
            return Ok("Successful");
        }

        [HttpDelete("{authorId}")]
        public async Task<ActionResult> DeleteAuthor(Guid authorId)
        {
            Author currentAuthor = await _authorDb.GetAuthor(authorId);
            if (currentAuthor == null)
            {
                return NotFound();
            }
            await _authorDb.DeleteAuthorById(authorId);
            return Ok("Successful");
        }
    }
}
