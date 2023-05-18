using BLL.interFaces;
using BLL.InterFaces;
using BLL.Spcefication;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositries
{
    public class GenericRepositry<T> : IGenercRepositry<T> where T : BaseEntity
    {
        private readonly Gdcs_Context _context;

        public GenericRepositry(Gdcs_Context context)
        {
            _context = context;
        }
        public async Task Add(T entity)
        => await _context.Set<T>().AddAsync(entity);

        public T Delete(T entity)
            => _context.Set<T>().Remove(entity).Entity;



        public T Update(T entity)
            => _context.Set<T>().Update(entity).Entity;
        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();


        public async Task<T> GetDataByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<T> GetDataByNameAsync(string Name)
           => await _context.Set<T>().FindAsync(Name);

        public async Task<IReadOnlyList<T>> GetAllDataWithSpecificatonAsync(ISpecification<T> spec)
                => await ApplySpecification(spec).ToListAsync();

        public async Task<T> GetDataByIdWithSpecificatonAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).FirstOrDefaultAsync();

        public async Task<int> Count(ISpecification<T> spec)
            => await ApplySpecification(spec).CountAsync();
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpcificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }

    }
}
