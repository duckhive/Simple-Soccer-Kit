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
    public float distanceToBall;
    public float distanceToGoal;
    public PitchZone currentPitchZone;
    public List<PitchZone> nearbyPitchZones;
    public PitchZone homeZone;
    public PitchZone attackZone;
    public PitchZone dangerZone;
    public PitchZone midZone;
    public PitchZone zoneCurrentlySeeking;

    
    
    private bool _seekingZone;
    private PitchSensor _pitchSensor;
    private RaycastHit _lineOfSight;
    private Animator _anim;
    private float _turboSpeed;


    private void Awake()
    {
        team = GetComponentInParent<Team>();
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();

        _pitchSensor = GetComponentInChildren<PitchSensor>();
        _anim = GetComponent<Animator>();
        
        _turboSpeed = speed * 1.2f;
    }

    private void Start()
    {
        otherTeam = FindObjectsOfType
                <Team>()
            .FirstOrDefault(t => t != team);
    }

    private void Update()
    {
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
                if (receivingPass)
                {
                    rb.velocity = direction * _turboSpeed * Time.deltaTime;
                }
                else
                {
                    rb.velocity = direction * speed * Time.deltaTime;
                }

            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<Player>())
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
            canShoot = true;
        else
            canShoot = false;
    }
    
    private void DetermineIfOpenHandler()
    {
        isOpen = true;
        
        foreach (var player in team.otherTeam.teamPlayers)
        {
            if(Vector3.Distance(player.transform.position, transform.position) < 5)
                if(Vector3.Dot(transform.forward, player.transform.position) < 0.75f)
                    isOpen = false;
        }
    }
}
