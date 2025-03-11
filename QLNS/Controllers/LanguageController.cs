using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS.Models;
using QLNS.Services;
using VoiceOtp.Models;

namespace QLNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageServices _languageServices;
        private readonly ILogger<LanguageController> _logger;

        public LanguageController(ILanguageServices languageServices, ILogger<LanguageController> logger)
        {
            _languageServices = languageServices;
            _logger = logger;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _languageServices.GetAll();
                return Ok(new { message = "Danh sách chứng chỉ", data });
            }
            catch (Exception ex)
            {
                _logger.LogError("GET ALL LANGUAGES ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }

        [HttpPost("CreateLanguage")]
        public async Task<IActionResult> CreateLanguage([FromBody] Language model)
        {
            try
            {
                var check = await _languageServices.CreateLanguage(model, "");
                if (!check.Item1) return StatusCode(404, new ResponseData(404, check.Item2, null));

                return Ok(check);
            }
            catch (Exception ex)
            {
                _logger.LogError("CREATE LANGUAGE ERROR: {0}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateLanguage/{id}")]
        public async Task<IActionResult> UpdateLanguage([FromBody] Language model, Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();

                var valid = await _languageServices.UpdateLanguage(model, tokenS);
                if (!valid.Item1)
                    return StatusCode(404, new ResponseData(404, valid.Item2, null));

                return Ok(new ResponseData(200, "Cập nhật chững chỉ thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("UPDATE LANGUAGE ERROR: {0}", ex.Message);
                return StatusCode(500, new ResponseData(500, ex.Message, ex));
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteLanguage(Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();

                var check = await _languageServices.DeleteLanguage(id, tokenS);
                if (!check.Item1) return StatusCode(406, new ResponseData(406, check.Item2, null));

                return StatusCode(200, new ResponseData(200, "Xóa bỏ chứng chỉ thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("DELETE LANGUAGE ERROR: {0}", ex.Message);
                return StatusCode(500, new ResponseData(500, ex.Message, ex));
            }
        }
    }
}
