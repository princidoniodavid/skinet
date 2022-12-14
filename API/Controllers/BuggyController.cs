using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    private readonly StoreContext _storeContext;

    public BuggyController(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        var thing = _storeContext.Products.Find(42);
        if (thing == null)
        {
            return NotFound(new ApiResponse(404));
        }

        return Ok();
    }

    [HttpGet("servererror")]
    public ActionResult GetServerRequest()
    {
        var thing = _storeContext.Products.Find(42);

        var thingToReturn = thing!.ToString();

        return Ok(thingToReturn);
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }

    [HttpGet("badrequest/{id}")]
    public ActionResult GetNotFoundRequest(int id)
    {
        return BadRequest();
    }

    [HttpGet("testauth")]
    [Authorize]
    public ActionResult<string> GetSecretText()
    {
        return "Secret stuff";
    }
}