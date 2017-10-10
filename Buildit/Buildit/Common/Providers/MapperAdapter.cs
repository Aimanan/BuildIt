
using AutoMapper;

namespace Buildit.Common.Providers
{
    public class MapperAdapter : IMapperAdapter
    {
        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}

//???