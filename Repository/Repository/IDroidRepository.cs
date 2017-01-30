using Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IDroidRepository
    {
        IEnumerable<Droid> GetAll();
        bool Create(Droid newDroid);
        Droid Get(int id);
        Droid Update(Droid droid);
        Droid Delete(int id);
    }
}
