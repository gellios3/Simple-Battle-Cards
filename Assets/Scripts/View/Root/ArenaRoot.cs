using Contexts;
using strange.extensions.context.impl;

namespace View.Root
{
    public class ArenaRoot : ContextView
    {
        private void Awake()
        {
            context = new ArenaContext(this);
        }
    }
}