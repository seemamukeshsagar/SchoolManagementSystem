// Create Controllers/Api/BaseApiController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.DTOs;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BaseApiController : ControllerBase
{
    protected IActionResult HandlePagedResult<T>(PagedResult<T> result)
    {
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(new
        {
            result.TotalCount,
            result.PageSize,
            PageNumber = result.PageNumber,
            TotalPages = result.TotalPages,
            CurrentPage = result.PageNumber,
            HasNext = result.PageNumber < result.TotalPages,
            HasPrevious = result.PageNumber > 1
        }));
        
        return Ok(result.Items);
    }

    protected IActionResult HandleResult<T>(T result)
    {
        if (result == null) return NotFound();
        return Ok(result);
    }
}