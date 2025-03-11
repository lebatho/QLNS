using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS.Models;
using QLNS.Services;
using VoiceOtp.Models;

namespace QLNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateServivces _certificateServices;
        private readonly ILogger<CertificateController> _logger;

        public CertificateController(ILogger<CertificateController> logger, ICertificateServivces departmentSevices)
        {
            _logger = logger;
            _certificateServices = departmentSevices;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _certificateServices.GetAll();
                return Ok(new { message = "Danh sách bằng cấp", data });
            }
            catch (Exception ex)
            {
                _logger.LogError("GET ALL CERTIFICATE ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }
        [HttpPost("CreateCertificate")]
        public async Task<IActionResult> CreateDepartment([FromBody] Certificate model)
        {
            try
            {
                var check = await _certificateServices.CreateCertificate(model, "");
                if (!check.Item1) return StatusCode(404, new ResponseData(404, check.Item2, null));

                return Ok(check);
            }
            catch (Exception ex)
            {
                _logger.LogError("TEMPLATE INDEX : {0}", ex.Message);
                return StatusCode(1500, ex.Message);
            }
        }
        [HttpPut("UpdateCertificate/{id}")]
        public async Task<IActionResult> UpdateCertificate([FromBody] Certificate model, Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();


                var valid = await _certificateServices.UpdateCertificate(model, tokenS);
                if (!valid.Item1)
                    return StatusCode(404, new ResponseData(404, valid.Item2, null));

                return Ok(new ResponseData(200, "Cập nhật bằng cấp thành công!", null));
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


                var check = await _certificateServices.DeleteCertificate(id, tokenS);
                if (!check.Item1) return StatusCode(406, new ResponseData(406, check.Item2, null));

                return StatusCode(200, new ResponseData(200, "Xóa bỏ bằng cấp thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("DELETE CUSTOMER : {0}", ex.Message);
                return StatusCode(505, new ResponseData(505, ex.Message, ex));
            }
        }
    }
}
