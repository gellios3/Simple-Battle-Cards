using strange.extensions.mediation.impl;
using UnityEngine;

namespace View.GameArena
{
    public class ManaView : EventView
    {
        public void InitManaView(int manaCount)
        {
            RefreshMana();


            for (var i = 0; i < manaCount; i++)
            {
                var manaObj = (GameObject) Instantiate(
                    Resources.Load("Prefabs/Mana", typeof(GameObject)), new Vector3(), Quaternion.identity,
                    transform
                );
                manaObj.transform.localPosition = new Vector3(manaObj.transform.localRotation.x,
                    manaObj.transform.localRotation.y, 0);
            }
        }

        private void RefreshMana()
        {
            // Refresh mana
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}