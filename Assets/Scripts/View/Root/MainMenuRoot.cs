using Contexts;
using strange.extensions.context.impl;
using UnityEngine;

namespace View.Root
{
    public class MainMenuRoot : ContextView
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            context = new MainMenuContext(this);
        }

        private void Update()
        {
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }
    }
}