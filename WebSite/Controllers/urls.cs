using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SfheraFront.Controllers
{
    public class urls
    {
        //url da aplicacao comunicacao com api
        public static readonly string api = ConfigurationManager.AppSettings["urlSistema"];
        //url vendas
        public static readonly string UrlVendas = api + "Relatorios/Vendas/";

        //montar menu dasboard
        public static readonly string UrlDasboardMenu = api + "Dashboards";
    }

}