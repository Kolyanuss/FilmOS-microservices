using EventBus.Messages.Events;
using MassTransit;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices;
using System;
using System.Threading.Tasks;

namespace Shoping.WEBAPI.EventBusConsumer.FilmsConsumer
{
    public class FilmsDeleteConsumer : IConsumer<FilmsDeleteDtoEvent>
    {
        private readonly ISQLFilmsService _FilmsService;

        public FilmsDeleteConsumer(ISQLFilmsService sqlFilmsService)
        {
            _FilmsService = sqlFilmsService ?? throw new ArgumentNullException(nameof(sqlFilmsService));
        }

        public async Task Consume(ConsumeContext<FilmsDeleteDtoEvent> context)
        {
            await _FilmsService.DeleteFilm(context.Message.Id_Film);
        }
    }
}
