using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public abstract class BasicRegistry : Registry
    {
        public BasicRegistry()
        {
            Register();

            RegisterInDifferentMode();
        }

        private void RegisterInDifferentMode()
        {
            var mode = RegistryModeFactory.GetCurrentMode();
            if (mode == RegistryMode.None) mode = RegistryMode.Release;

            if ((mode & RegistryMode.Debug) == RegistryMode.Debug) { RegisterInDebugMode(); }

            if ((mode & RegistryMode.Release) == RegistryMode.Release) { RegisterInReleaseMode(); }

            if ((mode & RegistryMode.Live) == RegistryMode.Live) { RegisterInLiveMode(); }
        }

        public abstract void Register();

        #region Debug

        public virtual void RegisterInDebugMode()
        {
        }

        #endregion

        #region Release
        public virtual void RegisterInReleaseMode()
        {
        }
        #endregion

        #region Live

        public virtual void RegisterInLiveMode()
        {
        }
        #endregion
    }
}
