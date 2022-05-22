using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Shoping.DAL.EntitiesDTO;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices;
using System;
using System.Threading.Tasks;

namespace Shoping.WEBAPI.EventBusConsumer.FilmsConsumer
{
    public class FilmsUpsertConsumer : IConsumer<FilmsUpsertDtoEvent>
    {
        private readonly ISQLFilmsService _FilmsService;
        private readonly IMapper _mapper;

        public FilmsUpsertConsumer(ISQLFilmsService sqlFilmsService)
        {
            _FilmsService = sqlFilmsService ?? throw new ArgumentNullException(nameof(sqlFilmsService));

            var config = new MapperConfiguration(cfg =>
                    cfg.AddProfile<WebUiProfile>()
                );
            _mapper = new Mapper(config);
        }

        public async Task Consume(ConsumeContext<FilmsUpsertDtoEvent> context)
        {
            var command = _mapper.Map<SQLFilmsForAddDTO>(context.Message);
            if (command.type_price_id == 0)
            {
                command.type_price_id = 1;
            }

            if (context.Message._is_add)
            { //add block
                await _FilmsService.AddFilm(command);
            }
            else //update block
                await _FilmsService.UpdateFilm(context.Message.Id_Film, command);
        }
    }
}
