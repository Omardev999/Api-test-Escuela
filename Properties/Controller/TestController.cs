using Microsoft.AspNetCore.Mvc;
using Db_Escuela.Properties.Model;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Db_Escuela.Properties.Controller;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly TestModel testModel;
    private readonly IConfiguration _configuration;

    public TestController(IConfiguration configuration)
    {
        _configuration = configuration;
        testModel = new TestModel(configuration);
    }

    // [HttpGet("test-connection")]
    // public IActionResult TestConnection([FromQuery] string connectionString)
    // {
    //     if (string.IsNullOrEmpty(connectionString))
    //     {
    //         return BadRequest("Connection string is required");
    //     }
    //
    //     DbConnectionStringBuilder builder = testModel.GetBuilder();
    //     builder.ConnectionString = connectionString;
    //     string result = testModel.Matricula();
    //
    //     return Ok(new { message = result });
    // }

    [HttpGet("matricula1")]
    public IActionResult TestMatricula()
    {
        try
        {
            List<String> result = testModel.Matricula();
        
            if (result == null || result.Count == 0)
            {
                return Ok(new { matriculas = new List<String>(), message = "No data found" });
            }
        
            return Ok(new { matriculas = result, count = result.Count });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message, stackTrace = ex.StackTrace });
        }
    }

    
    // [HttpGet("matricula2")]
    // public List<String> TestMatricula2()
    // {
    //     try
    //     {
    //         List<String> result = testModel.Matricula();
    //         return result;
    //     }
    //     catch (Exception ex)
    //     {
    //         return new List<String> { ex.Message };
    //     }
    // }
    
    
    // [HttpGet("validate")]
    // public IActionResult ValidateConnection()
    // {
    //     bool isValid = testModel.ValidateConnection();
    //     return Ok(new { isValid = isValid });
    // }
}
