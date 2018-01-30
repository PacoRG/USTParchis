using Domain.Model.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Persistence.Repositories
{
        public interface IGenericRepository<T> where T : class
        {
            Task<T> AddAsyn(T t);

            Task<int> CountAsync();

            Task DeleteAsyn(int id);

            void Dispose();

            Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);

            Task<T> FindAsync(Expression<Func<T, bool>> match);

            Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);

            Task<ICollection<T>> GetAllAsyn();

            Task<T> GetAsync(int id);

            Task<int> SaveAsync();

            Task<T> UpdateAsyn(T t, object key);

            Task<SearchResultModel<T>> SearchAsync(SearchModel searchModel);
        }
}
