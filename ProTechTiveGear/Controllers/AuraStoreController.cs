using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProTechTiveGear.Models;

namespace ProTechTiveGear.Controllers
{
    public class AuraStoreController : Controller
    {
		ProTechTiveGearEntities data = new ProTechTiveGearEntities();

		public ActionResult Signout()
		{
			FormsAuthentication.SignOut();
			//Response.Cookies.Clear();
			Session.Clear();

			return RedirectToAction("Index","AuraStore");

		}
		public ActionResult Search()
		{

			//var model = data.Items.Where(nv => nv.Name.Contains(search) || search == null).ToList();
			//return View(model);
			return PartialView();

		}
		private List<Item> NewItem(int count)
		{
			return data.Items.Where(d=>d.Active==true).OrderByDescending(a => a.DateImport).Take(count).ToList();
		}
		public ActionResult Index(string search)
        {
			var model = data.Items.Where(x => x.Active == true).OrderByDescending(c => c.DateImport).Where(nv => nv.Name.Contains(search) || search == null && nv.Active==true).ToList();
			
			return View(model);

		
		}
		public ActionResult Detail(int id)
		{
			var gear = from t in data.Items
					   where t.ID == id
					   select t;
			return View(gear.Single());
		}
		public ActionResult Menu()
		{
			var menu = from t in data.Menus select t;
			return PartialView(menu);
		}
		public ActionResult ItemMenuType(long id)
		{


			var b = (from t in data.ItemTypes where t.MenuID == id select t).ToList();

			return PartialView(b);
		}
		public ActionResult Brandtype(long id)
		{


			var c = (from d in data.Brands where d.MenuID == id select d).ToList();

			return PartialView(c);
		}
		public ActionResult ProductbyType(long id)
		{
			var pr = from d in data.Items where d.TypeID == id && d.Active==true select d;
			return View(pr);
		}
		public ActionResult BrandbyType(long id)
		{
			var pr = from d in data.Items where d.BrandID == id && d.Active == true select d;
			return View(pr);
		}
		public ActionResult Banner()
		{
			var bn = (from d in data.Banners select d).ToList();
			return PartialView(bn);
		}
		private List<Blog> NewBlogs(int count)
		{
			return data.Blogs.OrderByDescending(a => a.DateImport).Take(count).ToList();
		}
		public ActionResult Blog()
		{

			return View(NewBlogs(5));
		}
		public ActionResult BlogDetail(long id)
		{

			var blog = from t in data.Blogs
					   where t.ID == id
					   select t;
			return View(blog.Single());
		}
		public ActionResult RecentBlog()
		{

			return PartialView(NewBlogs(4));
		}
		public ActionResult Relatedproducts(long id)
		{
			var i = (from t in data.Items where t.BrandID == id && t.Active==true select t).Take(5).ToList();

			return PartialView(i);
		}
		public ActionResult Newdproducts()
		{
			
			return PartialView(NewItem(5));
		}
		//public ActionResult Helmetss()
		//{

		//		long id = 1;


		//	var i = data.ItemTypes.Find(
		//	var listitem=data.ItemTypes.Where(a=>a.ID==id)
		//		var temp = db.Clients.Find(id);
		//		var listorder = db.Orders.Where(o => o.IDClient == id);
		//		var lissordetai = db.OrderDetails.Where(d => d.Order.IDClient == id);
		//		ViewBag.listorder = listorder;
		//		//ViewBag.lissordetai = lissordetai;
		//		return View(new ClientManagerEntity(temp));

		//}
		public ActionResult Helmets()
		{
			long id = 2;
			var i = from t in data.Items
					join c in data.ItemTypes on t.TypeID equals c.ID
					//join d in data.Menus on c.MenuID equals d.ID
					where c.MenuID == id && t.Active==true
					select new { t, c };
			List<HelmetsEntity> listhl = new List<HelmetsEntity>();

			foreach (var info in i.ToList())
			{
				HelmetsEntity hl = new HelmetsEntity();
				hl.Name = info.t.Name;
				hl.Picture = info.t.Picture;
				hl.Quantity = info.t.Quantity;
				hl.Sellprice = info.t.SellPrice;
				hl.Status = info.t.ShortTitle;
				hl.Describe = info.t.Describe;
				listhl.Add(hl);
			}
			//long id = 2;
			//var temp = data.Menus.Find(id);
			//var a = data.ItemTypes.Where(b => b.MenuID == id);

			//var listorder = db.Orders.Where(o => o.IDClient == id);
			//var lissordetai = db.OrderDetails.Where(d => d.Order.IDClient == id);
			//ViewBag.listorder = listorder;
			////ViewBag.lissordetai = lissordetai;
			//return View(new ClientManagerEntity(temp));

			return View(listhl);
		}
		public ActionResult RiddingGear()
		{
			long id = 3;
			var i = from t in data.Items
					join c in data.ItemTypes on t.TypeID equals c.ID

					where c.MenuID == id && t.Active == true
					select new { t, c };
			List<HelmetsEntity> listhl = new List<HelmetsEntity>();

			foreach (var info in i.ToList())
			{
				HelmetsEntity hl = new HelmetsEntity();
				hl.Name = info.t.Name;
				hl.Picture = info.t.Picture;
				hl.Quantity = info.t.Quantity;
				hl.Sellprice = info.t.SellPrice;
				hl.Status = info.t.ShortTitle;
				hl.Describe = info.t.Describe;
				listhl.Add(hl);
			}


			return View(listhl);
		}

