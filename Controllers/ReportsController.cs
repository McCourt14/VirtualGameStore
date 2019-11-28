using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.XlsIO;
using VirtualGameStore.Models;

namespace VirtualGameStore.Controllers
{
    public class ReportsController : Controller
    {
        private readonly PROG3050Context _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReportsController(PROG3050Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet, ActionName("GameDetail")]
        public ActionResult GameDetailReport()
        {
            //Create an instance of ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Set the default application version as Excel 2016
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
                //Opening a file from a stream
                FileStream fileStream = new FileStream("GameDetailReport.xls", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

                //Create a workbook with a worksheet
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);

                //Access first worksheet from the workbook instance
                IWorksheet worksheet = workbook.Worksheets[0];

                //Insert sample text into cell “A1”
                worksheet.Range["A1"].Text = "Game Details";
                worksheet.Range["A2"].Text = "Game Title";
                worksheet.Range["B2"].Text = "Price";
                worksheet.Range["C2"].Text = "Launch Date";
                worksheet.Range["D2"].Text = "Description";
                worksheet.Range["E2"].Text = "Category";
                worksheet.Range["F2"].Text = "Company";
                worksheet.Range["G2"].Text = "Platform";

                var gamelist = _context.Game.Include(g => g.Category).Include(g => g.Company).Include(g => g.Platform);
                int i = 3;
                foreach (Game g in gamelist.ToList())
                {
                    string launchDate = "";

                    if ((g.LaunchDate) != null)
                    {
                        launchDate = ((DateTime)g.LaunchDate).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        launchDate = "";
                    }
                    worksheet.Range["A" + i].Text = g.Title;
                    worksheet.Range["B" + i].Text = Convert.ToString(g.Price);
                    worksheet.Range["C" + i].Text = launchDate;
                    worksheet.Range["D" + i].Text = g.Description;
                    worksheet.Range["E" + i].Text = g.Category.Categoriname;
                    worksheet.Range["F" + i].Text = g.Company.CompanyName;
                    worksheet.Range["G" + i].Text = g.Platform.Platformname;
                    i++;
                }

                workbook.SaveAs(fileStream, ExcelSaveType.SaveAsXLS);
                String name = fileStream.Name;
                workbook.Close();
                excelEngine.Dispose();
                fileStream.Close();

                fileStream = new FileStream("GameDetailReport.xls", FileMode.Open);

                return File(fileStream, "application/ms-excel", fileStream.Name);
            }
        }

