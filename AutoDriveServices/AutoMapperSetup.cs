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
                cfg.CreateMap<Model.Product, ProductEntity>();
            });
            AutoMap = config.CreateMapper();
        }
    }
}
