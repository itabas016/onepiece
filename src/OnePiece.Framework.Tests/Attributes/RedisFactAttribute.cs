using OnePiece.Framework.Core;
using OnePiece.Framework.RedisMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace OnePiece.Framework.Tests.Attributes
{
    [XunitTestCaseDiscoverer("OnePiece.Framework.Tests.XunitExtensions.SkippableFactDiscoverer", "OnePiece.Framework.Tests")]
    public class RedisFactAttribute : FactAttribute
    {
        internal class RedisMonitor : SingletonBase<RedisMonitor>
        {
            public DateTime? NextCheckPoint { get; set; }

            public bool IsAvailable
            {
                get
                {
                    if (!NextCheckPoint.HasValue || (NextCheckPoint.Value - DateTime.Now).TotalMinutes > 5 || _isAvaliable == null)
                    {
                        _isAvaliable = CanConnectToServer();
                    }

                    return _isAvaliable.GetValueOrDefault();
                }
            } private bool? _isAvaliable;


            public bool CanConnectToServer()
            {
                var service = new RedisService();
                return service.IsAvailable();
            }
        }
    }
}
