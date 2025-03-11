using Microsoft.AspNetCore.Mvc;
using QLNS.Models;
using QLNS.Services;
using VoiceOtp.Models;

namespace QLNS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PositionController : ControllerBase
    {
        private readonly IPositionServices _positionServices;
        private readonly ILogger<PositionController> _logger;

        public PositionController(ILogger<PositionController> logger, IPositionServices positionServices)
        {
            _logger = logger;
            _positionServices = positionServices;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _positionServices.GetAll();
                return Ok(new { message = "Danh sách chức vụ", data });
            }
            catch (Exception ex)
            {
                _logger.LogError("GET ALL POSITIONS ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }
        [HttpPost("CreatePosition")]
        public async Task<IActionResult> CreatePosition([FromBody] Position model)
        {
            try
            {
                var check = await _positionServices.CreatePosition(model, "");
                if (!check.Item1) return StatusCode(404, new ResponseData(404, check.Item2, null));

                return Ok(check);
            }
            catch (Exception ex)
            {
                _logger.LogError("TEMPLATE INDEX : {0}", ex.Message);
                return StatusCode(1500, ex.Message);
            }
        }
        [HttpPut("UpdatePosition/{id}")]
        public async Task<IActionResult> UpdatePosition([FromBody] Position model, Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();


                var valid = await _positionServices.UpdatePosition(model, tokenS);
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


                var check = await _positionServices.DeletePosition(id, tokenS);
                if (!check.Item1) return StatusCode(406, new ResponseData(406, check.Item2, null));

                return StatusCode(200, new ResponseData(200, "Xóa bỏ chức vụ thành công!", null));
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

                var check = await _positionServices.GetDetail(id, tokenS);
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
