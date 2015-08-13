using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversity.ViewModels;
using ContosoUniversity.Misc;

namespace ContosoUniversity.ApiControllers
{
    public class CoursesController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/Courses
        public IEnumerable<CoursesData> GetCourses()
        {
            // http://bengtbe.com/blog/2009/04/14/using-automapper-to-map-view-models-in-asp-net-mvc/
            Mapper.CreateMap<Course, CoursesData>();

            // https://lostechies.com/jimmybogard/2014/05/07/projecting-computed-properties-with-linq-and-automapper/
            IEnumerable<CoursesData> coursesList = db.Courses.Project().ToList<CoursesData>();

            //IEnumerable<Course> courses = db.Courses;
            //IEnumerable<CoursesData> coursesList = Mapper.Map<IEnumerable<Course>, IEnumerable<CoursesData>>(courses);
            return coursesList;            
        }

        // GET: api/Courses/5
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> GetCourse(int id)
        {
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCourse(int id, Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.CourseID)
            {
                return BadRequest();
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> PostCourse(Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Courses.Add(course);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseExists(course.CourseID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("ContosoUniversityApi", new { id = course.CourseID }, course);
        }

        // DELETE: api/Courses/5
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> DeleteCourse(int id)
        {
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            db.Courses.Remove(course);
            await db.SaveChangesAsync();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourseExists(int id)
        {
            return db.Courses.Count(e => e.CourseID == id) > 0;
        }
    }
}