using UnityEngine;

public class HorizontalPlayerController : MonoBehaviour
{
    [SerializeField]
    float m_Speed = 350f;

    Rigidbody2D rb;

    [SerializeField]
    KeyCode m_UpKey;

    [SerializeField]
    KeyCode m_DownKey;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyUp(m_UpKey) || Input.GetKeyUp(m_DownKey))
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if (Input.GetKeyDown(m_UpKey) || Input.GetKeyDown(m_DownKey))
        {
            float y = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(0, y) * m_Speed * Time.deltaTime;
        }
    }
}
