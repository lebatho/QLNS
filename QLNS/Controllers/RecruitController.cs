using Microsoft.AspNetCore.Mvc;
using QLNS.Models;
using QLNS.Services;
using VoiceOtp.Models;

namespace QLNS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecruitController : ControllerBase
    {
        private readonly IRecruitServices _recruitServices;
        private readonly ILogger<RecruitController> _logger;

        public RecruitController(ILogger<RecruitController> logger, IRecruitServices recruitServices)
        {
            _logger = logger;
            _recruitServices = recruitServices;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _recruitServices.GetAll();
                return Ok(new { message = "Danh sách tuyển dụng", data });
            }
            catch (Exception ex)
            {
                _logger.LogError("GET ALL RECRUITMENTS ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }
        [HttpPost("CreateRecruit")]
        public async Task<IActionResult> CreateRecruit([FromBody] Recruit model)
        {
            try
            {
                var check = await _recruitServices.CreateRecruit(model, "");
                if (!check.Item1) return StatusCode(404, new ResponseData(404, check.Item2, null));

                return Ok(check);
            }
            catch (Exception ex)
            {
                _logger.LogError("CREATE RECRUIT ERROR: {0}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateRecruit")]
        public async Task<IActionResult> UpdateRecruit([FromBody] Recruit model)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();

                var valid = await _recruitServices.UpdateRecruit(model, tokenS);
                if (!valid.Item1)
                    return StatusCode(404, new ResponseData(404, valid.Item2, null));

                return Ok(new ResponseData(200, "Cập nhật tuyển dụng thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("UPDATE RECRUIT ERROR: {0}", ex.Message);
                return StatusCode(500, new ResponseData(500, ex.Message, ex));
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRecruit(Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();

                var check = await _recruitServices.DeleteRecruit(id, tokenS);
                if (!check.Item1) return StatusCode(406, new ResponseData(406, check.Item2, null));

                return StatusCode(200, new ResponseData(200, "Xóa tuyển dụng thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("DELETE RECRUIT ERROR: {0}", ex.Message);
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

                var check = await _recruitServices.GetDetail(id, tokenS);
                if (!check.Item1) return StatusCode(406, new ResponseData(406, check.Item3, null));

                return StatusCode(200, new ResponseData(200, check.Item3, check.Item2));
            }
            catch (Exception ex)
            {
                _logger.LogError("DELETE RECRUIT ERROR: {0}", ex.Message);
                return StatusCode(505, new ResponseData(505, ex.Message, ex));
            }
        }
    }
}
