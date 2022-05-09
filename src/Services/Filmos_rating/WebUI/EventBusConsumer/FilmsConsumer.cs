using AutoMapper;
using EventBus.Messages.Events;
using Filmos_Rating_CleanArchitecture.Application.Film.Commands.UpsertFilms;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


namespace Filmos_Rating_CleanArchitecture.WebUI.EventBusConsumer
{
    public class FilmsConsumer : IConsumer<FilmsDtoEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<FilmsConsumer> _logger;

        public FilmsConsumer(IMediator mediator, ILogger<FilmsConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            var config = new MapperConfiguration(cfg =>
                    cfg.AddProfile<WebUiProfile>()
                );
            _mapper = new Mapper(config);
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<FilmsDtoEvent> context)
        {
            var command = _mapper.Map<UpsertFilmCommand>(context.Message);
            var result = await _mediator.Send(command);

            _logger.LogInformation("FilmsEvent consumed successfully. Created Films Id : {newId}", result);
        }
    }
}
