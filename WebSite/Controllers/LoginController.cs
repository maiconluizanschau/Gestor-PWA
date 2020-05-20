using Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiconFront.Controllers
{
    public class LoginController : Controller
    {

        //helper
        Helper helper = new Helper();

        ///urls
        string urlacessoplataforma = ConfigurationManager.AppSettings["urlSistema"] + "/token";


        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //autenticacao da plataforma
        public ActionResult LoginForm(FormCollection loginForm)
        {
            //acessar passar os dados vindos da view autenticacao
            var dados = "";
            dados += "&username=" + loginForm["Email"].ToString();
            dados += "&password=" + loginForm["Senha"].ToString();
            IRestResponse respondelogin = helper.Login(urlacessoplataforma, dados);
            if (respondelogin.StatusCode.ToString() == "OK")
            {
                //criacao da sessao 
                Session["EstaLogado"] = loginForm.ToString();
                Session["TokenUsuario"] = loginForm.ToString();

                string json = respondelogin.Content.ToString();
                UsuarioLoginDTO item = Newtonsoft.Json.JsonConvert.DeserializeObject<UsuarioLoginDTO>(json);
                int IDUsuario = item.IDUsuario;
                string email = item.Email;
                string nome = item.Nome;
                string token = item.access_token;
                UsuarioLoginDTO Token = new UsuarioLoginDTO();
                {
                    Token.access_token = token;
                };
                Session["IDUsuario"] = item.IDUsuario.ToString();
                Session["Nome"] = item.Nome.ToString();
                // Session["Email"] = item.Email.ToString();
                Session["TokenUsuario"] = item.access_token.ToString();
                return RedirectToAction("Index", "Home");

            }
            TempData["SuccessErrorS"] = "Ocorreu um problema ao processar sua autenticação!";
            return View("index");
        }

        //desconectar
        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("index");
            return RedirectToAction("Index", "Login");
        }

    }



}
