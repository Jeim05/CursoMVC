﻿using System.Web;
using System.Web.Optimization;

namespace CapaPresentacionAdmin
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include( // SE ELIMINA LA PALABRA SCRIPT ANTES DEL BUNDLE
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/complements").Include( // Se incluye el js que copiamos de la plantilla
                        "~/Scripts/fontawesome/all.min.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.responsive.js",
                        "~/Scripts/sweetalert.min.js",
                        "~/Scripts/loadingoverlay/loadingoverlay.min.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/scripts.js")); // Luego esto se debe poner en el layout


            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            //// para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));


            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js")); // Aqui agregamos el .bundle

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/DataTables/css/jquery.dataTables.css",
                 "~/Content/DataTables/css/responsive.dataTables.css",
                 "~/Content/sweetalert.css"
                 ));
        }
    }
}
