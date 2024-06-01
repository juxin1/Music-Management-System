using one.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace one.Controllers
{
    public class AccountController : Controller
    {
        oneEntities2 db = new oneEntities2();
        string User = null;
        // GET: Account
        [HttpGet]
        public ActionResult Login()     //展示登陆
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(string user, string pasword)     //提交登陆
        {
            Admin admin = new Admin();
            Admin admin2 = new Admin();
            if(user == ""||pasword=="")
            {
                return Content("<script>alert('请输入用户名或密码');window.location.href='Login'</script>");
            }
            
            /*if (admin == null || admin2 == null)
            {
                return Content("<script>alert('用户名或密码输入错误');window.location.href='Login'</script>");
            }*/
            else 
            {
                admin = db.Admin.SingleOrDefault(m => m.User == user);
                admin2 = db.Admin.SingleOrDefault(m => m.Pasword == pasword);
                if(admin ==null || admin2==null)
                {
                    return Content("<script>alert('用户名或密码错误！');window.location.href='Login'</script>");
                }
                string user1 = admin.User;
                string pasword1 = admin2.Pasword;
                int temp1 = user.CompareTo(user1);
                int temp2 = pasword.CompareTo(pasword1);
                ViewBag.test = pasword1;
                if (temp1 == temp2)
                {
                    /* return View("About");*/
                    TempData["user"] = user1;
                    /*return RedirectToAction("About");*/
                    return Content("<script>alert('登陆成功');window.location.href='About'</script>");
                }
                else
                {
                    return View();
                }
            }
        }
        public ActionResult About()    //展示音乐页
        {
            /*string user = TempData["user"].ToString();
            ViewBag.User = user;*/
            List<Music> list = db.Music.ToList();
            ViewBag.list = list;
            ViewBag.user = User;
            return View();
        }

        [HttpGet]
        public ActionResult Add()       //显示添加信息
        {
            ViewBag.user = User;
            return View();
        }

        [HttpPost]
        public ActionResult Add(string Name, string Image, string MusicUrl, string MusicID)       //提交信息
        {
            Music music = new Music();
            ViewBag.user = User;
            music.Name = Name;
            music.Image = Image;
            music.Music1 = MusicUrl;
            music.MusicID = MusicID;
            /*db.Entry(music).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();*/
            db.Music.Add(music);
            int count = db.SaveChanges();
            if (count > 0)
            {
                return Content("<script>alert('添加成功');window.location.href='Add'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');window.location.href='Add'</script>");
            }



            /*return RedirectToAction("About");*/

        }

        public ActionResult Admin()      //个人主页管理
        {
            ViewBag.user = User;
            List<Admin> list = db.Admin.ToList();
            ViewBag.list = list;
            return View();
        }                  

        public ActionResult Updata()        //显示修改信息
        {
            TempData["id"] = Convert.ToInt32(Request["Uid"]);
            Music music = db.Music.Find(TempData["id"]);
            ViewBag.user = User;
            /*   Music music = db.Music.Find(ID);
               ViewBag.name = music.Name;
               ViewBag.image = music.Image;
               ViewBag.musicurl = music.Music1;
               ViewBag.musicid = music.MusicID;*/
            /* ViewBag.music = music;*/
            ViewData.Model = music;
            return View();
        }                                           

        [HttpPost]
        public ActionResult Updata(string id, string Name, string Image, string MusicUrl, string MusicID)       //提交修改信息
        {
            int ID = Convert.ToInt32(id);
            int test = Convert.ToInt32(Request.Form["id"]);
            ViewBag.user = User;
            /*return Content(Name);*/
            Music music = db.Music.Find(test);
            music.Name = Name;
            music.Image = Image;
            music.Music1 = MusicUrl;
            music.MusicID = MusicID;
            db.Entry(music).State = System.Data.Entity.EntityState.Modified;
            int count = db.SaveChanges();
            if (count > 0)
            {
                return Content("<script>alert('修改成功');window.location.href='About'</script>");
            }
            else
            {
                return Content("<script>alert('修改失败');window.location.href='About'</script>");
            }

        }
        

        public ActionResult Delete()        //删除当前音乐
        {
            ViewBag.user = User;
            int ID = Convert.ToInt32(Request["Uid"]);
            Music music = db.Music.Find(ID);
            db.Music.Remove(music);
            int count = db.SaveChanges();
            //return View("About");
            if (count > 0)
            {
                return Content("<script>alert('删除成功');window.location.href='About'</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');window.location.href='About'</script>");
            }
        }
    }
    }


    
