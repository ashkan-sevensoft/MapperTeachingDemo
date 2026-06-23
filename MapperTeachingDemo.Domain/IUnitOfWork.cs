using MapperTeachingDemo.Domain.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperTeachingDemo.Domain;
public interface IUnitOfWork 
{
    IGenericRepository<T>Repository<T>()
        where T : BaseEntity;


    Task<int> SaveChangesAsync(CancellationToken cancellation= default);    

}
