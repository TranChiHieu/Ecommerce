using Bitis.Ecommerce.Shared.Entity;
using Bitis.Library.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitis.Library.Shared.Service
{
    public interface IService<TEntity> where TEntity : class, IEntity
    {
        IResult Get(Guid id);
        IResult Insert(TEntity value);
        IResult Update(TEntity entity);
        IResult ChangeDataStatus(Guid id, DataStatus status);
        IResult Paging(int pageSize, int pageIndex = 0);
    }
    public class BaseService<TEntity, TDbContext, TIUnitOfWork> : IService<TEntity>
        where TEntity : class, IEntity
        where TDbContext : DbContext, ISqlDbContext
        where TIUnitOfWork : IUnitOfWork<TDbContext>
    {
        protected readonly TIUnitOfWork UnitOfWork;
        public BaseService(TIUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected virtual IResult Do(Func<IResult> action)
        {
            IResult result;
            try
            {
                result = action();
            }
            catch (Exception e)
            {
                result = Result.Exception(e);
            }

            return result;
        }



        public virtual IResult Get(Guid id)
        {
            return Do(() =>
            {
                var result = UnitOfWork.GetRepository<TEntity>().Get(id);

                return Result<TEntity>.Ok(data: result);
            });

        }

        public virtual IResult Insert(TEntity value)
        {
            return Do(() =>
            {
                var result = UnitOfWork.GetRepository<TEntity>().Insert(value);

                return Result<TEntity>.Ok(data: result);
            });
        }

        public virtual IResult Update(TEntity entity)
        {
            return Do(() =>
            {
                var result = UnitOfWork.GetRepository<TEntity>().Update(entity);

                return Result<TEntity>.Ok(data: result);
            });

        }


        public virtual IResult Paging(int pageIndex, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> action)
        {
            return Do(() =>
            {
                IQueryable<TEntity> query = action(UnitOfWork.GetRepository<TEntity>().GetAll());
                var items = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                var totalItems = query.Count();
                var totalPages = (int)Math.Ceiling((double)totalItems / (double)pageSize);

                return PageResult<TEntity>.Ok(
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    totalItems: totalItems,
                    totalPages: totalPages,
                    data: items);

            });
        }

        public virtual IResult Paging<TEntity>(int pageIndex, int pageSize, Func<IOrderedQueryable<TEntity>> action)
        {
            return Do(() =>
            {
                IQueryable<TEntity> query = action();
                var totalItems = query.Count();
                var items = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                var totalPages = (int)Math.Ceiling((double)totalItems / (double)pageSize);

                return PageResult<TEntity>.Ok(
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    totalItems: totalItems,
                    totalPages: totalPages,
                    data: items);

            });
        }

        public virtual IResult Listing(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> action)
        {
            return Do(() =>
            {
                IQueryable<TEntity> query = action(UnitOfWork.GetRepository<TEntity>().GetAll());
                var items = query.ToList();

                return ListResult<TEntity>.Ok(data: items);

            });
        }

        public virtual IResult Listing<TEntity>(Func<IOrderedQueryable<TEntity>> action)
        {
            return Do(() =>
            {
                IQueryable<TEntity> query = action();
                var items = query.ToList();

                return ListResult<TEntity>.Ok(data: items);

            });
        }

        public virtual IResult ChangeDataStatus(Guid id, DataStatus status)
        {
            return Do(action: () =>
            {
                var result = UnitOfWork.GetRepository<TEntity>().UpdateDataStatus(id, status);

                return Result<TEntity>.Ok(data: result);
            });
        }

        public virtual IResult Paging(int pageSize, int pageIndex = 0)
        {
            return Paging(
                pageSize: pageSize,
                pageIndex: pageIndex,
                action: () => UnitOfWork.GetRepository<TEntity>().GetAll()
                    .OrderByDescending(x => x.CreatedDate));
        }
    }

    public abstract class BaseGeneralService
    {
        protected virtual IResult Do(Func<IResult> action)
        {
            IResult result;
            try
            {
                result = action();
            }
            catch (Exception e)
            {
                result = Result.Exception(e);
            }

            return result;
        }

        public virtual IResult Paging<TEntity>(int pageIndex, int pageSize, Func<IOrderedQueryable<TEntity>> action)
        {
            return Do(() =>
            {
                IQueryable<TEntity> query = action();
                var items = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                var totalItems = query.Count();
                var totalPages = (int)Math.Ceiling((double)totalItems / (double)pageSize);

                return PageResult<TEntity>.Ok(
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    totalItems: totalItems,
                    totalPages: totalPages,
                    data: items);

            });
        }

        public virtual IResult Listing<TEntity>(Func<IEnumerable<TEntity>> action)
        {
            return Do(() =>
            {
                IEnumerable<TEntity> query = action();
                var items = query.ToList();

                return ListResult<TEntity>.Ok(data: items);

            });
        }
    }
}
