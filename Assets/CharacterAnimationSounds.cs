using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationSounds : MonoBehaviour
{
    public bool Debug_Enabled = false;
    public string DashSFX = "Play_Dash";
    public string FootsepsSFX = "Play_Footsteps";
    public string Silence = "Play_Silence";
    public string FootstepsStealth = "Play_FootstepsStealth";
    public string JumpSFX = "Play_JumpPlayer";
    public string JumpLandSFX = "Play_JumpLand"; 
    public string WallSlideSFX = "Play_WallSlide";
    public string JumpWallSFX = "Play_JumpWall";
    public string AttackSlashSFX = "Play_AttackSlash";
    public string HitBoxSFX = "Play_HitBox";

    private bool hasStarted = false; // Track if the game has started.

    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.RegisterGameObj(gameObject);

        // Disable sound events until a short delay has passed.
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        // Wait for 1 second before enabling sound events.
        yield return new WaitForSeconds(1f);

        // Set the flag to indicate that the game has started.
        hasStarted = true;
    }

    void Play_Dash()
    {

        
        Debug.Log("Dash Triggered");
        AkSoundEngine.PostEvent(DashSFX, gameObject);
    }

    void Play_Footsteps()
    {
        if (!hasStarted) return; // Check if the game has started.

        Debug.Log("Footsteps Triggered");
        AkSoundEngine.PostEvent(FootsepsSFX, gameObject);
    }

    void Play_Silence()
    {


        Debug.Log("Silence Triggered");
        AkSoundEngine.PostEvent(Silence, gameObject);
    }

    void Play_FootstepsStealth()
    {
        if (!hasStarted) return; // Check if the game has started.

        Debug.Log("Play_FootstepsStealth Triggered");
        AkSoundEngine.PostEvent(FootstepsStealth, gameObject);
    }

    void Play_JumpPlayer()
    {
        if (!hasStarted) return; // Check if the game has started.

        Debug.Log("JumpPlayer Triggered");
        AkSoundEngine.PostEvent(JumpSFX, gameObject);
    }

    public void Play_JumpLand()
    {
        if (!hasStarted) return; // Check if the game has started.

        Debug.Log("JumpLand Triggered");
        AkSoundEngine.PostEvent(JumpLandSFX, gameObject);
    }

    public void Play_WallSlide()
    {


        Debug.Log("WallSlide Triggered");
        AkSoundEngine.PostEvent(WallSlideSFX, gameObject);
    }

    public void Play_JumpWall()
    {

        Debug.Log("JumpWall Triggered");
        AkSoundEngine.PostEvent(JumpWallSFX, gameObject);
    }

    public void Play_AttackSlash()
    {

        Debug.Log("Atatck Slash Triggered");
        AkSoundEngine.PostEvent(AttackSlashSFX, gameObject);
    }

    public void Play_HitBox()
    {

        Debug.Log("HitBox Triggered");
        AkSoundEngine.PostEvent(HitBoxSFX, gameObject);
    }
}
