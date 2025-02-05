using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOD.DAL;
interface IRepository<T> 
	where T : class
{
	List<T> GetList();
	Task<T> Get(int id);
	T Create(T item);
	T Update(T item);
	Task Delete(int id);
	Task SaveAsync();
}

