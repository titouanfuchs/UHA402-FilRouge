using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShapeAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.Net;

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
            return Ok(_ShapesService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseShape>> Get(int id)
        {
            return Ok(_ShapesService.Get(id));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CreateShape shapeQuery)
        {
            return Ok(_ShapesService.EditShape(id, shapeQuery));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> Delete(int id)
        {
            bool deleted = _ShapesService.DeleteShape(id);

            if (deleted)
                return StatusCode(200, new BaseResponse("Forme supprimée avec succès"));

            return StatusCode(500, new BaseResponse("Une erreur s'est produite lors de la suppression de la forme"));
        }

        [HttpPost("Shape")]
        public async Task<ActionResult<BaseShape>> CreateShape([FromQuery] ShapeDimension shapeDimenstion, [FromQuery] ShapeType shapeType,[FromBody][Required] CreateShape createQuery)
        {
            try
            {
                _ShapesService.CreateShape(createQuery, shapeDimenstion, shapeType);

                return StatusCode(201, new BaseResponse($"New Shape created"));
            }
            catch(Exception e)
            {
                return StatusCode(500, new BaseResponse($"Error : {e.Message}"));
            }
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
