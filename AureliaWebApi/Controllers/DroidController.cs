using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using Repository.DTO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AureliaWebApi.Controllers
{
    [Route("api/[controller]")]
    public class DroidsController : Controller
    {
        readonly IDroidRepository droidRepo;
        public DroidsController(IDroidRepository repository)
        {
            droidRepo = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var droids = droidRepo.GetAll();
            return new OkObjectResult(droids);
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        public IActionResult GetById(int id)
        {
            var droid = droidRepo.Get(id);
            if (droid == null)
            {
                return new NotFoundObjectResult(
                    new Error
                    {
                        HttpCode = 404,
                        Message = $"Droid with id:{id} - No such Droid in database!"
                    }
                );
            }
            return new OkObjectResult(droidRepo.Get(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Droid droid)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new Error
                {
                    HttpCode = 400,
                    Message = $"Invalid payload: {ModelState}"
                });
            }

            var result = droidRepo.Create(droid);
            return new CreatedAtRouteResult(nameof(GetById), new { id = droid.Id }, droid);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = droidRepo.Delete(id);

            if (result == null)
            {
                return new BadRequestObjectResult(new Error
                {
                    HttpCode = 404,
                    Message = "No such Droid in database!"
                });
            }

            return new NoContentResult();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Droid droid)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new Error
                {
                    HttpCode = 400,
                    Message = "Invalid payload"
                });
            }

            var result = droidRepo.Update(droid);

            if (result == null)
            {
                return new NotFoundObjectResult(new Error
                {
                    HttpCode = 410,
                    Message = "Could not find Droid in database!"
                });
            }

            return new OkObjectResult(droid);
        }
    }
    public class Error
    {
        public int HttpCode { get; set; }
        public string Message { get; set; }
    }
}
