using UnityEngine;

public class VerticalPlayerController : MonoBehaviour
{
    [SerializeField]
    KeyCode m_LeftKey;

    [SerializeField]
    KeyCode m_RightKey;

    void Update()
    {
        if (Input.GetKey(m_LeftKey))
        {
            float x = this.transform.position.x - 0.1f;
            this.transform.position = new Vector2(x, this.transform.position.y);
        }
        else if (Input.GetKey(m_RightKey))
        {
            float x = this.transform.position.x + 0.1f;
            this.transform.position = new Vector2(x, this.transform.position.y);
        }
    }

}
