using Contexts;
using strange.extensions.context.impl;
using UnityEngine;

namespace View.Root
{
    public class GameArenaRoot : ContextView
    {
        private void Awake()
        {
            context = new GameArenaContext(this);
        }

        private void Start()
        {
            // gestroy all DontDestroyOnLoad objects
            var go = new GameObject("Sacrificial Lamb");
            DontDestroyOnLoad(go);

            foreach (var root in go.scene.GetRootGameObjects())
            {
                Destroy(root);
            }
        }
        
        private void Update()
        {
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }

        protected override void OnDestroy()
        {
            if (Context.firstContext != null)
                base.OnDestroy();
        }
    }
}