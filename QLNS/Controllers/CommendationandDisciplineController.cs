using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS.Models;
using QLNS.Services;
using VoiceOtp.Models;

namespace QLNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommendationandDisciplineController : ControllerBase
    {
        private readonly ICommendationandDisciplineSevices _commendationandDisciplineSevices;
        private readonly ILogger<CommendationandDisciplineController> _logger;

        public CommendationandDisciplineController(ILogger<CommendationandDisciplineController> logger, ICommendationandDisciplineSevices commendationandDisciplinetSevices)
        {
            _logger = logger;
            _commendationandDisciplineSevices = commendationandDisciplinetSevices;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _commendationandDisciplineSevices.GetAll();
                return Ok(new { message = "Danh sách khen thưởng - kỷ luật", data });
            }
            catch (Exception ex)
            {
                _logger.LogError("GET ALL COMMENDATION AND DISCIPLINE ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }
        [HttpPost("CreateCommendationand")]
        public async Task<IActionResult> CreateCommendationand([FromBody] CommendationAndDisciplines model)
        {
            try
            {
                var check = await _commendationandDisciplineSevices.CreateCommendationAndDiscipline(model, "");
                if (!check.Item1) return StatusCode(404, new ResponseData(404, check.Item2, null));

                return Ok(check);
            }
            catch (Exception ex)
            {
                _logger.LogError("TEMPLATE INDEX : {0}", ex.Message);
                return StatusCode(1500, ex.Message);
            }
        }
        [HttpPut("UpdateCommendationAndDiscipline/{id}")]
        public async Task<IActionResult> UpdateCertificate([FromBody] CommendationAndDisciplines model, Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();


                var valid = await _commendationandDisciplineSevices.UpdateCommendationAndDiscipline(model, tokenS);
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
        [HttpDelete("DeleteCommendationAndDiscipline/{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();


                var check = await _commendationandDisciplineSevices.DeleteCommendationAndDiscipline(id, tokenS);
                if (!check.Item1) return StatusCode(406, new ResponseData(406, check.Item2, null));

                return StatusCode(200, new ResponseData(200, "Xóa bỏ tài khoản khách hàng thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("DELETE CUSTOMER : {0}", ex.Message);
                return StatusCode(505, new ResponseData(505, ex.Message, ex));
            }
        }
    }
}
