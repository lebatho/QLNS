﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNS.Models;
using QLNS.Services;
using VoiceOtp.Models;

namespace QLNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeServices employeeSevices, ILogger<EmployeeController> logger)
        {
            _employeeServices = employeeSevices;
            _logger = logger;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _employeeServices.GetAll();
                return Ok(new { message = "Danh sách nhân viên", data });
            }
            catch (Exception ex)
            {
                _logger.LogError("GET ALL EMPLOYEES ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee model)
        {
            try
            {
                var check = await _employeeServices.CreateEmployee(model, "");
                if (!check.Item1) return StatusCode(404, new ResponseData(404, check.Item2, null));

                return Ok(check);
            }
            catch (Exception ex)
            {
                _logger.LogError("TEMPLATE INDEX : {0}", ex.Message);
                return StatusCode(1500, ex.Message);
            }
        }
        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee model, Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();


                var valid = await _employeeServices.UpdateEmployee(model, tokenS);
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
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var tokenS = auth.Split(' ').Last();


                var check = await _employeeServices.DeleteEmployee(id, tokenS);
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

