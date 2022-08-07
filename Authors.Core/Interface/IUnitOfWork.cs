using Authors.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authors.Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        //Here we could add all entities
        Task<IEnumerable<Author>> GetAllAsync();

        //Task<IEnumerable<Activities>> GetAllAsync();

        //Task<IEnumerable<Books>> GetAllAsync();

        //Task<IEnumerable<CoverPhonos>> GetAllAsync();

        //Task<IEnumerable<Users>> GetAllAsync();

    }
}
