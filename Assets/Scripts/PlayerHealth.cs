using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = FindFirstObjectByType<PauseMenu>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            pauseMenu.RegisterHit();
        }
    }
}