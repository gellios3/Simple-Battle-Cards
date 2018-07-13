using Contexts;
using strange.extensions.context.impl;

namespace View.Root
{
    public class MainMenuRoot : ContextView
    {
        private void Awake()
        {
            context = new MainMenuContext(this);
        }
    }
}