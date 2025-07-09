using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController: ControllerBase
    {
        private static readonly List<TodoItem> _todoItems = [];
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return Ok(_todoItems);
        }
 
        [HttpGet("{id}")]

        public ActionResult<TodoItem> Get(int id)
        {
            var todoItem = _todoItems.FirstOrDefault(x => x.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }
        //POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] TodoItem todoItem)
        {
            _todoItems.Add(todoItem);
            //return Ok(todoItem);
            return CreatedAtAction(nameof(Get), new { id = todoItem.Id }, todoItem);
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]TodoItem todoItem)
        {
               if(id != todoItem.Id)
            {
                return BadRequest("ID mismatch"); 
            }

            var todoItemToUpdate = _todoItems.FirstOrDefault(x => x.Id == id);
            if(todoItemToUpdate == null)
            {
                return NotFound();
            }
            todoItemToUpdate.Title = todoItem.Title;
            todoItemToUpdate.Description = todoItem.Description;
            todoItemToUpdate.IsCompleted = todoItem.IsCompleted;
            return NoContent();
        }

        //DELETE api/values/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var todoItem = _todoItems.FirstOrDefault(x => x.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _todoItems.Remove(todoItem);
            return NoContent();
        }
    }
}
