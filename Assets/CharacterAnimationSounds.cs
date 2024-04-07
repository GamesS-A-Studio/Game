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
    // public string JumpLandSFX = "Play_JumpLand"; 
    public string WallSlideSFX = "Play_WallSlide";

    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.RegisterGameObj(gameObject);
    }

    
    void Play_Dash()
    {
        Debug.Log("Dash Triggered");
        AkSoundEngine.PostEvent(DashSFX, gameObject);
    }

    void Play_Footsteps()
    {
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
        Debug.Log("Play_FootstepsStealth Triggered");
        AkSoundEngine.PostEvent(FootstepsStealth, gameObject);
    }

    void Play_JumpPlayer()
    {
        Debug.Log("JumpPlayer Triggered");
        AkSoundEngine.PostEvent(JumpSFX, gameObject);
    }

    // public void Play_JumpLand()
    // {
    //     Debug.Log("JumpLand Triggered");
    //     AkSoundEngine.PostEvent(JumpLandSFX, gameObject);
    // }

    void Play_WallSlide()
    {
        Debug.Log("WallSlide Triggered");
        AkSoundEngine.PostEvent(WallSlideSFX, gameObject);
    }
}
