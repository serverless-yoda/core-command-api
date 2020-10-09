using Xunit;
using System;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using CoreCommandAPI.Controllers;
using CoreCommandEntities.Models;
using CoreCommandEntities.DTO;
using CoreCommandContracts;
using CoreCommandAPI.Mapping;



namespace CoreCommandAPI.Tests
{
    public class CoreCommandControllerTests:IDisposable
    {
        Mock<IRepositoryWrapper> mockRepo;
        MappingProfile realProfile;
        MapperConfiguration mapperConfiguration;
        IMapper mapper;

        List<Command> commands;
        public CoreCommandControllerTests()
        {
            mockRepo = new Mock<IRepositoryWrapper>();
            realProfile = new MappingProfile();
            mapperConfiguration =new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(mapperConfiguration);

            commands = new List<Command> {
                new Command() {
                    Id = Guid.Parse("025f2fcd-57d9-45b8-9912-7014f32f3c0d"),       //025f2fcd-57d9-45b8-9912-7014f32f3c0d"),
                    SnippetDescription = "my snippet description",
                    Platform = "dotnet",
                    Snippet = "my snippet"                           
                }
            };
        }

        public void Dispose()
        {
            mockRepo = null;
            realProfile = null;
            mapperConfiguration = null;
            mapper = null;
            
        }

        private List<Command> GetCommands(int num) {
            
            if(num > 0) {
                return commands;
            }
            return new List<Command>();
        }

        [Fact]
        public void GetAllCommands_ReturnZeroItems_WhenDBisEmpty() {
            //ARRANGE
           
            mockRepo.Setup(repo => repo.Command.GetAllCommands()).Returns(GetCommands(0));
            var controller = new CommandController(mockRepo.Object,mapper, null);

            //ACT
            var result = controller.GetAllCommands();
            //ASSERT
            Assert.IsType<OkObjectResult>(result.Result);
        }

        //[Fact]
        public void GetAllCommands_ReturnOneResource_WhenDBhasOneResource() {
            //ARRANGE
           
            mockRepo.Setup(repo => repo.Command.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandController(mockRepo.Object,mapper, null);

            //ACT
            var result = controller.GetAllCommands();
           
            //ASSERT
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDTO>;
            //Assert.Single(commands);
            
        }

        [Fact]
        public void GetAllCommands_Return200OK_WhenDBhasOneResource() {
            //ARRANGE
           
            mockRepo.Setup(repo => repo.Command.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandController(mockRepo.Object,mapper, null);

            //ACT
            var result = controller.GetAllCommands();
           
            //ASSERT
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnCorrectType_WhenDBhasOneResource() {
            //ARRANGE
           
            mockRepo.Setup(repo => repo.Command.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandController(mockRepo.Object,mapper, null);

            //ACT
            var result = controller.GetAllCommands();
           
            //ASSERT
            Assert.IsType<ActionResult<IEnumerable<CommandReadDTO>>>(result);
        }

        
        [Fact]
        public void GetCommandById_Returns404NotFound_WhenNonExistingIdProvided() {
            //ARRANGE
            mockRepo.Setup(repo => repo.Command.GetByCondition(c => c.Id.Equals(Guid.Parse("025f2fcd-57d9-45b8-9912-7014f32f3c0d")))).Returns(() => null);
            var controller = new CommandController(mockRepo.Object,mapper,null);
            //ACT
            var result = controller.GetCommand(Guid.Parse("025f2fcd-57d9-45b8-9912-7014f32f3c0e"));
            //ASSERT
            Assert.IsType<NotFoundResult>(result.Value);
        }
    }
}