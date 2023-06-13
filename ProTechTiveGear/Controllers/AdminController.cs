using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProTechTiveGear.Models;
namespace ProTechTiveGear.Controllers
{
	
    public class AdminController : Controller
    {
		// GET: Admin
		ProTechTiveGearEntities db = new ProTechTiveGearEntities();
		public ActionResult SignOut()
		{
			//FormsAuthentication.SignOut();
			Response.Cookies.Clear();
			return RedirectToAction("Login", "Admin");

		}
		public ActionResult Index()
        {
			DateTime dateTimeNow = DateTime.Now.Date;
			dateTimeNow = dateTimeNow.AddYears(-1);

			string[] dateX = new string[12];
			string[] data = new string[12];
			for (int i = 0; i < 12; i++)
			{

				dateX[i] = (dateTimeNow.Month.ToString() + "/" + dateTimeNow.Year.ToString()).ToString();
				var temp = db.Orders.Where(a => a.Orderdate.Value.Month == dateTimeNow.Month).Sum(s => s.Totalprice);
				if (temp == null)
				{
					temp = 0;
				}
				data[i] = temp.ToString();
				dateTimeNow = dateTimeNow.AddMonths(1);
			}
			ViewBag.dateX = dateX;
			ViewBag.data = data;

			// DatachartLine();
			var ac = (Admin)Session["Account"];
			if (ac == null)
			{
				return RedirectToAction("Login", "Admin");
			}
			else { return View(); }
			
        }
		public ActionResult Login()
		{
			return View();

		}
		[HttpPost]
		public ActionResult Login(FormCollection collection)
		{

			//var um = db.Admins.SingleOrDefault(o => o.Username.Equals(account.userName) && o.Passwords.Equals(account.passWord));
			//if (um != null)
			//{
			//	Session["Account"] = um;

			//	Response.Cookies["usr"].Value = account.userName;
			//	var name = db.Admins.SingleOrDefault(a => a.Username == account.userName).Name;
			//	Response.Cookies["Name"].Value = name;
			//	var atar = db.Admins.SingleOrDefault(a => a.Username == account.userName).Picture;

			//	if (atar == null || atar == "")
			//	{
			//		atar = "~/img/Item/avatar-default-icon.png";
			//	}
			//	Response.Cookies["avatar"].Value = atar;

			//	return RedirectToAction("Index", "Admin");
			//}
			//else
			//{
			//	ModelState.AddModelError("", "The user login or password  is incorrect..");
			var userName = collection["userName"];

			var passWord = collection["passWord"];


			Admin ad = db.Admins.SingleOrDefault(n => n.Username == userName && n.Passwords == passWord);
			if (ad != null)
			{
				Session["Account"] = ad;
				Response.Cookies["usr"].Value = ad.Username;

				var name = db.Admins.SingleOrDefault(a => a.Username == ad.Username).Name;
				Response.Cookies["Name"].Value = name;

				var atar = db.Admins.SingleOrDefault(a => a.Username == ad.Username).Picture;
				if (atar == null || atar == "")
				{
					atar = "~/img/Item/avatar-default-icon.png";
				}

				Response.Cookies["avatar"].Value = atar;

				return RedirectToAction("Index", "Admin");
			}
			else
			
				ModelState.AddModelError("", "The user login or password  is incorrect..");
			
			return View();


		
	}

		public ActionResult Create()
		{
			return View();
		}

		// POST: Admins/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Username,Passwords,Name,Picture")] Admin admin)
		{
			if (ModelState.IsValid)
			{
				db.Admins.Add(admin);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(admin);
		}



		///  
		/// </summary>
		/// <returns></returns>
		/// 


		//order
		public ActionResult ListOrder()
		{
			var temp = db.Orders.Where(o => o.Status == false).ToList();
			List<OrderEntity> lisorder = new List<OrderEntity>();
			foreach (var item in temp)
			{
				OrderEntity or = new OrderEntity();
				or.TypeOf_OrderEntity(item);
				lisorder.Add(or);


			}


			return View(lisorder);
		}

		// xacs nhan

		public ActionResult Comfirm(long ? id)
		{
			var temp = db.OrderDetails.Where(d => d.OrderID == id);
			List<OrderDetailEntity> listdetail = new List<OrderDetailEntity>();
			foreach (var item in temp)
			{
				OrderDetailEntity or = new OrderDetailEntity();
				or.TypeOf_OrderEntity(item);
				listdetail.Add(or);
			}
			ViewBag.Date = db.Orders.SingleOrDefault(a => a.ID == id).Deliverydate;
			ViewBag.id = id;
			return View(listdetail);

		}

		[HttpPost]

		public ActionResult Comfirm(FormCollection fc)
		{
			string date = fc["date"];
			long id = Convert.ToInt64(fc["id"]);
			var tem = db.Orders.SingleOrDefault(d => d.ID ==id);

			tem.Status = true;
			tem.Deliverydate = Convert.ToDateTime(date);
			db.SaveChanges();

		
			return RedirectToAction("ListOrder");

		}
		//-------------------------------------------
		public ActionResult AllListOrder()
		{
			var temp = db.Orders.ToList();
			List<OrderEntity> lisorder = new List<OrderEntity>();
			foreach (var item in temp)
			{
				OrderEntity or = new OrderEntity();
				or.TypeOf_OrderEntity(item);
				lisorder.Add(or);


			}


			return View(lisorder);
		}

		// xacs nhan

		public ActionResult OrderDetail(long? id)
		{
			var temp = db.OrderDetails.Where(d => d.OrderID == id);
			List<OrderDetailEntity> listdetail = new List<OrderDetailEntity>();
			foreach (var item in temp)
			{
				OrderDetailEntity or = new OrderDetailEntity();
				or.TypeOf_OrderEntity(item);
				listdetail.Add(or);
			}
			
			return View(listdetail);

		}
		
		public ActionResult Productnotsold()
		{

			//var results = from t1 in db.Items
			//			  where !(from t2 in db.Orders where t2.Orderdate == DateTime.Now
			//					  select t2.ID).Contains(t1.ID)
			//			  select t1;
			var results = from t1 in db.Items
						  where !(from t2 in db.Orders
								  join a in db.OrderDetails on t2.ID equals a.OrderID
								  where t2.Orderdate == DateTime.Now
								  select t2.ID).Contains(t1.ID)
						  select t1;
			return View(results.ToList());
		}
	}
}