using Contexts;
using strange.extensions.context.impl;

namespace View.Root
{
    public class NetworkLobbyRoot : ContextView
    {
        private void Awake()
        {
            context = new NetworkLobbyContext(this);
        }
    }
}