using Aether.Data.Models;
using Aether.Data.Models.Enums;
using Aether.Data.Repositories.Contracts;

using Aether.Services.Common.Validation;

using Aether.Services.Core.Contracts;
using Aether.Services.Core.Data;
using Aether.Services.Core.Data.Message.Request;
using Aether.Services.Core.Data.Message.Response;

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
                request.Skip < 0 || request.Take < 1)
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
                            SenderId = m.UserId,
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
        public async Task<CreateMessageForChannelResponse> CreateMessageForChannelAsync(
            CreateMessageForChannelRequest request, string userId)
        {
            if(!AttributeValidator.IsValid(request, out var errors))
            {
                return new CreateMessageForChannelResponse
                {
                    Status = ResponseStatus.InvalidRequest
                };
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                return new CreateMessageForChannelResponse
                {
                    Status = ResponseStatus.InvalidRequest
                };
            }

            try
            {
                var currentUser = await _channelRepository
                    .AllAsNoTracking()
                    .Where(c => 
                    c.Id == request.ChannelId &&
                    c.Category.RealmId == request.RealmId)
                    .SelectMany(c => c.Category.Realm.Members)
                    .Where(m =>
                    m.UserId == userId &&
                    !m.IsDeleted &&
                    !m.User.IsDeleted)
                    .Select(m => new
                    {
                        SenderName = m.User.UserName!,
                        SenderIcon = m.User.ProfilePicture
                    })
                    .SingleOrDefaultAsync();

                if (currentUser == null)
                {
                    return new CreateMessageForChannelResponse
                    {
                        Status = ResponseStatus.NotFound
                    };
                }

                var message = new Message
                {
                    Type = MessageType.Text,
                    Text = request.Text?.Trim(),
                    CreatedAt = DateTime.UtcNow,
                    TextChannelId = request.ChannelId,
                    UserId = userId
                };

                await _messageRepository.AddAsync(message);
                await _messageRepository.SaveChangesAsync();

                return new CreateMessageForChannelResponse
                {
                    Status = ResponseStatus.Success,
                    MessageId = message.Id,
                    Text = message.Text,
                    CreatedAt = message.CreatedAt,
                    ProfilePic = currentUser.SenderIcon,
                    Username = currentUser.SenderName
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception,
                    "Failed to create message in realm {RealmId}, channel {ChannelId}, for user {UserId}",
                    request.RealmId,
                    request.ChannelId,
                    userId);

                return new CreateMessageForChannelResponse
                {
                    Status = ResponseStatus.GeneralError
                };
            }
        }
    }
}
