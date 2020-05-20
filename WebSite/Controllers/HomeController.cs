using Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SfheraFront.Controllers
{
    public class HomeController : Controller
    {
        //helper
        Helper helper = new Helper();
        //link menu dasboard
        string UrlDasboardMenu = ConfigurationManager.AppSettings["urlSistema"] + "Dashboards";
        string UrlVendas = ConfigurationManager.AppSettings["urlSistema"] + "Relatorios/Vendas";

        public ActionResult Index()
        {

            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {
                var dados = "";
                string token = Session["TokenUsuario"].ToString();
                IRestResponse response = helper.RequisicaoRest(UrlDasboardMenu + "/", dados, token, "GET");
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.menu = JsonConvert.DeserializeObject<List<Dashboard>>(response.Content);
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Login");
        }




        //mostrar grafico 01 conforme menu
        public ActionResult MontarGrafico(string Layout1)
        {
            if (Session["EstaLogado"] != null && Session["TokenUsuario"] != null)
            {
                var dados = "";
                string token = Session["TokenUsuario"].ToString();


                IRestResponse response = helper.RequisicaoRest(UrlDasboardMenu + "/", dados, token, "GET");
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.menu = JsonConvert.DeserializeObject<List<Dashboard>>(response.Content);
                    foreach (Dashboard menu in ViewBag.menu)
                    {

                        if (Layout1 == menu.Layout)
                        {
                            ViewBag.grafico = Layout1;
                            return View();
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    return RedirectToAction("Index", "Home");

                }
            }
            return RedirectToAction("Index", "Login");
        }

      
    }
}