using Xunit;
using System;
using CoreCommandEntities.Models;

namespace CoreCommandAPI.Tests
{
    
    public class CoreCommandTests: IDisposable
    {
            Command testCoreCommand;
            public CoreCommandTests()
            {
              testCoreCommand = new Command {
                SnippetDescription = "my snippet description",
                Platform = "my platform",
                Snippet = "my snippet"
              };  
            }
            [Fact]
            public void CanChangePlatform() {
            //ARRANGE
            
            //ACT
            testCoreCommand.Platform = "your platform";
            //ASSERT
            Assert.Equal("your platform", testCoreCommand.Platform);
        }

        [Fact]
        public void CanChangeSnippetDescription() {
            //ARRANGE
            
            //ACT
            testCoreCommand.SnippetDescription = "your snippet description";
            //ASSERT
            Assert.Equal("your snippet description", testCoreCommand.SnippetDescription);
        }

        [Fact]
        public void CanChangeSnippet() {
            //ARRANGE
            //ACT
            testCoreCommand.Snippet = "your snippet";
            //ASSERT
            Assert.Equal("your snippet", testCoreCommand.Snippet);
        }

        public void Dispose()
        {
            testCoreCommand = null;
        }
    }
}