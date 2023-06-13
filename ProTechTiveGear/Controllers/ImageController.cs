using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProTechTiveGear.Controllers
{
    public class ImageController : Controller
    {
		// GET: Image
		public ActionResult UploadImage()
		{
			return View();
		}
		public string ProcessUpload(HttpPostedFileBase file)
		{

			try
			{
				//validate (tự validate)
				// xử lí upload
				//var fileName = Path.GetFileName(file.FileName);
				//var path = Path.Combine(Server.MapPath("~/img/Item"), fileName);
				//if(file==null){
				//	ViewBag.Filenull = "Chose image please";

				//}
				//else
				//{
				//	if(ModelState.IsValid){
				//if (ModelState.IsValid)
				//{
				//	//file.SaveAs(Server.MapPath("~/img/Item/" + file.FileName));
				//	var fileName = Path.GetFileName(file.FileName);
				//	var path = Path.Combine(Server.MapPath("~/img/Item"),fileName);

				//	if (System.IO.File.Exists(file.FileName))

				//		ViewBag.ImageExist = "Inmage Exit";
				//	else
				//	{
				//		file.SaveAs(path);
				//	}

				//}
				//	}
				//}

				file.SaveAs(Server.MapPath("~/img/Item/" + file.FileName));
				
				if (System.IO.File.Exists(file.FileName))

						ViewBag.ImageExist = "Inmage Exit";
				else
				{
					return file.FileName;
				}
					
			}
			catch (Exception ex)
			{

				Debug.WriteLine(ex.Message);
			}
			
			return null;


		}
	}
}