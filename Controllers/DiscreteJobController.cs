using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using api_appNature.Models;
using Newtonsoft.Json;

namespace api_appNature.Controllers
{
    [ApiController]
    [Route("/api/v1")]
    public class DiscreteJobController : Controller
    {
        private readonly IConfiguration _configuration;

        public DiscreteJobController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetDiscreteJob")]
        public async Task<IActionResult> GetDiscreteJob(string DiscreteNbr)
        {
            string connectionString = _configuration.GetConnectionString("DBConnect");

            try
            {

                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pp_DemoGetDiscreteNbr", sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@DiscreteNbr", DiscreteNbr));
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            Console.WriteLine($"dt: {dt}");
                            if (dt.Rows.Count > 0)
                            {
                                DiscreteModel a = new DiscreteModel();
                                List<RoutingDiscreteModel> lst = new List<RoutingDiscreteModel>();

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    a.DiscreteID = Int32.Parse(dt.Rows[i]["DiscreteID"].ToString());
                                    a.DiscreteNbr = dt.Rows[i]["DiscreteNbr"].ToString();
                                    a.StartDate = DateTime.Parse(dt.Rows[i]["StartDate"].ToString());
                                    a.EndDate = DateTime.Parse(dt.Rows[i]["EndDate"].ToString());
                                    a.DiscreteQty = decimal.Parse(dt.Rows[i]["DiscreteQty"].ToString());
                                    a.InventoryID = Int32.Parse(dt.Rows[i]["InventoryID"].ToString());
                                    a.InventoryCD = dt.Rows[i]["InventoryCD"].ToString();
                                    a.InventoryDesc = dt.Rows[i]["InventoryDesc"].ToString();
                                    a.DepartmentID = dt.Rows[i]["DepartmentID"].ToString();
                                    a.Unit = dt.Rows[i]["Unit"].ToString();


                                    RoutingDiscreteModel b = new RoutingDiscreteModel();
                                    b.RoutingID = Int32.Parse(dt.Rows[i]["RoutingID"].ToString());
                                    b.RoutingNo = Int32.Parse(dt.Rows[i]["No"].ToString());
                                    b.RoutingCode = dt.Rows[i]["RoutingCode"].ToString();
                                    b.RoutingName = dt.Rows[i]["RoutingName"].ToString();
                                    lst.Add(b);
                                }
                                a.Routings = lst;

                                var serializedProduct = JsonConvert.SerializeObject(a);
                                return Ok(serializedProduct);
                            }
                            else
                            {
                                return NotFound("NOT FOUND.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetDiscreteJob: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }
        [HttpPost]
        [Route("SaveTrackingDiscreteJob")]
        public IActionResult SaveTrackingDiscreteJob([FromBody] TrackingDiscreteJobModel model)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DBConnect");

                string query = @"INSERT INTO dbo.MFGTRACKINGDISCRETEJOB (CompanyID, DepartmentID, DiscreteID, RoutingNO, PrevQty, Qty, TrackingDate)
                        VALUES (@CompanyID, @DepartmentID, @DiscreteID, @RoutingNO, @PrevQty, @Qty, @TrackingDate);";

                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, sqlcon))
                    {
                        // Thêm các tham số vào truy vấn SQL
                        cmd.Parameters.AddWithValue("@CompanyID", model.CompanyID);
                        cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        cmd.Parameters.AddWithValue("@DiscreteID", model.DiscreteID);
                        cmd.Parameters.AddWithValue("@RoutingNO", model.RoutingNO);
                        cmd.Parameters.AddWithValue("@PrevQty", model.PrevQty);
                        cmd.Parameters.AddWithValue("@Qty", model.Qty);
                        cmd.Parameters.AddWithValue("@TrackingDate", model.TrackingDate);

                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                        sqlcon.Close();
                        var respon = new
                        {
                            Message = "Chỉnh sửa công đoạn thành công",
                            Status = true
                        };
                        return Ok(respon);
                        // return StatusCode((int)HttpStatusCode.Created); 
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error SaveTrackingDiscreteJob: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

    }
}
