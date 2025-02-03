using UnityEngine;

public class NavAgent : AIAgent
{
    public Waypoint Waypoint { get; set; }

    private void Start()
    {
        var waypoints = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);

        if(waypoints.Length > 0)
        {
            Waypoint = waypoints[Random.Range(0, waypoints.Length)];
        }
    }

    private void Update()
    {
        if (Waypoint != null)
        {
            movement.MoveTowards(Waypoint.transform.position);
        }

        transform.forward = movement.Direction;
    }
}