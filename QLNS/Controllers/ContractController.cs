using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QLNS.Models;
using QLNS.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using VoiceOtp.Models;

namespace QLNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractServices _contractServices;
        private readonly ILogger<ContractController> _logger;

        public ContractController(IContractServices contractServices, ILogger<ContractController> logger)
        {
            _contractServices = contractServices;
            _logger = logger;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _contractServices.GetAll();
                return Ok(new { message = "Danh sách hợp đồng", data });
            }
            catch (Exception ex)
            {
                _logger.LogError("GET ALL CONTRACT ERROR: {0}", ex.Message);
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }

        // Thêm hợp đồng mới
        [HttpPost("CreateContract")]
        public async Task<IActionResult> CreateContract([FromBody] Contract model)
        {
            try
            {
                var result = await _contractServices.CreateContract(model, "");
                if (!result.Item1) return StatusCode(404, new ResponseData(404, result.Item2, null));

                return Ok(new ResponseData(200, "Tạo hợp đồng thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("CREATE CONTRACT ERROR: {0}", ex.Message);
                return StatusCode(500, new ResponseData(500, ex.Message, ex));
            }
        }

        // Cập nhật hợp đồng
        [HttpPut("UpdateContract/{id}")]
        public async Task<IActionResult> UpdateContract([FromBody] Contract model, Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var token = auth.Split(' ').Last();

                var result = await _contractServices.UpdateContract(model, token);
                if (!result.Item1)
                    return StatusCode(404, new ResponseData(404, result.Item2, null));

                return Ok(new ResponseData(200, "Cập nhật hợp đồng thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("UPDATE CONTRACT ERROR: {0}", ex.Message);
                return StatusCode(500, new ResponseData(500, ex.Message, ex));
            }
        }

        // Xóa hợp đồng
        [HttpDelete("DeleteContract/{id}")]
        public async Task<IActionResult> DeleteContract(Guid id)
        {
            try
            {
                var auth = HttpContext.Request.Headers["Authorization"].ToString();
                var token = auth.Split(' ').Last();

                var result = await _contractServices.DeleteContract(id, token);
                if (!result.Item1) return StatusCode(406, new ResponseData(406, result.Item2, null));

                return StatusCode(200, new ResponseData(200, "Xóa hợp đồng thành công!", null));
            }
            catch (Exception ex)
            {
                _logger.LogError("DELETE CONTRACT ERROR: {0}", ex.Message);
                return StatusCode(500, new ResponseData(500, ex.Message, ex));
            }
        }
    }
}
