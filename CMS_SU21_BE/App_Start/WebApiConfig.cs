using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using CMS_SU21_BE.Common.Authentication;
using MySql.Data.MySqlClient;

namespace CMS_SU21_BE
{
    public static class WebApiConfig
    {
        public static MySqlConnection conn()
        {
           //string conn_string = "host=mysql-41915-0.cloudclusters.net;port=19690;database=cms_su21;username=admin;password=Oadiea2y;CHARSET=utf8;";
            //string conn_string = "host=mysql-47226-0.cloudclusters.net;port=19438;database=cms_su21;username=admin;password=INllOtWq;CHARSET=utf8;";
            string conn_string = "host=103.143.208.112;port=3307;database=cms_su21;username=root;password=123456;CHARSET=utf8;";
            MySqlConnection conn = new MySqlConnection(conn_string);
            return conn;
        }
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new AuthenticationAttribute());
            EnableCrossSiteRequests(config);
            AddRoutes(config);
        }

        private static void AddRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "api/{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void EnableCrossSiteRequests(HttpConfiguration config)
        {
            /*   var cors = new EnableCorsAttribute(
                   origins: "*",
                   headers: "*",
                   methods: "*");

               config.EnableCors(cors);*/
        }
    }
}
