using Domain.Model;
using Domain.Model.Extensions;
using Domain.Model.Infraestructure;
using Domain.Persistence.Repositories;
using Infraestructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DatabaseContext _context;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
        }

        public virtual async Task<ICollection<T>> GetAllAsyn()
        {

            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<SearchResultModel<T>> SearchAsync(SearchModel searchModel)
        {
            if (searchModel == null)
                throw new NullReferenceException(nameof(searchModel));

            IQueryable<T> query = _context.Set<T>();

            query = this.ApplySorting(query, searchModel);
            query = this.ApplyFiltering(query, searchModel);

            if (searchModel.Filters.Count > 0)
                searchModel.PageIndex = 1;

            var searchResult = new SearchResultModel<T>();
            searchResult.TotalRecords = await query.CountAsync();

            query = this.ApplyPagination(query, searchModel);
            searchResult.Records = await query.ToListAsync();

            return searchResult;
        }

        private IQueryable<T> ApplySorting(IQueryable<T> query, SearchModel searchModel)
        {
            if (!string.IsNullOrEmpty(searchModel.SortColumn) && searchModel.IsAscendingSort.HasValue)
                return this.OrderBy(query, searchModel.SortColumn, searchModel.IsAscendingSort.Value);

            return query;
        }

        private IQueryable<T> ApplyPagination(IQueryable<T> query, SearchModel searchModel)
        {
            if (searchModel.PageIndex.HasValue && searchModel.RecordsPerPage.HasValue)
            {
                return query
                    .Skip((searchModel.PageIndex.Value - 1) * searchModel.RecordsPerPage.Value)
                    .Take(searchModel.RecordsPerPage.Value);
            }

            return query;
        }

        private IQueryable<T> ApplyFiltering(IQueryable<T> query, SearchModel searchModel)
        {
            if (searchModel.Filters == null) return query;

            foreach (var filter in searchModel.Filters)
            {
                switch (filter.Type)
                {
                    case FilterType.Contains:
                        return this.FilterBy(query, filter.Column, filter.FilterValue);
                }
            }

            return query;
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> AddAsyn(T t)
        {
            _context.Set<T>().Add(t);
            await _context.SaveChangesAsync();
            return t;

        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().Where(match).ToListAsync();
        }

        public virtual async Task DeleteAsyn(int id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity);
            return;
        }

        public virtual async Task<T> UpdateAsyn(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await _context.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                await _context.SaveChangesAsync();
            }
            return exist;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        //public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        //{

        //    IQueryable<T> queryable = GetAll();
        //    foreach (Expression<Func<T, object>> includeProperty in includeProperties)
        //    {

        //        queryable = queryable.Include<T, object>(includeProperty);
        //    }

        //    return queryable;
        //}

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private IQueryable<T> OrderBy(IQueryable<T> source, string ordering, bool isAscending = true)
        {
            var type = typeof(T);
            var property = type.GetProperty(ordering);

            //E1 = x 
            var E1 = Expression.Parameter(type, "x");

            //E2 = E1.PROPERTY
            var E2 = Expression.MakeMemberAccess(E1, property);

            //E3 = E1 => E2
            var E3 = Expression.Lambda(E2, E1);

            var sortType = isAscending ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExp =
                Expression.Call(typeof(Queryable), sortType,
                                new Type[] { type, property.PropertyType },
                                source.Expression,
                                Expression.Quote(E3));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        private IQueryable<T> FilterBy(IQueryable<T> source, string filterProperty, string value)
        {
            if (filterProperty == null | value == null) return source;

            var type = typeof(T);
            var propertyType = type.GetProperty(filterProperty);

            var property = type.GetProperty(filterProperty);
            var containsMethod = typeof(string).GetMethod("Contains");

            //x
            var accessExpresion = Expression.Parameter(type, "x");

            // x.Name
            var propertyExpression = Expression.MakeMemberAccess(accessExpresion, property);

            MethodCallExpression predicate;

            if (propertyType.PropertyType.IsNumeric())
            {
                var toString = type.GetMethod("ToString");
                var stringExpression = Expression.Call(propertyExpression, toString);

                //predicate = Id.ToString().Contains(CONSTANT)
                predicate = Expression.Call(stringExpression, containsMethod, Expression.Constant(value));
            }
            else
            {
                //x.Name.Contains(value)
                predicate = Expression.Call(propertyExpression, containsMethod, Expression.Constant(value));
            }

            // x => x.Name.Contains(value)
            // x => x.Id.ToString().Contains(value)
            var queraExpresion = Expression.Lambda< Func<T, bool>> (predicate, accessExpresion);


            return source.Where(queraExpresion);
        }
    }
}
