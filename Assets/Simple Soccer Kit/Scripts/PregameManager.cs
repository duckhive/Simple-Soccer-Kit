using System.Collections;
using UnityEngine;

public class PregameManager : MonoBehaviour
{
    [SerializeField] private RectTransform gamepadRectTransform;
    
    public static PregameManager Instance;

    private bool _cooldown;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
    }

    private void Update()
    {
        TeamSelectorHandler();
    }

    private void TeamSelectorHandler()
    {
        if (!_cooldown)
        {
            if (!GameManager.Instance.awayTeam.user && !GameManager.Instance.homeTeam.user)
            {
                if (Input.GetAxis("Horizontal") < 0)
                {
                    GameManager.Instance.awayTeam.user = true;
                    MoveLeft();
                }

                if (Input.GetAxis("Horizontal") > 0)
                {
                    GameManager.Instance.homeTeam.user = true;
                    MoveRight();
                }
            }

            if (GameManager.Instance.awayTeam.user && !GameManager.Instance.homeTeam.user)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    GameManager.Instance.awayTeam.user = false;
                    MoveRight();
                }
            }

            if (!GameManager.Instance.awayTeam.user && GameManager.Instance.homeTeam.user)
            {
                if (Input.GetAxis("Horizontal") < 0)
                {
                    GameManager.Instance.homeTeam.user = false;
                    MoveLeft();
                }
            }
        }
    }

    private void MoveLeft()
    {
        var position = gamepadRectTransform.position;

        position.x -= 300;

        gamepadRectTransform.position = position;
        
        StartCoroutine(Cooldown());
    }
    
    private void MoveRight()
    {
        var position = gamepadRectTransform.position;

        position.x += 300;

        gamepadRectTransform.position = position;

        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        _cooldown = true;

        yield return new WaitForSeconds(0.25f);

        _cooldown = false;
    }
}
