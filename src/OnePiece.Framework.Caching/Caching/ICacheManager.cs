using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Caching
{
    public interface ICacheManager
    {
        void Add(string key, object value);
        void Add(string key, object value, int timeoutSeconds);
        void Add(string key, object value, DateTime? absoluteExpiration);
        void Add(string key, object value, TimeSpan? slidingExpiration);

        // merge to add method
        //void AddNullableData(string key, object value, int timeoutSeconds);
        //object GetNullableData(string key, Type dataType);

        bool Contains(string key);

        object Get(string key, Type dataType);
        T Get<T>(string key);
        void Remove(string key);

        //int Count { get; }
        //long Increment(string countId);

        void Clear(string prefix = "SVC");
        /// <summary>
        /// Please make sure your cache redis is separated from your logic db.
        /// </summary>
        void Flush();
    }
}
