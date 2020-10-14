using System;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using System.Collections.Generic;
using CoreCommandEntities.DTO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace CoreCommandAPI.Formatters
{
    public class DelimeterOutputFormatter: TextOutputFormatter
    {
        public DelimeterOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type type)
        {
            if(typeof(CommandReadDTO).IsAssignableFrom(type) || typeof(IEnumerable<CommandReadDTO>).IsAssignableFrom(type)){
                return base.CanWriteType(type);
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, 
        Encoding selectedEncoding) {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            if(context.Object is IEnumerable<CommandReadDTO>) {
                foreach(var command in (IEnumerable<CommandReadDTO>)context.Object) {
                    FormatCSV(buffer,command);
                }
            }
            else FormatCSV(buffer,(CommandReadDTO)context.Object);    
            await response.WriteAsync(buffer.ToString());
        }
        private static void FormatCSV(StringBuilder buffer, CommandReadDTO command) {
            buffer.AppendLine($"{command.Id},\"{command.Platform},\"{command.SnippetDescription},\"{command.Snippet}\"");
        }
    }
}