using strange.extensions.context.impl;

namespace Contexts
{
    public class MatсhRoomRoot : ContextView
    {
        private void Awake()
        {
            context = new MatchRoomContext(this);
        }
    }
}