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
    [Route("api/command/{commandId}/images")]
    [ApiController]
    public class CommandImageController: ControllerBase
    {
        private readonly IRepositoryWrapper repositoryWrapper;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

         public CommandImageController(IRepositoryWrapper _repositoryWrapper, IMapper _mapper, ILoggerManager _logger)
        {
            repositoryWrapper = _repositoryWrapper;
            mapper = _mapper;
            logger = _logger;
        }

        [HttpGet("{id}",Name="GetCommandImage")]
        public ActionResult<CommandImageReadDTO> GetCommandImage(Guid commandId, Guid id) {
            var commandImageDb = repositoryWrapper.CommandImage.GetByCondition(c => c.Id.Equals(id) && c.CommandId.Equals(commandId),false).FirstOrDefault();
            if(commandImageDb == null) {
                return NotFound();
            }

            var commandImageDto = mapper.Map<CommandImageReadDTO>(commandImageDb);
            return Ok(commandImageDto);
         }

        [HttpGet]
        public ActionResult<IEnumerable<CommandImageReadDTO>> GetAllCommandImage() {
            var commandImageDb = repositoryWrapper.CommandImage.GetAll(false).ToList();
            if(commandImageDb == null) {
                return NotFound();
            }

            var commandImageDto = mapper.Map<IEnumerable<CommandImageReadDTO>>(commandImageDb);
            return Ok(commandImageDto);
         }

        [HttpPost]
        public ActionResult<CommandImageReadDTO> Post(Guid commandId, [FromBody] CommandImageCreateDTO newCommand) {
            if(newCommand == null) {
                return BadRequest();
            }
            
            var commandImageDB = mapper.Map<CommandImage>(newCommand);
            commandImageDB.Id = Guid.NewGuid();
            commandImageDB.CommandId = commandId;
            repositoryWrapper.CommandImage.Create(commandImageDB);
            var result = repositoryWrapper.Save();
            logger.LogInfo(result ? "Success": "Failed");
            var commandImageDto = mapper.Map<CommandImageReadDTO>(commandImageDB);

            return CreatedAtRoute(nameof(GetCommandImage), new { commandId, id = commandImageDto.Id},commandImageDto);
        }
    }
}