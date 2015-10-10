using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine
{
    public abstract class Scene
    {
        /// <summary>
        /// Called everytime when App.CurrentScene is changed to this scene.
        /// </summary>
        public virtual void OnChange()
        {
        }

        /// <summary>
        /// Called when App.CurrentScene is changed to another scene.
        /// </summary>
        public virtual void OnExit()
        {
        }

        public abstract void Update();

        public virtual void Render()
        {
        }

    }
}
