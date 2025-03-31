using UnityEngine;

public class StateAgent : AIAgent
{
    [SerializeField] Perception perception;
    public Animator animator;

    public StateMachine stateMachine = new StateMachine();

    public ValueRef<float> timer = new ValueRef<float>();
    public ValueRef<float> health = new ValueRef<float>();
    public ValueRef<float> destinationDistance = new ValueRef<float>();

    public ValueRef<bool> enemySeen = new ValueRef<bool>();
    public ValueRef<float> enemyDistance = new ValueRef<float>();

    public AIAgent enemy;

    private void Start()
    {
        stateMachine.AddState(nameof(IdleState), new IdleState(this));
        stateMachine.AddState(nameof(PatrolState), new PatrolState(this));
        stateMachine.AddState(nameof(ChaseState), new ChaseState(this));
        stateMachine.AddState(nameof(AttackState), new AttackState(this));
        stateMachine.AddState(nameof(HitState), new HitState(this));
        stateMachine.AddState(nameof(DeathState), new DeathState(this));

        stateMachine.SetState(nameof(IdleState));
    }

    private void Update()
    {
        //update parameters
        timer.value -= Time.deltaTime;

        if (perception != null)
        {
            var gameObjects = perception.GetGameObjects();
            enemySeen.value = gameObjects.Length > 0;
            if (enemySeen)
            {
                gameObjects[0].TryGetComponent(out enemy);
                enemyDistance.value = transform.position.DistanceXZ(enemy.transform.position);
                //movement.Destination = gameObjects[0].transform.position;
            }
        }

        animator.SetFloat("Speed", movement.Velocity.magnitude);

        destinationDistance.value = transform.position.DistanceXZ(movement.Destination);

        stateMachine.Current?.CheckTransitions();
        stateMachine.Update();
    }

    private void OnGUI()
    {
        // draw label of current state above agent
        GUI.backgroundColor = Color.black;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        Rect rect = new Rect(0, 0, 100, 20);
        // get point above agent
        Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
        rect.x = point.x - (rect.width / 2);
        rect.y = Screen.height - point.y - rect.height - 20;
        // draw label with current state name
        GUI.Label(rect, stateMachine.Current.Name);
    }

    public void OnDamage(float damage)
    {
        health.value -= damage;

        if (health > 0) stateMachine.SetState(nameof(HitState));
        else stateMachine.SetState(nameof(DeathState));
    }

    public void Attack()
    {
        // check for collision with surroundings
        var colliders = Physics.OverlapSphere(transform.position, 3);
        //print(colliders.Length);
        foreach (var collider in colliders)
        {
            // enable collision only with enemy
            if (collider.gameObject != enemy.gameObject) continue;

            // check if collider object is a state agent, damage agent
            if (collider.gameObject.TryGetComponent<StateAgent>(out var agent))
            {
                agent.OnDamage(Random.Range(20, 50));
            }
        }
    }
}