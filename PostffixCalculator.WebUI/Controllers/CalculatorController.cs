using Microsoft.AspNetCore.Mvc;
using PostfixCalculator.Domain;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostffixCalculator.WebUI.Controllers
{
    using global::PostfixCalculator.Application;

    [Route("api/[controller]")]
    public class CalculatorController : Controller
    {
        IRecognizer recognizer;
        PostfixCalculatorCreator creator;
        PostfixCalculator calculator;

        public CalculatorController(IRecognizer recognizer)
        {
            this.recognizer = recognizer;
            creator = new PostfixCalculatorCreator(recognizer, WayToGetOperation.Internal);
            calculator = creator.CreateCalculator();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return calculator.GetAvailableOperations();
        }

        // GET api/<controller>/5
        [HttpGet("{expression}")]
        public string Get(string expression)
        {
            try
            {
                var result = calculator.Calculate(expression);
                return calculator.Calculate(expression).ToString();
            }
            catch (UnrecognizedOperationException e)
            {
                return e.Message;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
