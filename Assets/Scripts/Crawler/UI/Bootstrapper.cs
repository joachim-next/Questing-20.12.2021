using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Bootstrapper : MonoBehaviour
    {
        public void Start()
        {
            SceneManager.LoadScene("CraftingView");
        }
    }
}