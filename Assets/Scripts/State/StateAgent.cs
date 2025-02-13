using UnityEngine;

public class StateAgent : AIAgent
{
    [SerializeField] Perception perception;

    public StateMachine stateMachine = new StateMachine();

    private void Start()
    {
        stateMachine.AddState(nameof(IdleState), new IdleState(this));
        stateMachine.AddState(nameof(PatrolState), new PatrolState(this));

        stateMachine.SetState(nameof(IdleState));
    }

    private void Update()
    {
        stateMachine.Update();

        //if(perception != null)
        //{
        //    var gameObjects = perception.GetGameObjects();
        //    if(gameObjects.Length > 0 )
        //    {
        //        movement.Destination = gameObjects[0].transform.position;
        //    }
        //}
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
}