using SubSonic.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic
{
    /// <summary>
    /// Theoretically, the methods below should be same as the IDbContext extension methods.
    /// </summary>
    public interface IDbCacheContext
    {
        IList<T> All<T>(bool willModify = true) where T : EntityBase, ICloneable, new();

        IList<T> All<T>(Func<T, bool> predicate) where T : EntityBase, ICloneable, new();

        IList<T> Where<T>(Predicate<T> predicate) where T : EntityBase, ICloneable, new();

        T Single<T>(int id) where T : EntityBase, ICloneable, new();

        T Single<T>(Func<T, bool> predicate) where T : EntityBase, ICloneable, new();

        object Add<T>(T model, Action<T> addedAction = null) where T : EntityBase, ICloneable, new();

        void Add<T>(IEnumerable<T> models) where T : EntityBase, ICloneable, new();

        int Update<T>(T model, Action<T> updatedAction) where T : EntityBase, ICloneable, new();

        int Update<T>(T model, bool getOrigin = true) where T : EntityBase, ICloneable, new();

        int Update<T>(IEnumerable<T> models) where T : EntityBase, ICloneable, new();

        int Delete<T>(int id, Action deletedAction = null) where T : EntityBase, ICloneable, new();

        int Delete<T>(Predicate<T> predicate) where T : EntityBase, ICloneable, new();

        bool Exists<T>(Predicate<T> expresion) where T : EntityBase, ICloneable, new();

        IList<T> Find<T>(Predicate<T> predicate) where T : EntityBase, ICloneable, new();

        PagedList<T> GetPaged<T>(int pageIndex, int pageSize) where T : EntityBase, ICloneable, new();

        PagedList<T> GetPaged<T>(string sortBy, int pageIndex, int pageSize) where T : EntityBase, ICloneable, new();

        int Truncate<T>() where T : EntityBase, new();

        T Save<T>(Predicate<T> predicate, T model) where T : EntityBase, ICloneable, new();
    }
}
