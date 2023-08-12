/******************************************
 * AUTHOR:          Shah-MI
 * CREATION:       2023-08-12
 ******************************************/

using Microsoft.AspNetCore.Mvc;
using RESTful.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RESTful.Controllers.v1
{
    /// <summary>
    /// CRUD - WeatherForecast
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        #region constructore

        private readonly LinkGenerator _linkGenerator;

        public WeatherForecastController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        #endregion

        #region crud

        [SwaggerOperation(
            Summary = "Get WeatherForecast",
            Description = "Get WeatherForecast",
            OperationId = "WeatherForecast.Get",
            Tags = new[] { "WeatherForecast" })
        ]
        [HttpGet]
        [ProducesResponseType(typeof(List<WeatherForecast>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> Get()
        {
            var response = await GenerateLinks(data);
            return Ok(response);
            return Ok(response);
        }


        [SwaggerOperation(
            Summary = "Get a WeatherForecast",
            Description = "Get a WeatherForecast by Id",
            OperationId = "WeatherForecast.Get",
            Tags = new[] { "WeatherForecast" })
        ]
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(WeatherForecast), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> Get(int id)
        {
            var response = data.Find(_ => _.Id == id);
            if (response == null) return BadRequest(response);
            response = await GenerateLink(response);
            return Ok(response);
            return Ok(response);
        }

        [SwaggerOperation(
            Summary = "Post WeatherForecast",
            Description = "Create WeatherForecast",
            OperationId = "WeatherForecast.Get",
            Tags = new[] { "WeatherForecast" })
        ]
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual IActionResult Post([FromBody] CreateWeatherForecast request)
        {
            var newEntity = new WeatherForecast()
            {
                Id = Random.Shared.Next(2, 1000),
                Date = request.Date,
                TemperatureC = request.TemperatureC,
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
            data.Add(newEntity);
            var url = $"WeatherForecast/{newEntity.Id}";
            return Created(url, newEntity.Id);
            return Created(url, newEntity.Id);
        }

        [SwaggerOperation(
            Summary = "Put WeatherForecast",
            Description = "Update WeatherForecast",
            OperationId = "WeatherForecast.Put",
            Tags = new[] { "WeatherForecast" })
        ]
        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual IActionResult Put([FromBody] UpdateWeatherForecast request)
        {
            var response = data.Find(_ => _.Id == request.Id);
            if (response == null) return BadRequest(response);
            response.Edit(request.Date, request.TemperatureC);
            return Ok(response);
            return Ok(response);
        }


        [SwaggerOperation(
            Summary = "Delete WeatherForecast",
            Description = "Delete WeatherForecast",
            OperationId = "WeatherForecast.Delete",
            Tags = new[] { "WeatherForecast" })
        ]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual IActionResult Delete(int id)
        {
            var response = data.Find(_ => _.Id == id);
            if (response == null) return BadRequest(response);
            var result = data.Remove(response);
            return Ok(result);
            return Ok(result);
        }

        #endregion

        #region func

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static List<WeatherForecast> data = new List<WeatherForecast>
        {
            new WeatherForecast()
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }
        };


        // Generate List -> Links
        private async Task<List<WeatherForecast>> GenerateLinks(
            List<WeatherForecast> model)
        {
            foreach (var item in model)
            {
                item.Links = new List<Link>
                {
                    new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Get), values: new { item.Id }),
                        "self", "GET"),
                    new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Put), values: new { item.Id }),
                        "delete", "delete"),
                    new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Put)), "Update", "PUT"),
                };
            }

            return await Task.FromResult(model);
        }

        // Generate List -> Link
        private async Task<WeatherForecast> GenerateLink(WeatherForecast dto)
        {
            dto.Links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Get), values: new { dto.Id }),
                    "self", "GET"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Put), values: new { dto.Id }),
                    "delete", "delete"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Put)), "Update", "PUT"),
            };
            return await Task.FromResult(dto);
        }

        #endregion
    }
}