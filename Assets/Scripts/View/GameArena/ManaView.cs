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
                var manaGameObject = (GameObject) Instantiate(
                    Resources.Load("Prefabs/Mana", typeof(GameObject)), new Vector3(), Quaternion.identity,
                    transform
                );
                manaGameObject.transform.localPosition = new Vector3(manaGameObject.transform.localRotation.x,
                    manaGameObject.transform.localRotation.y, 0);
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