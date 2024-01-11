using AutoMapper;
using CommandService.SyncDataServices.Grpc;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;
        private readonly IPlatformDataClient _platformDataClient;

        public PlatformsController(ICommandRepo respository,IMapper mapper,IPlatformDataClient platformDataClient)
        {
            _repository = respository;
            _mapper = mapper;
            _platformDataClient = platformDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms from Command Service");

            var platformItems = _repository.GetAllPlatforms();

            if (!platformItems.Any())
            {
                var platforms = _platformDataClient.ReturnAllPlatforms();

                try
                {
                    foreach (var plat in platforms)
                    {
                        if (!_repository.ExternalPlatformExist(plat.ExternalID))
                        {
                            _repository.CreatePlatform(plat);
                        }
                        _repository.SaveChanges();
                        platformItems = _repository.GetAllPlatforms();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> cannot seed the data : {ex.Message}");
                }
            }

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));

        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");
            return Ok("Inbound test of from Platforms Controller");
        }   
    }
}