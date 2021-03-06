﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TLC.Data;

namespace TLC.Controllers
{
    public class TeamMembersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/TeamMembers
        public IQueryable<Member> GetTeamMembers()
        {
            return db.TeamMembers;
        }

        // GET: api/TeamMembers/5
        [ResponseType(typeof(Member))]
        public IHttpActionResult GetTeamMember(int id)
        {
            Member teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return Ok(teamMember);
        }

        // PUT: api/TeamMembers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeamMember(int id, Member teamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teamMember.MemberId)
            {
                return BadRequest();
            }

            db.Entry(teamMember).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamMemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TeamMembers
        [ResponseType(typeof(Member))]
        public IHttpActionResult PostTeamMember(Member teamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeamMembers.Add(teamMember);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teamMember.MemberId }, teamMember);
        }

        // DELETE: api/TeamMembers/5
        [ResponseType(typeof(Member))]
        public IHttpActionResult DeleteTeamMember(int id)
        {
            Member teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            db.TeamMembers.Remove(teamMember);
            db.SaveChanges();

            return Ok(teamMember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamMemberExists(int id)
        {
            return db.TeamMembers.Count(e => e.MemberId == id) > 0;
        }
    }
}