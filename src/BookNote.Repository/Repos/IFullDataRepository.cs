using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookNote.Repository.Repos
{
    public interface IFullDataRepository<T>
    {
		IEnumerable<T> GetAll();

		T GetById(int id);

		void Add(T entity);

		void Update(int id, T entity);

		void Delete(int id);
    }
}
