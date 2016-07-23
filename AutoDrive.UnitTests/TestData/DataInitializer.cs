using AutoDriveDataModel.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.UnitTests.TestData
{
	public class DataInitializer
	{
		public static List<Product> GetAllProducts()
		{
			var products = new List<Product>
				{
				new Product() {ProductName = "Laptop"},
				new Product() {ProductName = "Mobile"},
				new Product() {ProductName = "HardDrive"},
				new Product() {ProductName = "IPhone"},
				new Product() {ProductName = "IPad"}
				};
			return products;
		}

        public static List<Area> GetAllAreas()
        {
            var products = new List<Area>
                {
                    new Area() {Id= ObjectId.Parse("5791e186c0a1342f54f4372b"), AreaCode="EP1", Name="Epping" },
                    new Area() {Id= ObjectId.Parse("5791e186c0a1342f54f43721"), AreaCode="AR1", Name="StLeonards" },
                    new Area() { Id= ObjectId.Parse("5791e186c0a1342f54f43711"), AreaCode="AR1", Name="Chatswood" },
                };
            return products;
        }
    }
}
