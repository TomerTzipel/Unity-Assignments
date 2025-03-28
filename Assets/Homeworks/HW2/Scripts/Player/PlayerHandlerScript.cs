using UnityEngine;

namespace HW2
{
    public class PlayerHandlerScript : MonoBehaviour
    {
        [Header("Required Player Controller")]
        [SerializeField] protected PlayerController playerController;
        protected PlayerSettings PlayerSettings { get { return playerController.PlayerSettings; } }
    }
}



