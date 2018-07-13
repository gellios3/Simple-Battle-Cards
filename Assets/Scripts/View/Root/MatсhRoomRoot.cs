using Contexts;
using strange.extensions.context.impl;

namespace View.Root
{
    public class MatсhRoomRoot : ContextView
    {
        private void Awake()
        {
            context = new MatchRoomContext(this);
        }
    }
}