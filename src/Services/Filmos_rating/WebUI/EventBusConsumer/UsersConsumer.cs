using AutoMapper;
using EventBus.Messages.Events;
using Filmos_Rating_CleanArchitecture.Application.User.Commands.UpsertUsers;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.WebUI.EventBusConsumer
{
    public class UsersConsumer : IConsumer<UsersDtoEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersConsumer> _logger;

        public UsersConsumer(IMediator mediator, ILogger<UsersConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            var config = new MapperConfiguration(cfg =>
                    cfg.AddProfile<WebUiProfile>()
                );
            _mapper = new Mapper(config);
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<UsersDtoEvent> context)
        {
            var command = _mapper.Map<UpsertUsersCommand>(context.Message);
            var result = await _mediator.Send(command);

            _logger.LogInformation("UsersEvent consumed successfully. Created User Id : {newId}", result);
        }
    }
}
