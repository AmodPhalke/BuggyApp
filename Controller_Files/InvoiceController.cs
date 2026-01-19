using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace BuggyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetInvoice()
        {
            var items = new List<Item>();

            string user = "ASHWINIAMOD_PHALK_SCHEMA_ZLPAM";
            string password = "<CURRENT_PASSWORD>";
            string connectString = "db.freesql.com:1521/23ai_34ui2";
            
            string connString = $"User Id={user};Password={password};Data Source={connectString};";

            using (var conn = new OracleConnection(connString))
            {
                conn.Open();

                string sql = "SELECT INVOICEID, CUSTOMERNAME FROM INVOICES";

                using (var cmd = new OracleCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new Item
                        {
                            name = reader["CUSTOMERNAME"].ToString(),
                            price = 0 // or fetch from another column if you have PRICE
                        });
                    }
                }
            }

            if (items.Count == 0)
                return NotFound("No invoice found");

            return Ok(new { items });
        }

        public class Item
        {
            public string name { get; set; }
            public double price { get; set; }
        }
    }
}