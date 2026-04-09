using AutoMapper;
using SignalR.DtoLayer.SliderDto;
using SignalR.EntitytLayer.Entities;

namespace SignalRApi.Mapping
{
    public class SliderMapping : Profile
    {
        public SliderMapping()
        {
            CreateMap<Slider,ResultSliderDto>().ReverseMap();
        }
    }
}
