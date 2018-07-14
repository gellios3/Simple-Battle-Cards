using Contexts;
using strange.extensions.context.impl;

namespace View.Root
{
    public class GameArenaRoot : ContextView
    {
        private void Awake()
        {
            context = new GameArenaContext(this);
        }
    }
}