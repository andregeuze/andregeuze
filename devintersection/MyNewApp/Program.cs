using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace MyNewApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello DevIntersection!");

            var host = new WebHostBuilder()
                .UseKestrel()
                .Configure(app => app.Run(context => context.Response.WriteAsync("Hello World!")))
                .Build();

            host.Run();
        }
    }
}
