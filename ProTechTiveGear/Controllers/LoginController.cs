using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProTechTiveGear.Models;
namespace ProTechTiveGear.Controllers
{
    public class LoginController : Controller
    {
		// GET: Login
		ProTechTiveGearEntities db = new ProTechTiveGearEntities();
		
		public ActionResult Login()
		{
			return View();

		}
		[HttpPost]
		public ActionResult Login(FormCollection collection)
        {
			
			var userName = collection["userName"];
			var passWord = collection["passWord"];
			//passWord = Encryption.ComputeHash(passWord, "SHA512", GetBytes("acc"));
		
			Customer cs = db.Customers.SingleOrDefault(n => n.Username == userName && n.Passwords == passWord);
				//Customer cs = db.Customers.SingleOrDefault(n => n.Username == userName && n.Passwords == passWord);
				if (cs != null)
				{
					
					Session["usr"] = cs;
					return RedirectToAction("Index", "AuraStore");
				}
				else
					ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
			//}
			return View();
			
		}
		
		public ActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Register(FormCollection collection)
		{
			string userName = collection["Username"];
			string passWord = collection["Password"];
			string conFirmPassWord = collection["ConfirmPassword"];
			string name = collection["Name"];
			//var Birthday = String.Format("{0:MM/dd/yyyy}", collection["Birthday"]);
			string Email = collection["Email"];
			string address = collection["Address"];
			//int Gender = Convert.ToInt32(collection["Gender"]);
			string phoneNumber = collection["PhoneNumber"];
			//string picTure = collection["Picture"];
			//if (userName == "")
			//{
			//	ViewBag.username = "Please enter UserName";
			//}else
			//	if(passWord==""){
			//	ViewBag.password = "Please enter Password";
			//}
			//else
			//	if (conFirmPassWord !=passWord)
			//{
			//	ViewBag.password = "Confirm Password not correct";

			//}
			//else
			//	if (name == "")
			//{
			//	ViewBag.password = "Please enter name";
			//}
			//else
			//	if (Email == "")
			//{
			//	ViewBag.password = "Please enter Email";
			//}

			//else
			//	if (address == "")
			//{
			//	ViewBag.password = "Please enter Address";
			//}
			//else
			//	if (phoneNumber == "")
			//{
			//	ViewBag.password = "Please enter PhoneNumber";
			//}
			//passWord = Encryption.ComputeHash(passWord, "SHA512", GetBytes("acc"));
			//conFirmPassWord = Encryption.ComputeHash(conFirmPassWord, "SHA512", GetBytes("acc"));
			if (userName != null && passWord == conFirmPassWord)
			{
			
				
					var tem = db.Customers.SingleOrDefault(a => a.Username == userName);
					if (tem == null)
					{
						Customer cs = new Customer();
						cs.Username = userName;
						cs.Passwords = passWord;
						cs.Name = name;
						cs.EmailAddress = Email;
						//cs.Birthday = DateTime.Parse(Birthday);
						cs.Address = address;
						//cs.Gender = Gender;
						cs.Phone = phoneNumber;
						//cs.Picture = picTure;
						db.Customers.Add(cs);
						db.SaveChanges();
					}
			
				else
				{
					ModelState.AddModelError("", "Tài khoản đã tồn tại !");
					return View();
				}
				return RedirectToAction("Login", "Login");
			}
			else
			{
				ViewBag.Confirm = "Mật khẩu không trùng khớp";
			}

			return View();


		}
		public ActionResult Forgotpassword()
		{
			if (Session["usr"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			var ac = ((Customer)Session["usr"]);

			return View(new AccountClientEntity(ac));
		}
		[HttpPost]

		public ActionResult Forgotpassword(FormCollection fc)
		{
			//string userName = collection["Username"];
			//string passWord = collection["Password"];
			//string conFirmPassWord = collection["ConfirmPassword"];
			//string name = collection["Name"];
			//var Birthday = String.Format("{0:MM/dd/yyyy}", collection["Birthday"]);
			//string Email = collection["Email"];
			//string address = collection["Address"];
			//int Gender = Convert.ToInt32(collection["Gender"]);
			//string phoneNumber = collection["PhoneNumber"];
			var ac = ((Customer)Session["usr"]);

			if (Session["usr"] != null)
			{
				string userName = fc["userName"].ToString();
				string pass = fc["pass"].ToString();
				string newpass = fc["newpass"].ToString();
				string repass = fc["repass"].ToString();
				var temp = db.Customers.SingleOrDefault(x => x.Username == userName && x.Passwords == pass);
				if (temp != null && pass != "" && newpass != pass && newpass != "" && newpass == repass)
				{
					temp.Passwords = fc["newpass"];
					db.SaveChanges();
					Session["usr"] = temp;
					return RedirectToAction("Profile", "AuraStore");

				}



			}
			else
			{
				return RedirectToAction("Index", "AuraStore");
			}
			ModelState.AddModelError("", "Không thể thay đổi mật khẩu");
			return View(new AccountClientEntity(ac));
		}

		public ActionResult Changepassword()
		{
			if (Session["usr"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			var ac = ((Customer)Session["usr"]);

			return View(new AccountClientEntity(ac));
		}
		[HttpPost]

		public ActionResult Changepassword(FormCollection fc)
		{
			//string userName = collection["Username"];
			//string passWord = collection["Password"];
			//string conFirmPassWord = collection["ConfirmPassword"];
			//string name = collection["Name"];
			//var Birthday = String.Format("{0:MM/dd/yyyy}", collection["Birthday"]);
			//string Email = collection["Email"];
			//string address = collection["Address"];
			//int Gender = Convert.ToInt32(collection["Gender"]);
			//string phoneNumber = collection["PhoneNumber"];
			var ac = ((Customer)Session["usr"]);

			if (Session["usr"] != null)
			{
				string userName = fc["userName"].ToString();
				string pass = fc["pass"].ToString();
				string newpass = fc["newpass"].ToString();
				string repass = fc["repass"].ToString();
				//pass = Encryption.ComputeHash(pass, "SHA512", GetBytes("acc"));
				//newpass = Encryption.ComputeHash(newpass, "SHA512", GetBytes("acc"));
				//repass = Encryption.ComputeHash(repass, "SHA512", GetBytes("acc"));
				var temp = db.Customers.SingleOrDefault(x => x.Username == userName && x.Passwords == pass);
				if (temp != null && pass != "" && newpass != pass && newpass != "" && newpass == repass)
				{
					temp.Passwords = fc["newpass"];
					db.SaveChanges();
					Session["usr"] = temp;
					return RedirectToAction("Profile", "AuraStore");

				}



			}
			else
			{
				return RedirectToAction("Index", "AuraStore");
			}
			ModelState.AddModelError("", "Không thể thay đổi mật khẩu..");
			return View(new AccountClientEntity(ac));
		}
		public static byte[] GetBytes(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}
	}
}