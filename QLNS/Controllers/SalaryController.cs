using Microsoft.AspNetCore.Mvc;
using QLNS.Models;
using QLNS.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace QLNS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;
        private readonly ILogger<SalaryController> _logger;

        public SalaryController(ILogger<SalaryController> logger, ISalaryService salaryService)
        {
            _logger = logger;
            _salaryService = salaryService;
        }

        // 🔹 Lấy danh sách tất cả Salary
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var salaries = await _salaryService.GetAll();
                return Ok(new { message = "Danh sách lương", data = salaries });
            }
            catch (Exception ex)
            {
                _logger.LogError("GET ALL SALARY ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }

        // 🔹 Cập nhật Salary
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromBody] Salary model, Guid id)
        {
            try
            {
                var check = await _salaryService.Update(id, model);
                if (!check.Item1) return StatusCode(404, new { message = check.Item2 });

                return Ok(new { message = "Cập nhật lương thành công!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("UPDATE SALARY ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }

        // 🔹 Xóa Salary theo Id
        [HttpDelete("deleteById/{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            try
            {
                var check = await _salaryService.DeleteById(id);
                if (!check.Item1) return StatusCode(404, new { message = check.Item2 });

                return Ok(new { message = "Xóa lương thành công!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("DELETE SALARY ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }
    }
}
