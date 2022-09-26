using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();

        //--------------------------|Read|---------------------------------------------------------

        public ActionResult Index() 
        {
            List<student> list = new List<student>();

            client.BaseAddress = new Uri("https://localhost:44357/api/CRUD");


            var response = client.GetAsync("CRUD");

            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<student>>(); //reading as list of student objects

                display.Wait();

                list = display.Result;

            }

            return View(list);
        }
        //----------------------------------------------------------------------------------------------------------------------
        //--------------------------|Insert/Create|---------------------------------------------------------
        // url :- https://localhost:44357/Home/Create
        // while creating View select Create as template

        public ActionResult Create() //name must be same as this will be called when create button is clicked  
        {  
            return View();
        }

        [HttpPost]
        public ActionResult Create(student obj) 
        {

            client.BaseAddress = new Uri("https://localhost:44357/api/CRUD");


            var response = client.PostAsJsonAsync<student>("CRUD", obj); // it calls InsertStudent() which is marked as Post method in CRUDController

            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); //after insertnig user will be redirected to Index
            }

            return View("Create"); //if data insertion failed this View will be opened again
        }
        //----------------------------------------------------------------------------------------------------------------------

        //--------------------------|Details|---------------------------------------------------------
        // url :- https://localhost:44357/Home/Details/2
        // while creating View select Details as template
        public ActionResult Details(int id) //name must be same as this will be called when Details button is clicked , id is passed in URL
        {
            client.BaseAddress = new Uri("https://localhost:44357/api/CRUD");

            student student_obj = null;

            var response = client.GetAsync("CRUD?id=" + id.ToString()); // it calls GETStudentsById(),and passes id which is marked as Get method in CRUDController

            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<student>(); //reading as student object

                display.Wait();

                student_obj = display.Result;
            }

            return View(student_obj);
        }


        //----------------------------------------------------------------------------------------------------------------------

        //--------------------------|Edit/Update|---------------------------------------------------------
        // url:- https://localhost:44357/Home/Edit/1
        // while creating View select Edit as template

        //when edit button is clicked it searched id and gets student object and passes it to edit(student ) actiojn method
        public ActionResult Edit(int id) //edit accepts id as a paramenter in URL
        {

            client.BaseAddress = new Uri("https://localhost:44357/api/CRUD");

            student student_obj = null;

            var response = client.GetAsync("CRUD?id=" + id.ToString()); // it calls GETStudentsById(),and passes id which is marked as Get method in CRUDController

            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                //------------------|edited here|------------------
                var display = test.Content.ReadAsAsync<student>(); //reading as student object

                display.Wait();

                student_obj = display.Result; //calls UpdateStudentsById and gets student object
            }

            return View(student_obj); //it calls Edit(student student_object)
        }

        [HttpPost]
        public ActionResult Edit(student student_object)
        {
            client.BaseAddress = new Uri("https://localhost:44357/api/CRUD");


            var response = client.PutAsJsonAsync<student>("CRUD", student_object); // it calls UpdateStudent() which is marked as Put method in CRUDController

            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); //after Updating user will be redirected to Index
            }

            return View("Edit"); //if data insertion failed this View will be opened again
        }

        //----------------------------------------------------------------------------------------------------------------------

        //--------------------------|Delete|---------------------------------------------------------
        //url :- https://localhost:44357/Home/Delete/1
        // while creating View select Delete as template


        // when delete button is clicked , Delete(int id) gets student object from id and  controller goes top another action method( DeleteConfirmed )  which deletes that record
        public ActionResult Delete(int id) //Delete accepts id as a paramenter in URL
        {
            client.BaseAddress = new Uri("https://localhost:44357/api/CRUD");

            student student_obj = null;

            var response = client.GetAsync("CRUD?id=" + id.ToString());
            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<student>(); //reading as student object

                display.Wait();

                student_obj = display.Result; //calls UpdateStudentsById and gets student object
            }

            return View(student_obj); // calls DeleteConfirmed which is marked as Delete only 
        }

        [HttpPost, ActionName("Delete")] //when delete is clicked this method must be processed
        //   ActionName("Delete") aliases Delete to DeleteConfirmed
        public ActionResult DeleteConfirmed(int id)
        {
            client.BaseAddress = new Uri("https://localhost:44357/api/CRUD");


            var response = client.DeleteAsync("CRUD?id=" + id.ToString()); /// it calls DeleteStudent(),and passes id which is marked as Delete method in CRUDController
                                            // pass id through url , DeleteStudent takes id as parameter
            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); //after Updating user will be redirected to Index
            }

            return View("Delete"); //if data insertion failed this View will be opened again        }

            //----------------------------------------------------------------------------------------------------------------------

        }
    }
}
