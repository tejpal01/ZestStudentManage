using StudentManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentDto>> GetAllAsync();
        Task<StudentDto?> GetByIdAsync(int id);
        Task AddAsync(CreateStudentDto dto);
        Task UpdateAsync(UpdateStudentDto dto);
        Task DeleteAsync(int id);
    }
}
