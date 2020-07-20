using Framework.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace VaselinaWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        #region Acciones     

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {            
            var propertyInfos = typeof(Roles).GetFields().ToList();

            return Ok(propertyInfos.Select(x => x.Name).OrderBy(x => x));
        }
        #endregion
    }
}