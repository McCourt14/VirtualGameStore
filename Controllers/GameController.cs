using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirtualGameStore.Models;
using Syncfusion.XlsIO;
using System.IO;

namespace VirtualGameStore.Controllers
{
    public class GameController : Controller
    {
        private readonly PROG3050Context _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GameController(PROG3050Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Game
        public async Task<IActionResult> Index()
        {
            IdentityUser identityUser = _userManager.GetUserAsync(User).Result;
            if (User.Identity.IsAuthenticated && _userManager.IsInRoleAsync(identityUser, "administrators").Result)
            {
                ViewBag.isAdmin = "true";
            }
            var pROG3050Context = _context.Game.Include(g => g.Category).Include(g => g.Company).Include(g => g.Platform);
            return View(await pROG3050Context.ToListAsync());
        }

        // GET: Game/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            IdentityUser identityUser = _userManager.GetUserAsync(User).Result;
            if (User.Identity.IsAuthenticated && _userManager.IsInRoleAsync(identityUser, "administrators").Result)
            {
                ViewBag.isAdmin = "true";
            }

            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Category)
                .Include(g => g.Company)
                .Include(g => g.Platform)
                .FirstOrDefaultAsync(m => m.Gameid == id);
            if (game == null)
            {
                return NotFound();
            }

            var gamerates = _context.Gamerates.Where(c => c.Gameid == id).Include(c=>c.User);
            if (gamerates != null)
            {
                game.Gamerates = gamerates.ToList();
            }

            double sum = 0;
            double avgRates = 0;
            int count = 0;
            foreach(Gamerates gr in gamerates.ToList())
            {
                double drates = 0;
                count++;
                try {
                    drates = Convert.ToDouble(gr.Rates);
                } catch(Exception ex)
                {}
                sum += drates;
            }
            
            if (count > 0)
            {
                avgRates = Math.Round(sum / count, 2);
            }

            ViewBag.avgRates = avgRates;

            return View(game);
        }

        // GET: Game/Create
        public IActionResult Create()
        {
            IdentityUser identityUser = _userManager.GetUserAsync(User).Result;
            if (User.Identity.IsAuthenticated && _userManager.IsInRoleAsync(identityUser, "administrators").Result)
            {
                ViewBag.isAdmin = "true";
            }

            ViewData["Categoryid"] = new SelectList(_context.Category, "Categoryid", "Categoriname");
            ViewData["Companyid"] = new SelectList(_context.Company, "Companyid", "CompanyName");
            ViewData["Platformid"] = new SelectList(_context.Platform, "Platformid", "Platformname");
            return View();
        }

        // POST: Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Gameid,Title,Companyid,Price,LaunchDate,Platformid,Categoryid,Description,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.CreatedDatetime = DateTime.Now;
                game.UpdatedDatetime = DateTime.Now;
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categoryid"] = new SelectList(_context.Category, "Categoryid", "Categoriname", game.Categoryid);
            ViewData["Companyid"] = new SelectList(_context.Company, "Companyid", "CompanyName", game.Companyid);
            ViewData["Platformid"] = new SelectList(_context.Platform, "Platformid", "Platformname", game.Platformid);
            return View(game);
        }

        // GET: Game/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["Categoryid"] = new SelectList(_context.Category, "Categoryid", "Categoriname", game.Categoryid);
            ViewData["Companyid"] = new SelectList(_context.Company, "Companyid", "CompanyName", game.Companyid);
            ViewData["Platformid"] = new SelectList(_context.Platform, "Platformid", "Platformname", game.Platformid);
            return View(game);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Gameid,Title,Companyid,Price,LaunchDate,Platformid,Categoryid,Description,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Game game)
        {
            if (id != game.Gameid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    game.UpdatedDatetime = DateTime.Now;
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Gameid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categoryid"] = new SelectList(_context.Category, "Categoryid", "Categoriname", game.Categoryid);
            ViewData["Companyid"] = new SelectList(_context.Company, "Companyid", "CompanyName", game.Companyid);
            ViewData["Platformid"] = new SelectList(_context.Platform, "Platformid", "Platformname", game.Platformid);
            return View(game);
        }

        // GET: Game/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Category)
                .Include(g => g.Company)
                .Include(g => g.Platform)
                .FirstOrDefaultAsync(m => m.Gameid == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var game = await _context.Game.FindAsync(id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(decimal id)
        {
            return _context.Game.Any(e => e.Gameid == id);
        }

        [HttpGet, ActionName("Print")]
        public ActionResult PrintExcel()
        {
            //Create an instance of ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Set the default application version as Excel 2016
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;

                //DateTime 
                String dt = DateTime.Now.ToString("yyy-MM-dd_HHmmss");
                //Opening a file from a stream
                FileStream fileStream = new FileStream("GameList_"+ dt + ".xlsx", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

                //Create a workbook with a worksheet
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);

                //Access first worksheet from the workbook instance
                IWorksheet worksheet = workbook.Worksheets[0];

                //Insert sample text into cell “A1”
                worksheet.Range["A1"].Text = "Game List";
                worksheet.Range["A2"].Text = "Game Title";
                worksheet.Range["B2"].Text = "Price";
                worksheet.Range["C2"].Text = "Launch Date";
                worksheet.Range["D2"].Text = "Description";
                worksheet.Range["E2"].Text = "Category";
                worksheet.Range["F2"].Text = "Company";
                worksheet.Range["G2"].Text = "Platform";

                var gamelist = _context.Game.Include(g => g.Category).Include(g => g.Company).Include(g => g.Platform);
                int i = 3;
                foreach(Game g in gamelist.ToList())
                {
                    worksheet.Range["A" + i].Text = g.Title;
                    worksheet.Range["B" + i].Text = Convert.ToString(g.Price);
                    worksheet.Range["C" + i].Text = ((DateTime)g.LaunchDate).ToString("yyyy/MM/dd");
                    worksheet.Range["D" + i].Text = g.Description;
                    worksheet.Range["E" + i].Text = g.Category.Categoriname;
                    worksheet.Range["F" + i].Text = g.Company.CompanyName;
                    worksheet.Range["G" + i].Text = g.Platform.Platformname;
                    i++;
                }

                //Save the workbook to disk in xlsx format
                workbook.SaveAs(fileStream, ExcelSaveType.SaveAsXLS);
                String name = fileStream.Name;
                workbook.Close();
                excelEngine.Dispose();
                fileStream.Close();

                ViewBag.PrintMessage = "Game List Excel file is created: "+name;
            }

            return RedirectToAction("Index");
        }
     
    }

}
