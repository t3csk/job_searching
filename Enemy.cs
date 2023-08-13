using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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

    public EnemyHp enemyHp;

    private float timeElapsed;
    private float timeout = 1f;

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
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeout)
        {
            MyRandom random = new MyRandom();

            int rnd = random.Range(1, 6);

            if (rnd == 1)
            {
                animator.SetTrigger("IsAttack");
            }
            if (rnd == 2)
            {
                animator.SetTrigger("IsKick");
            }
            if (rnd == 3)
            {
                animator.SetTrigger("IsAppar");
            }
            if (rnd == 4)
            {
                animator.SetTrigger("IsRun");
            }
            if (rnd == 5)
            {
                animator.SetTrigger("IsBack");
            }

            //Move(rnd);

            timeElapsed = 0.0f;
        }
   }

    public void OnDamageEnemy(int damage)
    {
        CurrentStatus.Hp -= damage;
        animator.SetTrigger("IsDamage");
        //Debug.Log(CurrentStatus.Hp);
        enemyHp.HpDamage(damage);
        if (CurrentStatus.Hp <= 0)
        {
            DieEnemy();
        }
    }

    void DieEnemy()
    {
        CurrentStatus.Hp = 0;
        animator.SetTrigger("IsDie");
    }

    void OnAttackHitTriggerEnter( Collider col )
    {
        if( col.gameObject.tag == "Player" )
        {
            var player = col.gameObject.GetComponent<Player>();
            player?.OnDamagePlayer( CurrentStatus.Power );
            attackHit.SetActive( false );
        }
    }

    void OnKickHitTriggerEnter( Collider col )
    {
        if( col.gameObject.tag == "Player" )
        {
            var player = col.gameObject.GetComponent<Player>();
            player?.OnDamagePlayer( CurrentStatus.Power * 3 );
            kickHit.SetActive( false );
        }
    }

    void OnApperHitTriggerEnter( Collider col )
    {
        if( col.gameObject.tag == "Player" )
        {
            var player = col.gameObject.GetComponent<Player>();
            player?.OnDamagePlayer( CurrentStatus.Power * 5 );
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

    void Move(int a)
    {
        if (a == 1)
        {
            animator.ResetTrigger("IsAttack");
        }

        if (a == 2)
        {
            animator.ResetTrigger("IsKick");
        }

        if  (a == 3)
        {
            animator.ResetTrigger("IsAppar");
        }

        if (a == 4)
        {
            animator.ResetTrigger("IsRun");
        }

        if (a == 5)
        {
            animator.ResetTrigger("IsBack");
        }
    }

    public class MyRandom 
    {
        private Random.State state;

        public MyRandom() : this((int)System.DateTime.Now.Ticks){}

        public MyRandom(int seed)  
        {
            setSeed(seed);
        }

        public void setSeed(int seed) 
        {
            var prev_state = Random.state; Random.InitState(seed);
            state = Random.state; Random.state = prev_state;
        }

        public int Range(int min, int max) 
        {
            var prev_state = Random.state; // 使用前の状態 
            Random.state = state; // 前回の状態にセット 
            var result = Random.Range(min, max); state = Random.state; // 現在の状態を記録 
            Random.state = prev_state; // 使用前の状態に 
            return result;
        }
    }       
}