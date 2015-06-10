using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerMelee : NetworkBehaviour {

    public Animator SwordAnim;
    
    public enum SWINGDIRECTION
    {
        DEFAULT = 0,
        LEFT = 1,
        RIGHT = 2,
        UP = 3,
        DOWN = 4
    }

    public int CurrSwingDirection;
    
    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }

        CmdCheckInput();
	}

    [Command]
    void CmdCheckInput()
    {
        Vector3 SwingDir = new Vector3(Input.GetAxisRaw("Mouse X"),  // X    
                                               0,                            // Y
                                               Input.GetAxisRaw("Mouse Y"));   // Z (think of this as a D-Pad)

        if (Input.GetButton("Fire1"))
        {
            CmdCheckSwingDirection(SwingDir);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            CmdSwing();
        }
    }

    [Command]
    void CmdCheckSwingDirection(Vector3 SwingDir)
    {
        if (SwingDir.x < 0)
            CurrSwingDirection = (int)SWINGDIRECTION.LEFT;
        if (SwingDir.x > 0)
            CurrSwingDirection = (int)SWINGDIRECTION.RIGHT;

        if (SwingDir.z > 0)
            CurrSwingDirection = (int)SWINGDIRECTION.UP;
        if (SwingDir.z < 0)
            CurrSwingDirection = (int)SWINGDIRECTION.DOWN;

        if (SwingDir == Vector3.zero)
            CurrSwingDirection = (int)SWINGDIRECTION.DEFAULT;
    }

    [Command]
    void CmdSwing()
    {
        if (CurrSwingDirection == (int)SWINGDIRECTION.LEFT)
            Debug.Log("LEFT SWING");
        if (CurrSwingDirection == (int)SWINGDIRECTION.RIGHT)
            Debug.Log("RIGHT SWING");

        if (CurrSwingDirection == (int)SWINGDIRECTION.UP)
            SwordAnim.SetTrigger("UpSwing");
        if (CurrSwingDirection == (int)SWINGDIRECTION.DOWN)
            Debug.Log("DOWN SWING");

        if (CurrSwingDirection == (int)SWINGDIRECTION.DEFAULT)
            SwordAnim.SetTrigger("DefaultSwing");
    }

}
