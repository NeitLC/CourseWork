using AutoMapper;

namespace CourseWork.Business.Utils
{
    public class MapperUtil
    {
        public static TDestination Map<TSourse, TDestination>(
            TSourse sourse,
            TDestination destination = null,
            MapperConfiguration mapperConfiguration = null)
            where TSourse : class
            where TDestination : class
        {
            if (mapperConfiguration == null)
            {
                mapperConfiguration = new MapperConfiguration(cfg 
                    => cfg.CreateMap<TSourse, TDestination>());
            }

            var mapper = new Mapper(mapperConfiguration);

            if (destination == null)
            {
                return mapper.Map<TSourse, TDestination>(sourse);
            }

            mapper.Map<TSourse, TDestination>(sourse, destination);

            return destination;
        }
    }
}