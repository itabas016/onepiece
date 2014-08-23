using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class GlobalConfigRepository : SingletonBase<GlobalConfigRepository>
    {
        private IGlobalConfigService ConfigService
        {
            get
            {
                if (_configService == null && SingletonBase<IocFacotry>.Instance.Container != null)
                    _configService = SingletonBase<IocFacotry>.Instance.Container.GetInstance<IGlobalConfigService>();

                return _configService;
            }
            set
            {
                _configService = value;
            }
        } private IGlobalConfigService _configService;

        private List<Type> Types
        {
            get
            {
                if (_types == null) _types = new List<Type>();

                return _types;
            }
        } private List<Type> _types;

        public List<IGlobalConfigItem> DefaultValues
        {
            get
            {
                if (_defaultValues == null || !_defaultValues.Any())
                {
                    _defaultValues = this.GetDefaultValues();

                    if (_defaultValues == null) _defaultValues = new List<IGlobalConfigItem>();
                }

                return _defaultValues;
            }
        } private List<IGlobalConfigItem> _defaultValues;

        internal Dictionary<string, string> ConfigValues
        {
            get
            {
                if (_configValues == null)
                    _configValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                if (!_configValues.Any())
                {
                    foreach (var item in this.DefaultValues)
                    {
                        _configValues[item.Key] = item.Value;

                        if (this.ConfigService != null)
                        {
                            var dbValue = this.ConfigService.GetConfigItem(item.Key);
                            if (dbValue != null)
                                _configValues[item.Key] = dbValue.Value;
                        }
                    }
                }

                return _configValues;
            }
        } private Dictionary<string, string> _configValues;

        public List<IGlobalConfigItem> GetDefaultValues()
        {
            var globalConfigAttributeType = typeof(GlobalConfigAttribute);
            var defaultValues = new List<IGlobalConfigItem>();

            foreach (var type in Types)
            {
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
                foreach (var field in fields)
                {
                    var attribute = field.GetCustomAttributes(globalConfigAttributeType, false).FirstOrDefault() as GlobalConfigAttribute;
                    if (attribute != null)
                    {
                        var key = (string)field.GetValue(null);

                        var item = this.ConfigService.Create(attribute.Module, key, attribute.Name, attribute.DefaultValue);
                        defaultValues.Add(item);
                    }
                }
            }

            return defaultValues;
        }

        public void Register(Type type)
        {
            if (type != null)
                Types.Add(type);
        }

        public void Register(IEnumerable<Type> types)
        {
            if (types != null)
            {
                foreach (var type in types)
                {
                    Register(type);
                }
            }
        }

        public void Flush()
        {
            this.ConfigValues.Clear();
        }

        public void Update(string key, string value)
        {
            if (this.ConfigValues.ContainsKey(key))
            {
                this.ConfigValues[key] = value;
            }
        }

        public string Get(string key)
        {
            var value = string.Empty;
            if (this.ConfigValues.ContainsKey(key))
            {
                value = this.ConfigValues[key];
            }
            else
            {
                value = key.ConfigValue();
            }

            return value;
        }
    }
}
