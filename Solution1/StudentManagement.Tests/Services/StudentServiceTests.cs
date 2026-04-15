using FluentAssertions;
using Moq;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;
using StudentManagement.Domain.Entities;
using Xunit;

namespace StudentManagement.Tests.Services
{
    public class StudentServiceTests
    {
        private readonly Mock<IStudentRepository> _repoMock;
        private readonly StudentService _service;

        public StudentServiceTests()
        {
            _repoMock = new Mock<IStudentRepository>();
            _service = new StudentService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnStudents()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Tej", Email = "tej@test.com", Age = 20, Course = "CS" }
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(students);

            var result = await _service.GetAllAsync();

            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result[0].Name.Should().Be("Tej");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnStudent_WhenExists()
        {
            var student = new Student { Id = 1, Name = "Tej" };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(student);

            var result = await _service.GetByIdAsync(1);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrow_WhenNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Student?)null);

            Func<Task> act = async () => await _service.GetByIdAsync(1);

            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Student not found");
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepository()
        {
            var dto = new CreateStudentDto
            {
                Name = "Tej",
                Email = "tej@test.com",
                Age = 20,
                Course = "CS"
            };

            await _service.AddAsync(dto);

            _repoMock.Verify(r => r.AddAsync(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdate_WhenExists()
        {
            var student = new Student { Id = 1 };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(student);

            var dto = new UpdateStudentDto
            {
                Id = 1,
                Name = "Updated",
                Email = "updated@test.com",
                Age = 22,
                Course = "IT"
            };

            await _service.UpdateAsync(dto);

            _repoMock.Verify(r => r.UpdateAsync(student), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Student?)null);

            var dto = new UpdateStudentDto { Id = 1 };

            Func<Task> act = async () => await _service.UpdateAsync(dto);

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldDelete_WhenExists()
        {
            var student = new Student { Id = 1 };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(student);

            await _service.DeleteAsync(1);

            _repoMock.Verify(r => r.DeleteAsync(student), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrow_WhenNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Student?)null);

            Func<Task> act = async () => await _service.DeleteAsync(1);

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}