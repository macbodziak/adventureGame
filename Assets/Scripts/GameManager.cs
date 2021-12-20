using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private PlayerInputActions playerInputActions;


    public static GameManager Instance { get { return _instance; } }
    
    public PlayerInputActions PlayerInput {
        get { return playerInputActions;}
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            Debug.Assert(playerInputActions != null);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
