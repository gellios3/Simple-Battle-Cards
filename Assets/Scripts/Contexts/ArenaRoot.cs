using strange.extensions.context.impl;

namespace Contexts
{
    public class ArenaRoot : ContextView
    {
        private void Awake()
        {
            context = new ArenaContext(this);
        }
    }
}