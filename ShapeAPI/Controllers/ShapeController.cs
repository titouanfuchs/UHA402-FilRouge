using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace basicForms.Controllers
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

        /// <summary>
        /// Retourne toutes les formes de tous les groupes
        /// </summary>
        [HttpGet()]
        public ActionResult<GetAllShapesResponse> GetAll()
        {
            return Ok(_ShapesService.GetAll());
        }

        /// <summary>
        /// Retourne une seule forme par son Identifiant
        /// </summary>
        /// <param name="id">Identifiant de la forme</param>
        [HttpGet("{id}")]
        public ActionResult<BaseShape> Get(int id)
        {
            return Ok(_ShapesService.GetShape(id));
        }

        /// <summary>
        /// Permet l'édition d'une forme par son ID.
        /// </summary>
        /// <param name="id">Identifiant de la forme</param>
        [HttpPatch("{id}")]
        public ActionResult<BaseShape> Edit(int id, [FromBody] CreateShape shapeQuery)
        {
            try
            {
                return Ok(_ShapesService.EditShape(id, shapeQuery));
            }
            catch(Exception e)
            {
                return StatusCode(500, new BaseResponse($"Shape with ID {id} not found."));
            }
        }

        /// <summary>
        /// Permet la suppression d'une forme par son ID.
        /// </summary>
        /// <param name="id">Identifiant de la forme</param>
        [HttpDelete("{id}")]
        public ActionResult<BaseShape> Delete(int id)
        {
            try
            {
                _ShapesService.DeleteShape(id);
                return StatusCode(200, new BaseResponse("Forme supprimée avec succès"));
            }
            catch(Exception e)
            {
                return StatusCode(501, new BaseResponse($"Error : {e.Message}"));
            }
        }

        /// <summary>
        /// Permet la création d'une forme.
        /// </summary>
        /// <param name="shapeType">Type de la forme.
        ///     0: Rectangle
        ///     1: Circle
        ///     2: Triangle
        /// </param>
        [HttpPost("Shape")]
        public ActionResult<BaseShape> CreateShape([FromQuery] ShapeType shapeType,[FromBody][Required] CreateShape createQuery)
        {
            try
            {
                BaseShape shape = _ShapesService.CreateShape(createQuery, shapeType);

                return StatusCode(201, shape);
            }
            catch(Exception e)
            {
                return StatusCode(500, new BaseResponse($"Error : {e.Message}"));
            }
        }

        /// <summary>
        /// Permet la création de groupes de formes.
        /// </summary>
        /// <param name="groupName">Nom du groupe</param>
        [HttpPost("Group")]
        public ActionResult<ShapeGroup> CreateGroup([FromQuery][Required] string groupName)
        {
            return Ok(_ShapesService.CreateGroup(groupName));
        }

        /// <summary>
        /// Permet l'ajout d'une forme dans un groupe de formes.
        /// </summary>
        /// <param name="shapeID">Identifiant de la forme.</param>
        /// <param name="groupID">Identifiant du groupe.</param>
        [HttpPost("AddShapeToGroup")]
        public ActionResult<string> AddShapeToGroup([FromQuery][Required] int shapeID, [FromQuery][Required] int groupID)
        {
            try
            {
                _ShapesService.AddShapeToGroup(groupID, shapeID);
                return Ok("Shape added to group successfully.");
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Error : {e.Message}");
            }
        }
    }
}
