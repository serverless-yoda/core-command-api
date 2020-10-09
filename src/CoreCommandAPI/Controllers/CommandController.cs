using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CoreCommandEntities.Models;
using CoreCommandEntities.DTO;
using CoreCommandContracts;
using System;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;

namespace CoreCommandAPI.Controllers
{
    [Route("api/command")]
    [ApiController]
    public class CommandController: ControllerBase {
        
        private readonly IRepositoryWrapper repositoryWrapper;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        public CommandController(IRepositoryWrapper _repositoryWrapper, IMapper _mapper, ILoggerManager _logger)
        {
            repositoryWrapper = _repositoryWrapper;
            mapper = _mapper;
            logger = _logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands() {
            var commandsDb = repositoryWrapper.Command.GetAll();
            var commandDto = mapper.Map<IEnumerable<CommandReadDTO>>(commandsDb);
            return Ok(commandDto);
         }

        [HttpGet("{id}",Name="GetCommand")]
        public ActionResult<CommandReadDTO> GetCommand(Guid id) {
            var commandDb = repositoryWrapper.Command.GetByCondition(c => c.Id.Equals(id)).FirstOrDefault();
            if(commandDb == null) {
                return NotFound();
            }

            var commandDto = mapper.Map<CommandReadDTO>(commandDb);
            return Ok(commandDto);
         }

        [HttpPost]
        public ActionResult<CommandReadDTO> Post([FromBody] CommandCreateDTO newCommand) {
            if(newCommand == null) {
                return BadRequest();
            }
            
            var commandDB = mapper.Map<Command>(newCommand);
            commandDB.Id = Guid.NewGuid();
            repositoryWrapper.Command.Create(commandDB);
            var result = repositoryWrapper.Save();
            logger.LogInfo(result ? "Succee": "Failed");
            var commandDto = mapper.Map<CommandReadDTO>(commandDB);

            return CreatedAtRoute(nameof(GetCommand), new { id = commandDto.Id},commandDto);
        }

        [HttpPut("{id}")]
        public ActionResult<CommandReadDTO> Put(Guid id, [FromBody] CommandUpdateDTO updateCommand) {
            if(updateCommand == null) 
            {
                return BadRequest();
            }
            if(!ModelState.IsValid) {
                return UnprocessableEntity();
            }
            var commandDb = repositoryWrapper.Command.GetByCondition(c => c.Id.Equals(id)).FirstOrDefault();
            if(commandDb == null) {
                return NotFound();
            }
            mapper.Map(updateCommand,commandDb);
            repositoryWrapper.Command.Update(commandDb);
            var result = repositoryWrapper.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult<CommandReadDTO> Patch(Guid id, JsonPatchDocument<CommandUpdateDTO> patchDoc) {
            if(patchDoc == null) {
                return BadRequest();
            }
            var commandDb = repositoryWrapper.Command.GetByCondition(c => c.Id.Equals(id)).FirstOrDefault();
            if(commandDb == null) {
                return NotFound();
            }

            var commandDto = mapper.Map<CommandUpdateDTO>(commandDb);
            patchDoc.ApplyTo(commandDto,ModelState);

            if(!TryValidateModel(commandDto)) {
                return ValidationProblem(ModelState);
            }

            mapper.Map(commandDto,commandDb);
            repositoryWrapper.Command.Update(commandDb);
            var result = repositoryWrapper.Save();

            return NoContent();
        }
    
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id) {
            var commandDb = repositoryWrapper.Command.GetByCondition(c => c.Id.Equals(id)).FirstOrDefault();
            if(commandDb == null) {
                return NotFound();
            }

            repositoryWrapper.Command.Delete(commandDb);
            var result = repositoryWrapper.Save();
            return NoContent();
        }
    }
}