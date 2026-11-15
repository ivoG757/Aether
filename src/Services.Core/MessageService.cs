using Aether.Data.Models;
using Aether.Data.Repositories.Contracts;
using Aether.Services.Core.Contracts;
using Aether.Services.Core.Data;
using Aether.Services.Core.Data.Message.Response;
using Aether.Services.Core.Data.Message.Request;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aether.Services.Core
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ITextChannelRepository _channelRepository;
        private readonly ILogger<MessageService> _logger;

        public MessageService(
            IMessageRepository messageRepository,
            ITextChannelRepository channelRepository,
            ILogger<MessageService> logger)
        {
            _logger = logger;
            _channelRepository = channelRepository;
            _messageRepository = messageRepository;
        }
        public async Task<GetChannelMessagesResponse> GetMessagesForChannelAsync(
            GetChannelMessagesRequest request, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId) ||
                request.RealmId == Guid.Empty ||
                request.ChannelId == Guid.Empty ||
                request.Skip < 0 ||
                request.Take < 1)
            {
                return new GetChannelMessagesResponse
                {
                    Status = ResponseStatus.InvalidRequest
                };
            }

            try
            {
                IQueryable<TextChannel> channels = _channelRepository
                    .AllAsNoTracking()
                    .Where(channel =>
                    channel.Category.RealmId == request.RealmId &&
                    channel.Id == request.ChannelId &&
                    channel.Category.Realm.Members.Any(member =>
                    member.UserId == userId &&
                    !member.IsDeleted));

                GetChannelMessagesResponse? response = await channels
                    .Select(channel => new GetChannelMessagesResponse
                    {
                        Status = ResponseStatus.Success,
                        Messages = channel.Messages
                        .OrderByDescending(m => m.CreatedAt)
                        .ThenByDescending(m => m.Id)
                        .Skip(request.Skip)
                        .Take(request.Take)
                        .Select(m => new MessageResponse
                        {
                            Id = m.Id,
                            SenderId = m.ApplicationUserId,
                            SenderName = m.User.UserName!,
                            SenderIcon = m.User.ProfilePicture,
                            Text = m.Text,
                            AttachmentUrl = m.AttachmentUrl,
                            CreatedAt = m.CreatedAt
                        })
                        .ToList()
                    }).FirstOrDefaultAsync();

                if (response == null)
                {
                    return new GetChannelMessagesResponse
                    {
                        Status = ResponseStatus.NotFound
                    };
                }

                return response;
            }

            catch (Exception exception)
            {
                _logger.LogError(
                    exception,
                    "Failed to load messages for channel in realm: {RealmId}, channel {ChannelId}, user {UserId}",
                    request.RealmId,
                    request.ChannelId,
                    userId);

                return new GetChannelMessagesResponse
                {
                    Status = ResponseStatus.GeneralError
                };
            }
        }
    }
}
