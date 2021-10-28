using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{

    [SerializeField]
    Camera m_CurrentCamera;

    [SerializeField]
    float m_Speed = 15f;

    [SerializeField]
    ParticleSystem m_HitParticle;

    [SerializeField]
    ParticleSystem m_ExplosionParticle;

    [SerializeField]
    Text playerOnePoints;

    [SerializeField]
    Text playerTwoPoints;

    [SerializeField]
    AudioSource m_AudioSource;

    [SerializeField]
    AudioClip[] m_HitSounds;

    [SerializeField]
    AudioClip m_HitSoundOnLost;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();

        ResetGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ResetGame();
    }


    void ResetGame()
    {
        var direction = Vector2.up;
        int r = Random.Range(0, 4);

        if (r == 1)
            direction = Vector2.down;
        if (r == 2)
            direction = Vector2.left;
        if (r == 3)
            direction = Vector2.right;


        rb.position = new Vector2(0, 0);
        rb.velocity = direction * m_Speed;

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.collider.CompareTag("Player"))
        {
            m_HitParticle.Play();

            float x = col.gameObject.name == "Player" ? 1 : -1;
            float y = HitFactorY(transform.position, col.transform.position, col.collider.bounds.size.y);
            rb.velocity = new Vector2(x, y).normalized * m_Speed;

            StartCoroutine(Shake(0.1f, 0.1f));

            int r = Random.Range(0, m_HitSounds.Length);
            var audio = m_HitSounds[r];
            m_AudioSource.PlayOneShot(audio);
        }

        else if (col.collider.CompareTag("PlayerTop"))
        {
            m_HitParticle.Play();
          

            float x = HitFactorX(transform.position, col.transform.position, col.collider.bounds.size.x);
            //float x = col.gameObject.name == "PlayerTop" ? 1 : -1;
            float y = col.gameObject.name == "PlayerTop" ? -1 : 1;
            rb.velocity = new Vector2(x, y).normalized * m_Speed;

            StartCoroutine(Shake(0.1f, 0.1f));

            int r = Random.Range(0, m_HitSounds.Length);
            var audio = m_HitSounds[r];
            m_AudioSource.PlayOneShot(audio);
        }
        else if (col.collider.name == "LeftPoint" || col.collider.name == "TopPoint")
        {

            int currentPlayerTwoPoints = int.Parse(playerTwoPoints.text);
            playerTwoPoints.text = (currentPlayerTwoPoints + 1).ToString();
            m_AudioSource.PlayOneShot(m_HitSoundOnLost);
            m_ExplosionParticle.Play();
            ResetGame();

        }
        else if (col.collider.name == "RightPoint" || col.collider.name == "BottomPoint")
        {
            int currentPlayerOnePoints = int.Parse(playerOnePoints.text);
            playerOnePoints.text = (currentPlayerOnePoints + 1).ToString();
            m_AudioSource.PlayOneShot(m_HitSoundOnLost);
            m_ExplosionParticle.Play();
            ResetGame();
        }

    }

    //Physics Methods
    float HitFactorY(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    float HitFactorX(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        if (m_CurrentCamera != null)
        {
            Vector3 orignalPosition = m_CurrentCamera.transform.position;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                m_CurrentCamera.transform.position = new Vector3(x, y, -10f);
                elapsed += Time.deltaTime;
                yield return 0;
            }
            m_CurrentCamera.transform.position = orignalPosition;
        }
    }
}