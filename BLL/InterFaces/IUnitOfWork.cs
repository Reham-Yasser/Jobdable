using BLL.interFaces;
using BLL.Repositries;
using DAL.Entities;

namespace BLL.InterFaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenercRepositry<T> Repositry<T>() where T : BaseEntity;
        Task<int> Complet();


    }
}
