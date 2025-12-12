using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using KnightAdventure.Utils;
public class EnemyAi : MonoBehaviour
{

    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamimgDistanceMin = 3f;
    [SerializeField] private float roamimgTimerMax = 2f;



    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startingPosition;
    private enum State
    {

        Roaming,
        Chasing,
        Attacking,
        Death
    }
    //private void Start()
    //{
    //    startingPosition = transform.position;
    //}


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
        //_currentState = startingState;

        //_roamingSpeed = _navMeshAgent.speed;
        //_chasingSpeed = _navMeshAgent.speed * chasingSpeedMultiplier;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamimgTimerMax;
                }
                break;

        }

    }

    private void Roaming()

    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        ChangeFacingDirection(startingPosition, roamPosition);
        navMeshAgent.SetDestination(roamPosition);
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamimgDistanceMin, roamingDistanceMax);
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        transform.rotation = sourcePosition.x > targetPosition.x ? Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);
    }
}
