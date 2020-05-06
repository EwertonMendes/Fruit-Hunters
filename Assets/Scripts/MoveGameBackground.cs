
using UnityEngine;

namespace Assets.Scripts
{
    class MoveGameBackground : MonoBehaviour
    {
        public float bgSpeed;
        private Renderer bgRend;
        void Update()
        {
            if (CountdownController.instance.canStartGame())
            {
                bgRend = GetComponent<MeshRenderer>();
                bgRend.material.mainTextureOffset += new Vector2(0f, bgSpeed * Time.deltaTime);
            }
        }
    }
}
