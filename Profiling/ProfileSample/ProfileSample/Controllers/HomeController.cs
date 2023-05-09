using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProfileSample.DAL;
using ProfileSample.Models;

namespace ProfileSample.Controllers
{
    public class HomeController : Controller
    {
        private const int BatchCount = 10;
        public ActionResult Index()
        {
            var context = new ProfileSampleEntities();

            var count = context.ImgSources.Count();
            var model = new List<ImageModel>();

            // looks like pagination
            for (int i = 0; i < count; i += BatchCount)
            {
                var imgs = context.ImgSources.OrderBy(x => x.Id)
                    .Skip(i).Take(BatchCount)
                    .Select(x => new ImageModel()
                    {
                        Name = x.Name,
                        Data = x.Data
                    });
                model.AddRange(imgs);
            }

            return View(model);
        }

        public ActionResult Convert()
        {
            var files = Directory.GetFiles(Server.MapPath("~/Content/Img"), "*.jpg");

            using (var context = new ProfileSampleEntities())
            {
                foreach (var file in files)
                {
                    using (var stream = new FileStream(file, FileMode.Open))
                    {
                        byte[] buff = new byte[stream.Length];

                        stream.Read(buff, 0, (int)stream.Length);

                        var entity = new ImgSource()
                        {
                            Name = Path.GetFileName(file),
                            Data = buff,
                        };

                        context.ImgSources.Add(entity);
                        context.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}