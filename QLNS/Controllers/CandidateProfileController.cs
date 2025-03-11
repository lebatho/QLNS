using Microsoft.AspNetCore.Mvc;
using QLNS.Models;
using QLNS.Services;
using VoiceOtp.Models;

namespace QLNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateProfileController : ControllerBase
    {
        private readonly ICandidateProfileSevices _candidateServices;
        private readonly ILogger<CandidateProfileController> _logger;

        public CandidateProfileController(ILogger<CandidateProfileController> logger, ICandidateProfileSevices candidateServices)
        {
            _logger = logger;
            _candidateServices = candidateServices;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _candidateServices.GetAll();
                return Ok(new { message = "Danh sách hồ sơ ứng viên", data });
            }
            catch (Exception ex)
            {
                _logger.LogError("GET ALL CANDIDATE PROFILE ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }

        // Thêm ứng viên
        [HttpPost("CreateCandidate")]
        public async Task<IActionResult> CreateCandidate([FromBody] CandidateProfile model)
        {
            try
            {
                var result = await _candidateServices.CreateCandidate(model, "");
                if (!result.Item1)
                    return StatusCode(404, new ResponseData(404, result.Item2, null));

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("CREATE CANDIDATE ERROR: {0}", ex.Message);
                return StatusCode(500, new ResponseData(500, ex.Message, ex));
            }
        }

        // Cập nhật ứng viên
        [HttpPut("UpdateCandidate/{id}")]
        public async Task<IActionResult> UpdateCandidate([FromBody] CandidateProfile model, Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();

                var result = await _candidateServices.UpdateCandidate(model, tokenS);
                if (!result.Item1)
                    return StatusCode(404, new ResponseData(404, result.Item2, null));

                return Ok(new ResponseData(200, "Cập nhật ứng viên thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("UPDATE CANDIDATE ERROR: {0}", ex.Message);
                return StatusCode(500, new ResponseData(500, ex.Message, ex));
            }
        }

        // Xóa ứng viên
        [HttpDelete("DeleteCandidate/{id}")]
        public async Task<IActionResult> DeleteCandidate(Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();

                var result = await _candidateServices.DeleteCandidate(id, tokenS);
                if (!result.Item1)
                    return StatusCode(406, new ResponseData(406, result.Item2, null));

                return Ok(new ResponseData(200, "Xóa ứng viên thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("DELETE CANDIDATE ERROR: {0}", ex.Message);
                return StatusCode(500, new ResponseData(500, ex.Message, ex));
            }
        }
        [HttpGet("GetDetail/{id}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();

                var check = await _candidateServices.GetDetail(id, tokenS);
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
