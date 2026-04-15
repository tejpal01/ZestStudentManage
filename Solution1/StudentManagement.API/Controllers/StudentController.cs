using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Common;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<List<StudentDto>>.SuccessResponse(data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null)
                return NotFound(ApiResponse<string>.Failure("Student not found"));

            return Ok(ApiResponse<StudentDto>.SuccessResponse(data));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentDto dto)
        {
            await _service.AddAsync(dto);
            return Ok(ApiResponse<string>.SuccessResponse("", "Student created"));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudentDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok(ApiResponse<string>.SuccessResponse("", "Student updated"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse("", "Student deleted"));
        }
    }
}
