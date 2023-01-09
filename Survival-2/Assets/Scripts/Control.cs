using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

    private Animator animator;
    public GameObject Melt;
    public GameObject die;
    public GameObject snowParticle;
    public GameObject particleCarrot;

    public AudioSource first;
    public AudioSource hit;
    public AudioSource impact;
    public AudioSource carrot;

    private Rigidbody2D rigidbody2d;

    public HealthBar healthBar;

    public float jumpHeight = 12;
    float linearVelocity = 6;
    int jumping = 10;

    public bool isOnGround;
    bool canMove;
    public Transform floorMarker;
    private float radioMarkerFloor;
    public LayerMask layerFloor;

    bool lookRight;

    public event triggerDelegate eliminate;
    public event triggerDelegate finalLevel;
    public event triggerDelegate sumCounter;
    public delegate void triggerDelegate();

    //Functions that control collision or trigger behaviors with different Tags
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Meta"))
        {
            finalLevel();
        }

        else if (other.gameObject.tag.Equals("mushroom"))
        {
            healthBar.Recovery(20);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Carrot"))
        {
            carrot.Play();
            healthBar.Recovery(20);
            GameObject ps = (GameObject)Instantiate(particleCarrot, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(ps, 0.2f);          
            sumCounter();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
    
    if (col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("EnemyMine"))
        {
        healthBar.TakeDamage(15);
        if (!isOnGround)
        {
            Vector2 v = col.contacts[0].point - (Vector2)transform.position;
             if (Mathf.Abs(Vector2.Angle(v, Vector3.up)) > 45.0)
             {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumping);

                if (col.gameObject.tag.Equals("Enemy"))
                {
                    GameObject ps = (GameObject)Instantiate(snowParticle, transform.position, transform.rotation);
                    hit.Play();
                    healthBar.TakeDamage(15);
                    Destroy(col.gameObject);
                    Destroy(ps, 0.2f);
                    Instantiate(Melt, col.transform.position, col.transform.rotation);
                    }
                else
                    {
                    GameObject ps = (GameObject)Instantiate(snowParticle, transform.position, transform.rotation);
                    hit.Play();
                    healthBar.TakeDamage(15);
                    Destroy(col.gameObject);
                    Destroy(ps, 0.2f);
                    Instantiate(die, col.transform.position, col.transform.rotation);
                    }
                }
            }
        }
        else if (col.gameObject.tag.Equals("Snowball") ||col.gameObject.tag.Equals("Rock") || col.gameObject.tag.Equals("Special"))
        {
            impact.Play();
            healthBar.TakeDamage(15);
        }
        else if (col.gameObject.tag.Equals("Defeat"))
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
        else if (col.gameObject.tag.Equals("DeadZone"))
        {
            healthBar.TakeDamage(100);
        }
    }


    //functions Start, Update and FixedUpdate
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        radioMarkerFloor = 0;
        animator = GetComponent<Animator>();
        lookRight = true;
        canMove = true;
       
    }

    void Update()
    {        
        Invoke("Movement", 0);
        if (Input.GetKey(KeyCode.LeftArrow) && canMove)
        {
            if (lookRight) {
                lookRight = false;
                Vector3 Scale = transform.localScale;
                Scale.x *= -1;
                transform.localScale = Scale;
            }
            
            if (rigidbody2d.velocity.x * -1 < linearVelocity / 2)
            {
                rigidbody2d.velocity += new Vector2(transform.right.x * linearVelocity*1.5f * -1, transform.right.y * linearVelocity)*1.5f * Time.deltaTime;
            }
            else if (rigidbody2d.velocity.x * -1 > linearVelocity/2)
            {
                rigidbody2d.velocity = new Vector2(-linearVelocity, rigidbody2d.velocity.y);
            }

        }
        if (Input.GetKey(KeyCode.RightArrow) && canMove)
        {
            
            if (rigidbody2d.velocity.x < linearVelocity / 2)
            {
                rigidbody2d.velocity += new Vector2(transform.right.x * linearVelocity*1.5f, transform.right.y * linearVelocity)*1.5f * Time.deltaTime;
            }
            else if (rigidbody2d.velocity.x > linearVelocity/2)
            {
                rigidbody2d.velocity = new Vector2(linearVelocity, rigidbody2d.velocity.y);
            }
            
            if (!lookRight) {
                lookRight = true;
                Vector3 Scale = transform.localScale;
                Scale.x *= -1;
                transform.localScale = Scale;
            }          
        }
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpHeight);
            Invoke("Jump", 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) && isOnGround)
        {
            canMove = false;
            Invoke("LookDown", 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) && isOnGround)
        {
            canMove = false;
            Invoke("LookUp", 0);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            canMove = true;
        }
        animator.SetBool("under", false);
        animator.SetBool("above", false);
    }

    void FixedUpdate()
    {
        isOnGround = Physics2D.OverlapCircle(floorMarker.position, radioMarkerFloor, layerFloor);
        Invoke("Jump", 0);
    }


    private void Walking()
    {
        first.Play();
    }

    public void setVelocity()
    {
        rigidbody2d.velocity = new Vector2(0,0);
    }


    //Invokes for animations; set floats or boleanos
    private void Movement()
    {
        if (rigidbody2d.velocity.x.Equals(0))
        {
            animator.SetBool("velocity", false);
        }
        else
        {
            animator.SetBool("velocity", true);
        }
    }

    private void Jump()
    {
        animator.SetBool("idle", isOnGround);
    }

    private void LookDown()
    {
        animator.SetBool("under", true);
    }

    private void LookUp()
    {
        animator.SetBool("above", true);
    }

    


}