        [HttpGet, ActionName("GameList")]
        public ActionResult GameListReport()
        {
            //Create an instance of ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Set the default application version as Excel 2016
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
                //Opening a file from a stream
                FileStream fileStream = new FileStream("GameListReport.xls", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

                //Create a workbook with a worksheet
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);

                //Access first worksheet from the workbook instance
                IWorksheet worksheet = workbook.Worksheets[0];

                //Insert sample text into cell “A1”
                worksheet.Range["A1"].Text = "Game List";
                worksheet.Range["A2"].Text = "Game Title";
                worksheet.Range["B2"].Text = "Release Date";

                var gamelist = _context.Game.Include(g => g.Category).Include(g => g.Company).Include(g => g.Platform);
                int i = 3;
                foreach (Game g in gamelist.ToList())
                {
                    string launchDate = "";

                    if ((g.LaunchDate) != null)
                    {
                        launchDate = ((DateTime)g.LaunchDate).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        launchDate = "";
                    }
                    worksheet.Range["A" + i].Text = g.Title;
                    worksheet.Range["B" + i].Text = launchDate;
                    i++;
                }

                workbook.SaveAs(fileStream, ExcelSaveType.SaveAsXLS);
                String name = fileStream.Name;
                workbook.Close();
                excelEngine.Dispose();
                fileStream.Close();

                fileStream = new FileStream("GameListReport.xls", FileMode.Open);

                return File(fileStream, "application/ms-excel", fileStream.Name);
            }
        }

        [HttpGet, ActionName("MemberList")]
        public ActionResult MemberListReport()
        {
            //Create an instance of ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Set the default application version as Excel 2016
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
                //Opening a file from a stream
                FileStream fileStream = new FileStream("MemberListReport.xls", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

                //Create a workbook with a worksheet
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);

                //Access first worksheet from the workbook instance
                IWorksheet worksheet = workbook.Worksheets[0];

                //Insert sample text into cell “A1”
                worksheet.Range["A1"].Text = "Member List";
                worksheet.Range["A2"].Text = "Display Name";
                worksheet.Range["B2"].Text = "Start Date";

                var userlist = _context.User;
                int i = 3;
                foreach (User u in userlist.ToList())
                {
                    string startDate = "";

                    if ((u.StartDate) != null)
                    {
                        startDate = ((DateTime)u.StartDate).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        startDate = "";
                    }

                    worksheet.Range["A" + i].Text = u.DisplayName;
                    worksheet.Range["B" + i].Text = startDate;
                    i++;
                }

                workbook.SaveAs(fileStream, ExcelSaveType.SaveAsXLS);
                String name = fileStream.Name;
                workbook.Close();
                excelEngine.Dispose();
                fileStream.Close();

                fileStream = new FileStream("MemberListReport.xls", FileMode.Open);

                return File(fileStream, "application/ms-excel", fileStream.Name);
            }
        }

        [HttpGet, ActionName("MemberDetail")]
        public ActionResult MemberDetailReport()
        {
            //Create an instance of ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Set the default application version as Excel 2016
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
                //Opening a file from a stream
                FileStream fileStream = new FileStream("MemberDetailReport.xls", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

                //Create a workbook with a worksheet
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);

                //Access first worksheet from the workbook instance
                IWorksheet worksheet = workbook.Worksheets[0];

                //Insert sample text into cell “A1”
                worksheet.Range["A1"].Text = "Member Details";
                worksheet.Range["A2"].Text = "Display Name";
                worksheet.Range["B2"].Text = "User Type";
                worksheet.Range["C2"].Text = "Postal Code";
                worksheet.Range["D2"].Text = "Gender";
                worksheet.Range["E2"].Text = "Birth Day";
                worksheet.Range["F2"].Text = "Phone Number";

                var userlist = _context.User;
                int i = 3;
                foreach (User u in userlist.ToList())
                {
                    string birthDay = "";
                    string userType = "";

                    if ((u.BirthDate) != null)
                    {
                        birthDay = ((DateTime)u.BirthDate).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        birthDay = "";
                    }

                    if(u.Usertype == "99")
                    {
                        userType = "Admin";
                    }
                    else
                    {
                        userType = "Customer";
                    }
                    worksheet.Range["A" + i].Text = u.DisplayName;
                    worksheet.Range["B" + i].Text = userType;
                    worksheet.Range["C" + i].Text = u.PostCode;
                    worksheet.Range["D" + i].Text = u.Gender;
                    worksheet.Range["E" + i].Text = birthDay;
                    worksheet.Range["F" + i].Text = u.CellPhone;
                    i++;
                }

                workbook.SaveAs(fileStream, ExcelSaveType.SaveAsXLS);
                String name = fileStream.Name;
                workbook.Close();
                excelEngine.Dispose();
                fileStream.Close();

                fileStream = new FileStream("MemberDetailReport.xls", FileMode.Open);

                return File(fileStream, "application/ms-excel", fileStream.Name);
            }
        }

        [HttpGet, ActionName("WishList")]
        public ActionResult WishListReport()
        {
            //Create an instance of ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Set the default application version as Excel 2016
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
                //Opening a file from a stream
                FileStream fileStream = new FileStream("WishListReport.xls", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

                //Create a workbook with a worksheet
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);

                //Access first worksheet from the workbook instance
                IWorksheet worksheet = workbook.Worksheets[0];

                //Insert sample text into cell “A1”
                worksheet.Range["A1"].Text = "WishList";
                worksheet.Range["A2"].Text = "Game Title";
                worksheet.Range["B2"].Text = "Date Added";
                worksheet.Range["C2"].Text = "Display Name";

                var wishlist = _context.Wishlist.Include(w => w.User).Include(w => w.Game);
                int i = 3;
                foreach (Wishlist w in wishlist.ToList())
                {
                    string dateAdded = "";

                    if ((w.CreatedDatetime) != null)
                    {
                        dateAdded = ((DateTime)w.CreatedDatetime).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        dateAdded = "";
                    }

                    worksheet.Range["A" + i].Text = w.Game.Title;
                    worksheet.Range["B" + i].Text = dateAdded;
                    worksheet.Range["C" + i].Text = w.User.DisplayName;
                    i++;
                }

                workbook.SaveAs(fileStream, ExcelSaveType.SaveAsXLS);
                String name = fileStream.Name;
                workbook.Close();
                excelEngine.Dispose();
                fileStream.Close();

                fileStream = new FileStream("WishListReport.xls", FileMode.Open);

                return File(fileStream, "application/ms-excel", fileStream.Name);
            }
        }

        [HttpGet, ActionName("Sales")]
        public ActionResult SalesReport()
        {
            //Create an instance of ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Set the default application version as Excel 2016
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;
                //Opening a file from a stream
                FileStream fileStream = new FileStream("SalesReport.xls", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

                //Create a workbook with a worksheet
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);

                //Access first worksheet from the workbook instance
                IWorksheet worksheet = workbook.Worksheets[0];

                //Insert sample text into cell “A1”
                worksheet.Range["A1"].Text = "Sales";
                worksheet.Range["A2"].Text = "Game Title";
                worksheet.Range["B2"].Text = "Display Name";
                worksheet.Range["C2"].Text = "Price";
                worksheet.Range["D2"].Text = "Quantity";
                worksheet.Range["E2"].Text = "Date";

                var orders = _context.Order.Include(o => o.User).Include(o => o.Game);
                int i = 3;
                foreach (Order o in orders.ToList())
                {
                    string orderDate = "";

                    if ((o.OrderDate) != null)
                    {
                        orderDate = ((DateTime)o.OrderDate).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        orderDate = "";
                    }

                    worksheet.Range["A" + i].Text = o.Game.Title;
                    worksheet.Range["B" + i].Text = o.User.DisplayName;
                    worksheet.Range["C" + i].Text = o.OrderPrice.ToString();
                    worksheet.Range["D" + i].Text = o.OrderCount.ToString();
                    worksheet.Range["E" + i].Text = orderDate;
                    i++;
                }

                workbook.SaveAs(fileStream, ExcelSaveType.SaveAsXLS);
                String name = fileStream.Name;
                workbook.Close();
                excelEngine.Dispose();
                fileStream.Close();

                fileStream = new FileStream("SalesReport.xls", FileMode.Open);

                return File(fileStream, "application/ms-excel", fileStream.Name);
            }
        }
    }
}