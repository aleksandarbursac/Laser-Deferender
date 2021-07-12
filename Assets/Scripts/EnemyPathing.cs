﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (waypointIndex < (waypoints.Count - 1))
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            HandleMovement(targetPosition, waveConfig.MoveSpeed);
        }
        else if (waypointIndex == (waypoints.Count - 1))
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            HandleMovement(targetPosition, waveConfig.MoveSpeedLastWaypoint);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void HandleMovement(Vector3 targetPosition, float moveSpeed)
    {
        var movementThisFrame = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(
            transform.position, targetPosition, movementThisFrame);

        if (transform.position == targetPosition)
        {
            waypointIndex++;
        }
    }
}
