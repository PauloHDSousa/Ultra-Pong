using UnityEngine;

public class VerticalPlayerController : MonoBehaviour
{
    [SerializeField]
    float m_Speed = 350f;

    Rigidbody2D rb;

    [SerializeField]
    KeyCode m_LeftKey;

    [SerializeField]
    KeyCode m_RightKey;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyUp(m_LeftKey) || Input.GetKeyUp(m_RightKey))
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if (Input.GetKeyDown(m_LeftKey) || Input.GetKeyDown(m_RightKey))
        {
            float x = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(x, 0) * m_Speed * Time.deltaTime;
        }
    }

}
