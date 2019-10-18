using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AICrowdController : MonoBehaviour
{
    public GameObject[] goals;
    private NavMeshAgent _agent;
    private Animator _anim;
    float _speedMulti;
    public float detectionRadius = 5f;
    public float fleeRadius = 10f;

    void ResetAgent()
    {
        _speedMulti = Random.Range(0.1f, 1.5f);
        _agent.speed = 2 * _speedMulti;
        _agent.angularSpeed = 120;
        _anim.SetFloat("wSpeed", _speedMulti);
        _anim.SetTrigger("isWalking");
        _agent.ResetPath();
    }
    void Start()
    {
        goals = GameObject.FindGameObjectsWithTag("goal");
        _agent = this.GetComponent<NavMeshAgent>();
        Vector3 pos = goals[Random.Range(0, goals.Length)].transform.position;
        _agent.SetDestination(pos);
        _anim = this.GetComponent<Animator>();
        _anim.SetFloat("wOffset", Random.Range(0, 1));
        _anim.SetFloat("wSpeed", _speedMulti);
        _anim.SetTrigger("isWalking");
        ResetAgent();
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance < 1)
        {
            _agent.SetDestination(goals[Random.Range(0, goals.Length)].transform.position);
        }
    }

    public void DetectNewObstacle(Vector3 position)
    {
        if (Vector3.Distance(position, this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - position).normalized;
            Vector3 newGoal = this.transform.position + fleeDirection * fleeRadius;
            NavMeshPath path = new NavMeshPath();
            _agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                _agent.SetDestination(path.corners[path.corners.Length - 1]);
                _anim.SetTrigger("isWalking");
                _agent.speed = 10;
                _agent.angularSpeed = 300;
            }
        }
    } 
}
