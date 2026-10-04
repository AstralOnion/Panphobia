using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    [SerializeField] private GameObject playerObject;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        // Move the enemy towards the player
        transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, speed * Time.deltaTime);
    }
}
