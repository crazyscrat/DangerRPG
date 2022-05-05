using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Services
{
  public class AllServices
  {
    private static AllServices _instance;
    public static AllServices Container => _instance ??= new AllServices();

    private List<IService> _services = new List<IService>();

    public void RegisterService(IService service)
    {
      _services.Add(service);
    }
    //
    // public void RegisterService<T>(T service) where T: IService
    // {
    //   _services.Add(service);
    // }

    public T GetService<T>() where T : IService
    {
      // foreach (IService service in _services)
      // {
      //   if (service is T) return (T) service;
      // }
      
      return (T) _services.First(service => service is T);
    }
  }
}