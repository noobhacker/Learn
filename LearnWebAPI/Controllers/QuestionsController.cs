﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LearnWebAPI.Models;

namespace LearnWebAPI.Controllers
{
    public class QuestionsController : ApiController
    {
        private LearnWebAPIContext db = new LearnWebAPIContext();

        // GET: api/Questions
        [ResponseType(typeof(List<Question>))]
        public IHttpActionResult GetQuestions(int id)
        {
            return Ok(db.Questions.Where(x=>x.BookId == id).ToList());
        }

        //// GET: api/Questions/5
        //[ResponseType(typeof(Question))]
        //public async Task<IHttpActionResult> GetQuestion(int id)
        //{
        //    Question question = await db.Questions.FindAsync(id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(question);
        //}

        //// PUT: api/Questions/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutQuestion(int id, Question question)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != question.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(question).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!QuestionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Questions
        [ResponseType(typeof(Question))]
        public async Task<IHttpActionResult> PostQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Questions.Add(question);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = question.Id }, question);
        }

        //// DELETE: api/Questions/5
        //[ResponseType(typeof(Question))]
        //public async Task<IHttpActionResult> DeleteQuestion(int id)
        //{
        //    Question question = await db.Questions.FindAsync(id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Questions.Remove(question);
        //    await db.SaveChangesAsync();

        //    return Ok(question);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool QuestionExists(int id)
        //{
        //    return db.Questions.Count(e => e.Id == id) > 0;
        //}
    }
}