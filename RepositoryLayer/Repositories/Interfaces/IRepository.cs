using System;
using DomainLayer.Common;
using DomainLayer.Entities;

namespace RepositoryLayer.Repositories.Interfaces
{
	public interface IRepository<T> where T: BaseEntity
	{
		void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(Predicate<T> predicate);
        List<T> GetAll(Predicate<T> predicate);
    }
}