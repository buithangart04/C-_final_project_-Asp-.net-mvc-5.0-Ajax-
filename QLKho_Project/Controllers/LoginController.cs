using Model.Dal;
using QLKho_Project.Common;
using QLKho_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using System.Configuration;

namespace QLKho_Project.Controllers
{
    public class LoginController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginFaceBook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FBAppId"],
                client_secret = ConfigurationManager.AppSettings["FBAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type="code",
                scope="email"
            }) ;
            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token",new
            {
                client_id = ConfigurationManager.AppSettings["FBAppId"],
                client_secret = ConfigurationManager.AppSettings["FBAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code=code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string username = me.id;
                string firstName = me.first_name;
                string middleName = me.middle_name;
                string lastName = me.last_name;
                var userSession = new UserLogin();
                userSession.UserName = username;
                //userSession.Password = user.MatKhau;
                Session.Add("LoginUser", userSession);
                
            }
            else
            {
                ModelState.AddModelError("", "đăng nhập không đúng ");
            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var dao = new TrangTraiDao() ;
                var result = dao.Login(model.username, model.password);
                if (result)
                {
                    var user = dao.getByUserName(model.username);
                    var userSession = new UserLogin();
                    userSession.UserName = user.TenDangNhap;
                    userSession.Password = user.MatKhau;
                    Session.Add("LoginUser", userSession);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "đăng nhập không đúng ");
                }
            }
            return View("Index");
        }
    }
}