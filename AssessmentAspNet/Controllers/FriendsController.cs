using AssessmentAspNet.Models;
using AssessmentAspNet.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssessmentAspNet.Models;

namespace AssessmentAspNet.Controllers {
    public class FriendsController : Controller {

        public ActionResult Index() {
            var repository = new FriendsRepository();
            var friends = repository.GetAllFriends();

            return View(
                friends.Select(a => new FriendViewModel {
                    Id = a.Id,
                    FristName = a.FristName,
                    LastName = a.LastName,
                    BirthDate = a.BirthDate
                }));
        }

        public ActionResult Details(int id) {
            return View();
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            var FristName = collection["FristName"];
            var LastName = collection["LastName"];
            var BirthDate = collection["BirthDate"];
            var date = DateTime.Parse(BirthDate);

            try {
                var repository = new FriendsRepository();
                repository.InsertFriend(FristName, LastName, date);
                return RedirectToAction("Index");
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return View();
            }
        }

        public ActionResult Edit(int id) {
            var repository = new FriendsRepository();
            FriendViewModel fv = repository.GetFriendById(id);
            return View(fv);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) {
            try {
                return RedirectToAction("Index");

            } catch {
                return View();
            }
        }

        public ActionResult Delete(int id) {
            return View();
        }

        // POST: Friends/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            try {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }
    }
}
