using MeetingsAPI_V2.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MeetingsAPI_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private readonly string _url = "http://contacts:5000/contacts/";
        private readonly string _url = "http://localhost/contacts/";

        static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                string responseBody = await client.GetStringAsync(_url);
                return Ok(JsonConvert.DeserializeObject<IEnumerable<User>>(responseBody));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                string responseBody = await client.GetStringAsync(_url + id);
                return Ok(JsonConvert.DeserializeObject<User>(responseBody));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return NotFound();
        }
    }
}
