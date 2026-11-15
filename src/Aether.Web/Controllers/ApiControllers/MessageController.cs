using Aether.Services.Core.Contracts;
using Aether.Services.Core.Data;
using Aether.Services.Core.Data.Message.Response;
using Aether.Services.Core.Data.Message.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aether.Web.Controllers.ApiControllers
{
    [ApiController]
    [Authorize]
    [Route("api/realms/{realmId:guid}/channels/{channelId:guid}/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<MessageResponse>>> GetMessagesForChannelAsync(
            Guid realmId, Guid channelId, [FromQuery] int skip = 0)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var request = new GetChannelMessagesRequest
            {
                RealmId = realmId,
                ChannelId = channelId,
                Take = 30,
                Skip = skip
            };

            GetChannelMessagesResponse response =
                await _messageService.GetMessagesForChannelAsync(request, userId);

            return response.Status switch
            {
                ResponseStatus.Success => Ok(response.Messages),
                ResponseStatus.InvalidRequest => BadRequest(),
                ResponseStatus.NotFound => NotFound(),
                ResponseStatus.GeneralError => 
                StatusCode(StatusCodes.Status500InternalServerError),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };

        }
    }
}
