using AutoDriveEntities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = AutoDriveDataModel.Models;
namespace AutoDriveServices
{
    public class AutoMapperSetup
    {
        public static IMapper AutoMap { get; set; }
        public static void Init()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Model.Product, ProductEntity>();
                cfg.CreateMap<Model.Area, AreaEntity>();
                cfg.CreateMap<Model.Suburb, SuburbEntity>();
                cfg.CreateMap<Model.Instructor, InstructorEntity>();
                cfg.CreateMap<Model.Student, StudentEntity>();
                cfg.CreateMap<Model.StudentInstructor, StudentInstructor>();
                cfg.CreateMap<Model.Booking, BookingEntity>()
                .ForMember(dest => dest.Title,
               opts => opts.MapFrom(src => src.Student.StudentName))
               .ForMember(dest => dest.StartsAt,
               opts => opts.MapFrom(src => src.StartDateTime))
               .ForMember(dest => dest.EndsAt,
               opts => opts.MapFrom(src => src.EndDateTime));
                cfg.CreateMap<Model.BookingInstructor, BookingInstructor>();
                cfg.CreateMap<Model.BookingStudent, BookingStudent>();
                cfg.CreateMap<Model.ApplicationUser, UserEntity>();
            });
            AutoMap = config.CreateMapper();
        }
    }
}
