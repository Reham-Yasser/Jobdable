using BLL.interFaces;
using BLL.InterFaces;
using BLL.Repositries;
using DAL;
using DAL.Entities;
using System.Collections;

namespace BLL.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Gdcs_Context _context;
        private Hashtable _repositries;

        public UnitOfWork(Gdcs_Context context)
        {
            _context = context;
        }
        public async Task<int> Complet()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();


        public IGenercRepositry<T> Repositry<T>() where T : BaseEntity
        {

            if (_repositries == null) _repositries = new Hashtable();
            var type = typeof(T);
            if (!_repositries.ContainsKey(type))
            {
                var repositry = new GenericRepositry<T>(_context);
                _repositries.Add(type, repositry);
            }
            return (IGenercRepositry<T>)_repositries[type];
        }

    }
}
