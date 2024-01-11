using PlatformService.Models;
using System.Collections.Generic;
namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();//This will return true if the changes were saved successfully and false otherwise
        IEnumerable<Platform> GetAllPlatforms();//This will return all the platforms in the database
        Platform GetPlatformById(int id);//This will return the platform with the specified id
        void CreatePlatform(Platform platform);//This will add the specified platform to the database
    }
}