using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [System.Serializable]
    public class Status
    {
        public int Hp = 10;
        public int Power = 1;
    }

    Animator animator;
    Rigidbody rb;
    [SerializeField] ColliderCallReceiver attackHitCall = null;
    [SerializeField] ColliderCallReceiver kickHitCall = null;
    [SerializeField] ColliderCallReceiver apperHitCall = null;
    [SerializeField] Status DefaultStatus = new Status();
    public Status CurrentStatus = new Status();
    [SerializeField] GameObject attackHit = null;
    [SerializeField] GameObject kickHit = null;
    [SerializeField] GameObject apperHit = null;
   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        attackHitCall.TriggerEnterEvent.AddListener( OnAttackHitTriggerEnter );
        kickHitCall.TriggerEnterEvent.AddListener( OnKickHitTriggerEnter );
        apperHitCall.TriggerEnterEvent.AddListener( OnApperHitTriggerEnter );
        CurrentStatus.Hp = DefaultStatus.Hp;
        CurrentStatus.Power = DefaultStatus.Power;
        attackHit.SetActive( false );
        kickHit.SetActive( false );
        apperHit.SetActive( false );
    }

    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 worldAngle = myTransform.eulerAngles;
        float world_angle_y = worldAngle.y;


        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("IsAttack");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("IsKick");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("IsAppar");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("IsHadou");
        }

        Movement(world_angle_y);
    }

    void OnAttackHitTriggerEnter( Collider col )
    {
        if( col.gameObject.tag == "Enemy" )
        {
            var enemy = col.gameObject.GetComponent<EnemyManager>();
            enemy?.OnDamage( CurrentStatus.Power );
            attackHit.SetActive( false );
        }
    }

    void OnKickHitTriggerEnter( Collider col )
    {
        if( col.gameObject.tag == "Enemy" )
        {
            var enemy = col.gameObject.GetComponent<EnemyManager>();
            enemy?.OnDamage( CurrentStatus.Power * 3 );
            kickHit.SetActive( false );
        }
    }

    void OnApperHitTriggerEnter( Collider col )
    {
        if( col.gameObject.tag == "Enemy" )
        {
            var enemy = col.gameObject.GetComponent<EnemyManager>();
            enemy?.OnDamage( CurrentStatus.Power * 5 );
            apperHit.SetActive( false );
        }
    }

    void AnimAttackHit()
    {
        attackHit.SetActive( true );
    }

    void AnimAttackEnd()
    {
        attackHit.SetActive( false );
    }

    void KickHit()
    {
        kickHit.SetActive( true );
    }

    void KickEnd()
    {
        kickHit.SetActive( false );
    }

    void ApperHit()
    {
        apperHit.SetActive( true );
    }

    void ApperEnd()
    {
        apperHit.SetActive( false );
    }

    void Movement(float a)
    {
        float x = Input.GetAxisRaw("Horizontal"); 
        if (a < 80 | 100 < a)
        {
            animator.SetFloat("Run", -x);
        }
        if ( 80 < a & a < 100)
        {
            animator.SetFloat("Run", x);
        }
    }
}
