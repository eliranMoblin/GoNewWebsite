using System;
using System.Configuration;
using Web.Concrete;

namespace ResourceBuilder 
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new Web.Utility.ResourceBuilder();
            
            string filePath = builder.Create(new DbResourceProvider(),
                summaryCulture: "en-us");

            Console.WriteLine("Created file {0}", filePath);

        }
    }
}
