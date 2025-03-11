using Microsoft.AspNetCore.Mvc;
using QLNS.Models;
using QLNS.Services;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace QLNS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeKeepingController : ControllerBase
    {
        private readonly ITimeKeepingServices _timeKeepingService;
        private readonly ILogger<TimeKeepingController> _logger;

        public TimeKeepingController(ILogger<TimeKeepingController> logger, ITimeKeepingServices timeKeepingService)
        {
            _logger = logger;
            _timeKeepingService = timeKeepingService;
        }

        // 🔹 Lấy danh sách tất cả TimeKeeping
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var timeKeepings = await _timeKeepingService.GetAll();
                return Ok(new { message = "Danh sách chấm công", data = timeKeepings });
            }
            catch (Exception ex)
            {
                _logger.LogError("GET ALL TIMEKEEPING ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }

        // 🔹 Cập nhật TimeKeeping
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromBody] TimeKeeping model, Guid id)
        {
            try
            {
                var check = await _timeKeepingService.Update(id, model);
                if (!check.Item1) return StatusCode(404, new { message = check.Item2 });

                return Ok(new { message = "Cập nhật chấm công thành công!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("UPDATE TIMEKEEPING ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }

        // 🔹 Xóa TimeKeeping theo Id
        [HttpDelete("deleteById/{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            try
            {
                var check = await _timeKeepingService.DeleteById(id);
                if (!check.Item1) return StatusCode(404, new { message = check.Item2 });

                return Ok(new { message = "Xóa chấm công thành công!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("DELETE TIMEKEEPING ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }
    }
}
