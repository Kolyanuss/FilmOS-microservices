using AutoMapper;
using EventBus.Messages.Events;
using Filmos_Rating_CleanArchitecture.Application.User.Commands.UpsertUsers;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.WebUI.EventBusConsumer.UsersConsumer
{
    public class UsersUpsertConsumer : IConsumer<UsersUpsertDtoEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersUpsertConsumer(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            var config = new MapperConfiguration(cfg =>
                    cfg.AddProfile<WebUiProfile>()
                );
            _mapper = new Mapper(config);
        }

        public async Task Consume(ConsumeContext<UsersUpsertDtoEvent> context)
        {
            IRequest<string?> command;
            if (context.Message._is_add)
            {
                command = _mapper.Map<InsertUserCommand>(context.Message);
            }
            else
                command = _mapper.Map<UpdateUserCommand>(context.Message);

            await _mediator.Send(command);
        }
    }
}
