using AutoDriveDataModel.Models;
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
	}
}
