using UnityEngine;

namespace Code.Menu
{
    public class UIBehaviour : MonoBehaviour
    {
        private static readonly int Transparency = Animator.StringToHash("Transparency");

        public void IncreaseSize()
        {
            transform.LeanScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).setEaseInOutSine();
        }

        public void IncreaseTransparency()
        {
            GetComponent<Animator>().SetTrigger(Transparency);
        }
    }
}
