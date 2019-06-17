using System;
using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Plugins;

namespace WsdPreprocessingStudio.Core.Data.Collections
{
    public class PluginData
    {
        private Dictionary<string, object> _dictionary;

        public PluginData()
        {
            _dictionary = new Dictionary<string, object>();
        }

        public TData GetData<TPlugin, TData>(string key)
            where TPlugin : IPlugin
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (_dictionary.TryGetValue(FormKey<TPlugin, TData>(key), out var value))
            {
                return (TData) value;
            }

            return default(TData);
        }

        public void SetData<TPlugin, TData>(string key, TData value)
            where TPlugin : IPlugin
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            _dictionary[FormKey<TPlugin, TData>(key)] = value;
        }

        private string FormKey<TPlugin, TData>(string key)
        {
            var pluginType = typeof(TPlugin);
            var dataType = typeof(TData);

            return string.Join("__", pluginType.AssemblyQualifiedName, dataType.AssemblyQualifiedName, key);
        }
    }
}
