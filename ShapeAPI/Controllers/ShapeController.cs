using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShapeAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace ShapeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShapeController : ControllerBase
    {
        private ShapesService _ShapesService;

        public ShapeController(ShapesService shapesService)
        {
            _ShapesService = shapesService;
        }

        [HttpGet()]
        public async Task<ActionResult<GetAllShapesResponse>> GetAll()
        {
            return Ok(new GetAllShapesResponse("Sucess", new List<Shape3DGroup>(), new List<ShapeGroup>()));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
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
        public async Task<IActionResult> AddShapeToGroup([FromQuery][Required] int shapeID, [FromQuery][Required] int groupID)
        {
            return Ok();
        }
    }
}
