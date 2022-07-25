using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject userIndicator;

    [HideInInspector] public Team team;
    [HideInInspector] public Team otherTeam;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Quaternion startRotation;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Shooting shooting;
    [HideInInspector] public Vector3 direction;

    public float speed = 300f;
    public float passPower;
    public Transform ballPosition;
    public bool user;
    public bool hasPossession;
    public bool receivingPass;
    public bool canShoot;
    public bool isOpen;
    public bool isOpenForPass;
    public bool possessionCooldown;
    public bool seekingBall;
    public float distanceToBall;
    public float distanceToGoal;
    public PitchZone homeZone;
    public PitchZone attackZone;
    public PitchZone dangerZone;
    public PitchZone midZone;
    public PitchZone zoneCurrentlySeeking;
    public Player playerCovering;

    public List<Player> allOtherTeammates;
    
    private bool _seekingZone;
    private RaycastHit _lineOfSight;
    private Animator _anim;
    private float _turboSpeed;


    private void Awake()
    {
        team = GetComponentInParent<Team>();
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _turboSpeed = speed * 1.2f;
    }

    private void Start()
    {
        otherTeam = FindObjectsOfType
                <Team>()
            .FirstOrDefault(t => t != team);

        allOtherTeammates = team.teamPlayers.Where(t => t != this).ToList();
    }

    private void Update()
    {
        if(hasPossession)
            DetermineTeammatesCanPassTo();
        
        GetDistanceToBallHandler();
        GetDistanceToGoalHandler();
        DetermineIfOpenHandler();
        CanShootHandler();
        
        if (GameManager.Instance.gameActive)
        {
            if (user)
                _anim.enabled = false;
            else
                _anim.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameActive)
        {
            if (!user)
            {
                if (receivingPass || seekingBall || hasPossession)
                {
                    rb.velocity = Vector3.Lerp(transform.forward, direction, 0.25f) * _turboSpeed * Time.deltaTime;
                }
                else
                {
                    rb.velocity = Vector3.Lerp(transform.forward, direction, 0.25f) * speed * Time.deltaTime;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.GetComponent<Player>())
            rb.AddForce(other.contacts[0].normal * 20, ForceMode.Impulse);
    }

    public void UserBrain()
    {
        user = true;
        team.currentUser.Insert(0, this);

        if(team.currentUser.Count > 2)
            team.currentUser.RemoveAt(2);
        
        userIndicator.SetActive(true);
    }

    public void AiBrain()
    {
        user = false;
        userIndicator.SetActive(false);
    }

    public void ResetPositionAndRotation()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void GetDistanceToBallHandler()
    {
        distanceToBall = Vector3.Distance(BallManager.Instance.transform.position,transform.position);
    }

    private void GetDistanceToGoalHandler()
    {
        distanceToGoal = Vector3.Distance(transform.position, team.shotTarget.transform.position);
    }

    private void CanShootHandler()
    {
        if (distanceToGoal < 45)
        {
            if (team.teamEnum == TeamEnum.Away)
            {
                if (Physics.Linecast(BallManager.Instance.transform.position, team.shotTarget.transform.position,
                        LayerMask.GetMask("Home Player")))
                {
                    canShoot = false;
                }
                else
                {
                    canShoot = true;
                }
            }
            if (team.teamEnum == TeamEnum.Home)
            {
                if (Physics.Linecast(BallManager.Instance.transform.position, team.shotTarget.transform.position,
                        LayerMask.GetMask("Away Player")))
                {
                    canShoot = false;
                }
                else
                {
                    canShoot = true;
                }
            }    
        }
        else
        {
            canShoot = false;
        }
    }
    
    private void DetermineIfOpenHandler()
    {
        isOpen = true;
        
        foreach (var player in team.otherTeam.teamPlayers)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 5)
            {
                isOpen = false;
                playerCovering = player;
            }
        }
    }

    private void DetermineTeammatesCanPassTo()
    {
        foreach (var player in allOtherTeammates)
        {
            player.isOpenForPass = true;

            if(team.teamEnum == TeamEnum.Away)
                if(Physics.Linecast(BallManager.Instance.transform.position, player.transform.position, LayerMask.GetMask("Home Player")))
                    player.isOpenForPass = false;
            
            if(team.teamEnum == TeamEnum.Home)
                if(Physics.Linecast(BallManager.Instance.transform.position, player.transform.position, LayerMask.GetMask("Away Player")))
                    player.isOpenForPass = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (hasPossession)
        {
            foreach (var player in allOtherTeammates)
            {
                if(player.isOpenForPass)
                    Gizmos.color = Color.green;
                else
                    Gizmos.color = Color.red;
                
                Gizmos.DrawLine(BallManager.Instance.transform.position, player.transform.position);
            }
        }
    }
}
