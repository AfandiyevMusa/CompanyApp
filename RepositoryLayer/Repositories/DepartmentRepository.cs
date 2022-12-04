using System;
using DomainLayer.Entities;
using RepositoryLayer.Datas;
using RepositoryLayer.Repositories.Interfaces;

namespace RepositoryLayer.Repositories
{
    public class DepartmentRepository : IRepository<Department>
    {
        public void Add(Department entity)
        {
            if (entity == null) throw new ArgumentNullException();
            AppDbContext<Department>.values.Add(entity);
        }

        public void Delete(Department entity)
        {
            if (entity == null) throw new ArgumentNullException();
            AppDbContext<Department>.values.Remove(entity);
        }

        public Department Get(Predicate<Department> predicate)
        {
            return AppDbContext<Department>.values.Find(predicate);
        }

        public List<Department> GetAll(Predicate<Department> predicate)
        {
            return predicate == null ? AppDbContext<Department>.values : AppDbContext<Department>.values.FindAll(predicate);
        }

        public void Update(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}

