using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS.Models;
using QLNS.Services;
using VoiceOtp.Models;

namespace QLNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentServices _departmenServices;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentServices departmentServices)
        {
            _logger = logger;
            _departmenServices = departmentServices;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var data = await _departmenServices.GetAll();
                return Ok(new { message = "Danh sách phòng ban", data });
            }
            catch (Exception ex)
            {
                _logger.LogError("GET ALL DEPARTMENT ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });

            }
        }
        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] Department model)
        {
            try
            {
                var check = await _departmenServices.CreateDepartment(model, "");
                if (!check.Item1) return StatusCode(404, new ResponseData(404, check.Item2, null));

                return Ok(check);
            }
            catch (Exception ex)
            {
                _logger.LogError("TEMPLATE INDEX : {0}", ex.Message);
                return StatusCode(1500, ex.Message);
            }
        }
        [HttpPut("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartment([FromBody] Department model, Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();


                var valid = await _departmenServices.UpdateDepartment(model, tokenS);
                if (!valid.Item1)
                    return StatusCode(404, new ResponseData(404, valid.Item2, null));

                return Ok(new ResponseData(200, "Cập nhật chức vụ thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("UPDATE POSITION ERROR: {0}", ex.Message);
                return StatusCode(500, new ResponseData(500, ex.Message, ex));
            }
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();


                var check = await _departmenServices.DeleteDepartment(id, tokenS);
                if (!check.Item1) return StatusCode(406, new ResponseData(406, check.Item2, null));

                return StatusCode(200, new ResponseData(200, "Xóa bỏ tài khoản khách hàng thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("DELETE CUSTOMER : {0}", ex.Message);
                return StatusCode(505, new ResponseData(505, ex.Message, ex));
            }
        }
        [HttpGet("GetDetail/{id}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();

                var check = await _departmenServices.GetDetail(id, tokenS);
                if (!check.Item1) return StatusCode(406, new ResponseData(406, check.Item3, null));

                return StatusCode(200, new ResponseData(200, check.Item3, check.Item2));
            }
            catch (Exception ex)
            {
                _logger.LogError("GET DETAIL CANDIDATE ERROR: {0}", ex.Message);
                return StatusCode(505, new ResponseData(505, ex.Message, ex));
            }
        }
    }
}
