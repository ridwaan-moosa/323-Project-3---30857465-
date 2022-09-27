using _30857465Project3.Models;
using System;
using System.Threading.Tasks;

namespace _30857465Project3.Repository
{
    public interface IZoneRepository : IGenericRepository<Zone>
    {
        Zone GetMostRecentZone();
       
    }
}
