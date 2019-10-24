using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VirtualGameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace VirtualGameStore.Controllers
{
    public class CartController : Controller
    {
        private readonly PROG3050Context _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(PROG3050Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> Buy(decimal id)
        {

            if (GameExists(id))
            {
                var game = _context.Game.FindAsync(id);

                String carts = HttpContext.Session.GetString("cart");
                if (carts == null)
                {
                    List<Item> cart = new List<Item>();


                    cart.Add(new Item { Game = await game, Quantity = 1 });

                    setCartSession(cart);
                }
                else
                {
                    List<Item> cart = getCartSession();
                    int index = isExist(id);
                    if (index != -1)
                    {
                        cart[index].Quantity++;
                    }
                    else
                    {
                        cart.Add(new Item { Game = await game, Quantity = 1 });
                    }
                    setCartSession(cart);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Remove(decimal id)
        {
            List<Item> cart = getCartSession();
            int index = isExist(id);
            cart.RemoveAt(index);
            setCartSession(cart);
            return RedirectToAction("Index");
        }

        private int isExist(decimal id)
        {
            List<Item> cart = getCartSession();
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Game.Gameid.Equals(id))
                    return i;
            return -1;
        }
        
        private bool GameExists(decimal id)
        {
            return _context.Game.Any(e => e.Gameid == id);
        }

        public List<Item> getCartSession()
        {
            List<Item> carts = new List<Item>();
            String cart = HttpContext.Session.GetString("cart");
            String[] rows = cart.Split(",");
            foreach(String row in rows)
            {
                String[] items = row.Split(";");
                Game g = new Game();
                g.Gameid = Convert.ToDecimal(items[0]);
                g.Title = items[1];
                g.Price = Convert.ToDecimal(items[2]);
                int q = Convert.ToInt32(items[3]);

                Item item = new Item { Game = g, Quantity = q };

                carts.Add(item);
            }
            return carts;
        }

        public void setCartSession(List<Item> list)
        {
            int i = 0;
            String cart = "";
            foreach (Item item in list)
            {
                String id = item.Game.Gameid.ToString();
                String name = item.Game.Title;
                String price = item.Game.Price.ToString();
                String qty = item.Quantity.ToString();
                String row = id + ";" + name + ";" + price + ";" + qty;

                if (i == 0) { 
                    cart += row;
                }else{
                    cart += ",";
                    cart += row;
                }

                i++;
            }

            HttpContext.Session.SetString("cart", cart);
            
        }

    }
}