using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Registrar
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseWebRoot("Static")
        .UseStartup<Startup>()
        .Build();

      host.Run();
    }
  }
}