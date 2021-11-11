using MVCList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCList.Controllers
{
    public class ListController : Controller
    {
        // GET: List
        public ActionResult Index()
        {
            IEnumerable<MVCListModel> list;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("lists").Result;
            list = response.Content.ReadAsAsync<IEnumerable<MVCListModel>>().Result;
            return View(list);
        }

        [ValidateInput(false)]
        public JsonResult AddNew(MVCListModel model)
        {

            
           HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("lists", model).Result;
            return Json("Index");
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new MVCListModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("lists/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCListModel>().Result);
            }
            
        }
        [HttpPost]
        public ActionResult AddOrEdit(MVCListModel model)
        {
            if (model.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("lists", model).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("lists/" + model.Id, model).Result;
            }

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("lists/" + Id.ToString()).Result;
            return RedirectToAction("Index");

        }
    }
}
