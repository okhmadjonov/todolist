using AutoMapper;
using Todolist.Models;
using Todolist.Models.Dtos;

namespace Todolist.Automapper
{
    public class AutoMapperProvider : Profile
    {
        public AutoMapperProvider()
        {
            CreateMap<Models.Task, TaskDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore()); 
        }
    }
}
