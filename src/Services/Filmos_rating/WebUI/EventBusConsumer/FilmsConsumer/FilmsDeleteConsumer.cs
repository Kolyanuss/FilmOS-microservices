using AutoMapper;
using EventBus.Messages.Events;
using Filmos_Rating_CleanArchitecture.Application.Film.Commands.DeleteFilms;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.WebUI.EventBusConsumer.FilmsConsumer
{
    public class FilmsDeleteConsumer : IConsumer<FilmsDeleteDtoEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<FilmsDeleteConsumer> _logger;

        public FilmsDeleteConsumer(IMediator mediator, ILogger<FilmsDeleteConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            var config = new MapperConfiguration(cfg =>
                    cfg.AddProfile<WebUiProfile>()
                );
            _mapper = new Mapper(config);
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<FilmsDeleteDtoEvent> context)
        {
            var command = _mapper.Map<DeleteFilmsCommand>(context.Message);
            var result = await _mediator.Send(command);

            //_logger.LogInformation("FilmsDeleteEvent delete film successfully. Deleted Films Id : {Id}", result);
        }
    }
}
