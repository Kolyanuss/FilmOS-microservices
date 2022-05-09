using AutoMapper;
using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions.Abstract;
using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces.ISQLServices;
using EFCoreCodeFirstSampleWEBAPI.DAL.Interfaces;
using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using EventBus.Messages.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EFCoreCodeFirstSampleWEBAPI.BLL.Services.SQLServices
{
    public class UsersService : IUsersService
    {
        private IRepositoryWrapper _wrapper;
        private IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;


        public UsersService(IRepositoryWrapper wraper, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _wrapper = wraper;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task<IEnumerable<UserDTO>> Get()
        {
            IEnumerable<User> users = await _wrapper.User.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetById(int id)
        {
            User user = await _wrapper.User.GetByIdAsync(id);
            if (user == null)
            {
                throw new UsersNotFoundException(id);
            }
            else
            {
                return _mapper.Map<UserDTO>(user);
            }
        }

        public async Task<UserDTO> Post(UserForCreationDto userdto)
        {
            if (userdto == null)
            {
                throw new BadRequestException("User is null.");
            }
            if (userdto.UserName == null)
            {
                throw new BadRequestException("Parametr UserName in USers is null.");
            }
            var user = _mapper.Map<User>(userdto);
            await _wrapper.User.Add(user);

            // send checkout event to rabbitmq
            var eventMessage = _mapper.Map<UsersDtoEvent>(userdto);
            await _publishEndpoint.Publish<UsersDtoEvent>(eventMessage);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task Put(int id, UserForCreationDto userdto)
        {
            if (userdto == null)
            {
                throw new BadRequestException("User is null.");
            }
            if (userdto.UserName == null)
            {
                throw new BadRequestException("Parametr UserName in USers is null.");
            }
            User ToUpdate = await _wrapper.User.GetByIdAsync(id);
            if (ToUpdate == null)
            {
                throw new UsersNotFoundException(id);
            }
            _mapper.Map(userdto, ToUpdate);
            _wrapper.User.Update(ToUpdate);
        }

        public async Task Delete(int id)
        {
            User user = await _wrapper.User.GetByIdAsync(id);
            if (user == null)
            {
                throw new UsersNotFoundException(id);
            }
            _wrapper.User.Delete(user);
        }
    }
}
