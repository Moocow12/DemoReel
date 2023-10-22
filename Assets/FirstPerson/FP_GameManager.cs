using UnityEngine;

namespace FirstPerson
{
    public class FP_GameManager : MonoBehaviour
    {
        public static FP_GameManager instance = null;

        [SerializeField] public FP_Player player;

        void Start()
        {
            if (!instance)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

