using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShapeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShapeController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpPatch()]
        public async Task<IActionResult> Edit(int id)
        {
            return Ok();
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }

        [HttpPost("Shape")]
        public async Task<IActionResult> CreateShape()
        {
            return Ok();
        }

        [HttpPost("Group")]
        public async Task<IActionResult> CreateGroup()
        {
            return Ok();
        }

        [HttpPost("AddShapeToGroup")]
        public async Task<IActionResult> AddShapeToGroup(int shapeID, int groupID)
        {
            return Ok();
        }
    }
}
