using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float movementSpeed = 16;
    public float jumpVelocity = 10;
    public float health = 100;

    private SpriteRenderer m_spriteRenderer;
    private Rigidbody2D m_rigidbody;
    private Animator m_animator;

    private bool m_canBeDamaged = true;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        Vector2 v = m_rigidbody.velocity;
        v.x = hMove;
        m_rigidbody.velocity = v;


        bool ground = false;
        var colliders = Physics2D.OverlapBoxAll(
            (Vector2)transform.position + Vector2.down * 0.1f, 
            new Vector2(0.45f, 1),
            0
        );
        foreach (var c in colliders)
            if (c.gameObject != gameObject)
                ground = true;

        if(Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (ground)
            {
                m_rigidbody.AddForce(Vector2.up * jumpVelocity * m_rigidbody.mass, ForceMode2D.Impulse);
            }
        }

        m_animator.SetBool("Moving", hMove != 0);
        m_animator.SetBool("Grounded", ground);
    }

    public void Damage(float damage)
    {
        if (m_canBeDamaged)
        {
            health -= damage;
            if (health <= 0) Application.LoadLevel(Application.loadedLevel);
            else StartCoroutine(_Damage());
        }
    }
    IEnumerator _Damage()
    {
        m_canBeDamaged = false;
        m_spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        m_spriteRenderer.color = Color.white;
        m_canBeDamaged = true;
    }

    private void OnGUI()
    {
        GUI.Label(
            new Rect(5, 0, 200, 200),
            $"Health: {health}", 
            new GUIStyle { fontSize = 24, fontStyle = FontStyle.Bold }
        );
    }
}
