using Microsoft.AspNetCore.Mvc;
using Module.Dto.Configuration;
using Module.Factory.Interface.Mapper;

namespace Base
{
    public class ServiceController : ControllerBase
    {
        public ConfigDto Config { get; set; }
        public IObjectConverter ObjectConverter { get; set; }
    }
}