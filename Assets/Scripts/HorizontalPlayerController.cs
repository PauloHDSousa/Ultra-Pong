using UnityEngine;

public class HorizontalPlayerController : MonoBehaviour
{

    [SerializeField]
    KeyCode m_UpKey;

    [SerializeField]
    KeyCode m_DownKey;
    void Update()
    {
        if (Input.GetKey(m_UpKey))
        {
            float y = this.transform.position.y + 0.1f;
            this.transform.position = new Vector2(this.transform.position.x, y);
        }
        else if (Input.GetKey(m_DownKey))
        {
            float y = this.transform.position.y - 0.1f;
            this.transform.position = new Vector2(this.transform.position.x, y);
        }
    }
}
