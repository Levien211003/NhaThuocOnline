using ProTechTiveGear.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace ProTechTiveGear.Controllers
{
	public class CartController : Controller
	{
		ProTechTiveGearEntities db = new ProTechTiveGearEntities();
		public List<CartEntity> GetCart()
		{
			List<CartEntity> lstCart = Session["Cart"] as List<CartEntity>;
			if (lstCart == null)
			{

				lstCart = new List<CartEntity>();
				Session["Cart"] = lstCart;
			}
			return lstCart;
		}
		public ActionResult AddtoCart(long id, string strURL)
		{
			List<CartEntity> lstcart = GetCart();
			//Kiem tra sách này tồn tại trong Session["Giohang"] chưa?

			CartEntity Product = lstcart.Find(n => n.IdItem == id);
			if (Product == null)
			{
				Product = new CartEntity(id);
				lstcart.Add(Product);
				return Redirect(strURL);
			}
			else
			{
				Product.Quantity++;
				return Redirect(strURL);
			}
		}
		public ActionResult Cart()
		{
			List<CartEntity> lstCart = GetCart();
			if (lstCart.Count == 0)
			{
				return RedirectToAction("Index", "AuraStore");
			}
			ViewBag.TotalQuantity = TotalQuantity();
			ViewBag.ToTalPrice = ToTalPrice();
			return View(lstCart);
		}

		private int TotalQuantity()
		{
			int iTongSoLuong = 0;
			List<CartEntity> lstcart = Session["Cart"] as List<CartEntity>;
			if (lstcart != null)
			{
				iTongSoLuong = lstcart.Sum(n => n.Quantity);
			}
			return iTongSoLuong;
		}

		private Double ToTalPrice()
		{
			double ToTal = 0;
			List<CartEntity> lstCart = Session["Cart"] as List<CartEntity>;
			if (lstCart != null)
			{
				ToTal = lstCart.Sum(n => n.TotalPrices);
			}
			return ToTal;
		}

		public ActionResult CartPartial()
		{
			ViewBag.TotalQuantity = TotalQuantity();
			ViewBag.TotalPrice = ToTalPrice();
			return PartialView();
		}
		//Cap nhat Giỏ hàng
		public ActionResult EditCart(long id, FormCollection f)
		{


			List<CartEntity> lstGiohang = GetCart();

			CartEntity item = lstGiohang.SingleOrDefault(n => n.IdItem == id);

			if (item != null)
			{
				item.Quantity = int.Parse(f["txtSoluong"].ToString());
			}
			return RedirectToAction("Cart");
		}
		//Xoa Giohang
		public ActionResult DeleteCart(long id)
		{

			List<CartEntity> lstGiohang = GetCart();

			CartEntity sanpham = lstGiohang.SingleOrDefault(n => n.IdItem == id);

			if (sanpham != null)
			{
				lstGiohang.RemoveAll(n => n.IdItem == id);
				return RedirectToAction("Cart");

			}
			if (lstGiohang.Count == 0)
			{
				return RedirectToAction("Index", "AuraStore");
			}
			return RedirectToAction("Cart");
		}

		public ActionResult DeleteAll()
		{

			List<CartEntity> lstGiohang = GetCart();
			lstGiohang.Clear();
			return RedirectToAction("Index", "AuraStore");
		}
		[HttpGet]
		public ActionResult Order()
		{
			if (Session["usr"] == null || Session["usr"].ToString()=="")
			{
				return RedirectToAction("Login", "Login");
			}
			if (Session["Cart"] == null)
			{
				return RedirectToAction("Index", "AuraStore");
			}
			List<CartEntity> listcart = GetCart();
			ViewBag.ToTalQuanttity = TotalQuantity();
			ViewBag.TotalPrice = ToTalPrice();

			return View(listcart);
		}
		public ActionResult Order(FormCollection collection)
		{
			Order or = new Order();
			Customer cus = (Customer)Session["usr"];
			
			List<CartEntity> crt = GetCart();
			or.CustomerID = cus.ID;
			or.Orderdate = DateTime.Now;
			//var DeliveryDate = string.Format("{0:MM/dd/yyyy}", collection["Deliverydate"]);
			or.Status = false;
			or.Totalprice = (decimal)ToTalPrice();
			db.Orders.Add(or);
			db.SaveChanges();
			foreach (var item in crt)
			{
				OrderDetail ordt = new OrderDetail();
				ordt.OrderID = or.ID;
				ordt.ItemId = item.IdItem;
				ordt.Quantity = item.Quantity;
				ordt.Totalprice = (decimal)item.Prices;
				db.OrderDetails.Add(ordt);


				var it = db.Items.Find(item.IdItem);
				it.Quantity = (it.Quantity) - item.Quantity;
				db.SaveChanges();
			}
			db.SaveChanges();
			Session["Cart"] = null;

			return RedirectToAction("OrderConfirmation", "Cart");

	}
		public ActionResult OrderConfirmation()
		{
			return View();
		}
		public ActionResult Newinformation()
		{
			if (Session["usr"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			var ac = ((Customer)Session["usr"]);

			return View(new AccountClientEntity(ac));
		}
		[HttpPost]
		public ActionResult Newinformation(FormCollection fc)
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
				string address = fc["address"].ToString();
				string phoneNumber = fc["phonenumber"].ToString();
				string name = fc["name"].ToString();
				string emailAdress = fc["email"].ToString();

				var temp = db.Customers.SingleOrDefault(x => x.Username == userName);
				if (temp != null && address != "" )
				{
					temp.Address = fc["address"];
					temp.Name = fc["name"];
					temp.Phone = fc["phonenumber"];
					temp.EmailAddress = fc["email"];
					db.SaveChanges();
					Session["usr"] = temp;
					return RedirectToAction("Order", "Cart");

				}



			}
			else
			{
				return RedirectToAction("Index", "AuraStore");
			}
			ModelState.AddModelError("", "Error cannot change Infomation..");
			return View(new AccountClientEntity(ac));
		}
	}
			
}