		public ActionResult Accsesories()
		{
			long id = 4;
			var i = from t in data.Items
					join c in data.ItemTypes on t.TypeID equals c.ID

					where c.MenuID == id && t.Active==true
					select new { t, c };
			List<HelmetsEntity> listhl = new List<HelmetsEntity>();

			foreach (var info in i.ToList())
			{
				HelmetsEntity hl = new HelmetsEntity();
				hl.Name = info.t.Name;
				hl.Picture = info.t.Picture;
				hl.Quantity = info.t.Quantity;
				hl.Sellprice = info.t.SellPrice;
				hl.Status = info.t.ShortTitle;
				hl.Describe = info.t.Describe;
				listhl.Add(hl);
			}


			return View(listhl);
		}
		//public ActionResult DetailProduct(long? id)
		//{

		//	var temp = data.Items.Find(id);

		//	return View(new ItemEntity(temp));
		//}
		public ActionResult DetailProduct(long id)
		{


			var i = from t in data.Items
					join c in data.ItemTypes on t.TypeID equals c.ID

					where t.ID.Equals(id)
					select t;
			List<HelmetsEntity> listhl = new List<HelmetsEntity>();

			foreach (var info in i)
			{
				HelmetsEntity hl = new HelmetsEntity();
				//var a = from t in data.Items where t.ID == hl.ID;
				hl.Name = info.Name;
				hl.Picture = info.Picture;
				hl.Quantity = info.Quantity;
				hl.Sellprice = info.SellPrice;
				hl.Status = info.ShortTitle;
				hl.Describe = info.Describe;
				listhl.Add(hl);
			}


			return View(listhl);

		}
		public ActionResult Brand()
		{

			var i = from t in data.Brands select t;
				

			return View(i.ToList());
		}
		public ActionResult Contact()
		{
			return View();
		}
		public ActionResult Sessionlogin()
		{
			return PartialView();
		}
		public ActionResult ListOrderClient()
		{
			var ac = (Customer)Session["usr"];
			if (ac == null)
			{
				return RedirectToAction("Login", "Acction");
			}
			
			var temp = data.Orders.Where(p => p.Customer.Username == ac.Username);
			List<OrderEntity> listProdcut = new List<OrderEntity>();
			foreach (var item in temp)
			{
				OrderEntity pr = new OrderEntity();
				pr.TypeOf_OrderEntity(item);
				listProdcut.Add(pr);
			}
			

			return View(listProdcut);

			
		}
		public ActionResult ListOrderDetailClient(long? id)
		{
			var temp = data.OrderDetails.Where(d => d.OrderID == id);
			List<OrderDetailEntity> listdetail = new List<OrderDetailEntity>();
			foreach (var item in temp)
			{
				OrderDetailEntity or = new OrderDetailEntity();
				or.TypeOf_OrderEntity(item);
				listdetail.Add(or);
			}
			
		
			return PartialView(listdetail);

		}
		[HttpGet]
		public ActionResult CancelOrder(long? id)
		{

			var temp = data.OrderDetails.Where(d => d.OrderID == id).ToList();
			data.OrderDetails.RemoveRange(temp);
			data.SaveChanges();
			var tem = data.Orders.SingleOrDefault(d => d.ID == id);
			data.Orders.Remove(tem);
			data.SaveChanges();
			return RedirectToAction("ListOrderClient");

		}
		public ActionResult Profile()
		{
			var ac = (Customer)Session["usr"];


			var t = from a in data.Customers where a.Username == ac.Username select a;


			return View(t.ToList());


		}
	}
}