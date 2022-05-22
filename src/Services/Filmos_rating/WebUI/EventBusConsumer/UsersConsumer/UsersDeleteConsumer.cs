using AutoMapper;
using EventBus.Messages.Events;
using Filmos_Rating_CleanArchitecture.Application.User.Commands.DeleteUsers;
using MassTransit;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.WebUI.EventBusConsumer.UsersConsumer
{
    public class UsersDeleteConsumer : IConsumer<UsersDeleteDtoEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersDeleteConsumer(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            var config = new MapperConfiguration(cfg =>
                    cfg.AddProfile<WebUiProfile>()
                );
            _mapper = new Mapper(config);
        }

        public async Task Consume(ConsumeContext<UsersDeleteDtoEvent> context)
        {
            var command = _mapper.Map<DeleteUserCommand>(context.Message);
            var result = await _mediator.Send(command);
        }
    }
}
