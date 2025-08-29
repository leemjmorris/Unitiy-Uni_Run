using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [Header("Scroll Settings")]
    public float speed = 10f;

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return; //This part is made to stop the scrolling when the game is over.

        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
