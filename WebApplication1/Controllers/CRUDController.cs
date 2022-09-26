using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http; //need this
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CRUDController : ApiController
    {
        MydatabaseEntities db = new MydatabaseEntities(); //this will be present inside model->Model1.Context.tt-?Model1.Context.cs->inside partial class public MydatabaseEntities method
        //this is an direct database object 
        //------------------------------|Read|-------------------------------------------------------
        [HttpGet] //returns values from database
        public IHttpActionResult GETStudents()
        {
            List<student> list = db.students.ToList<student>();

            return Ok(list);
        }
        //---------------------------------------------------------------------------------------------

        //-----------------------|Insert|--------------------------------------------------------------
        [HttpPost]  //insert data to database
        public IHttpActionResult InsertStudent(student obj)
        {

            db.students.Add(obj);
            db.SaveChanges();
            return Ok();
        }
        //---------------------------------------------------------------------------------------------
        //-----------------------|Details|--------------------------------------------------------------

        [HttpGet] //returns values from database
        public IHttpActionResult GETStudentsById(int id)
        {
            var obj = db.students.Where( model => model.Id == id  ).FirstOrDefault(); //gets full row of that id

            return Ok(obj);
        }
        //---------------------------------------------------------------------------------------------
        //-----------------------|Update/Edit|--------------------------------------------------------------
        [HttpPut] //used in update in database
        public IHttpActionResult UpdateStudent(student obj)
        {
            var student_object = db.students.Where(model => model.Id == obj.Id).FirstOrDefault();  //searched obj id in database and select all rows and put in inside student_object

            if (student_object != null)
            {
                student_object.Id = obj.Id;
                student_object.name = obj.name;
                student_object.@class = obj.@class;

                db.SaveChanges(); //commits changes
            }
            else
            {
                return NotFound(); // returns notfound view
            }

            return Ok();
        }

        //---------------------------------------------------------------------------------------------

        //-----------------------|Delete|--------------------------------------------------------------

        [HttpDelete]  //Delete data to database
        public IHttpActionResult DeleteStudent(int id)
        {
            var obj = db.students.Where(model => model.Id == id).FirstOrDefault(); //gets full row of that id
            db.Entry(obj).State = System.Data.Entity.EntityState.Deleted; //deletes that id
            db.SaveChanges();
            return Ok();
        }

        //---------------------------------------------------------------------------------------------
    }
}
