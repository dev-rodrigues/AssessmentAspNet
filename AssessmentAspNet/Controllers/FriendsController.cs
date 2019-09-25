using AssessmentAspNet.Models;
using AssessmentAspNet.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessmentAspNet.Controllers {
    public class FriendsController : Controller {

        // ordenar lista
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
            var repo = new FriendsRepository();
            var fv = repo.GetFriendById(id);
            return View(fv);
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
            var repository = new FriendsRepository();
            try {
                FriendViewModel fvm = new FriendViewModel();

                fvm.Id = Int32.Parse(collection["Id"]);
                fvm.FristName = collection["FristName"];
                fvm.LastName = collection["LastName"];
                fvm.BirthDate = DateTime.Parse(collection["BirthDate"]);
                repository.UpdateFriend(fvm);
                return RedirectToAction("Index");

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return View();
            }
        }

        public ActionResult Delete(int id) {
            var repository = new FriendsRepository();
            return View(repository.GetFriendById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            try {
                var repository = new FriendsRepository();
                repository.DeleteFriend(id);
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        public ActionResult Buscar() {
            string pesquisar = "";
            var repo = new FriendsRepository();
            return View(repo.GetFriendByString(pesquisar));
        }

        [HttpPost]
        public ActionResult Buscar(string pesquisa) {
            try {
                var repo = new FriendsRepository();

                return View(repo.GetFriendByString(pesquisa));
            } catch {
                return View();
            }
        }
    }
}
