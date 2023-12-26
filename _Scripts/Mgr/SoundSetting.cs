using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using static MoreMountains.Tools.MMSoundManager;

public class SoundSetting : MonoBehaviour //set sound volume and input mode
{
    // Start is called before the first frame update
    public MMF_Player musicFB, soundFB;
    public MMF_MMSoundManagerTrackControl musicVolume, soundVolume;
    public MySlider myMusicSlider,mySoundSlider;
    public MyToggle myToggle;
    private void Awake()
    {
        musicVolume = musicFB.GetFeedbackOfType<MMF_MMSoundManagerTrackControl>();
        soundVolume = soundFB.GetFeedbackOfType<MMF_MMSoundManagerTrackControl>();
        myMusicSlider.Value =MMSoundManager.Instance.GetTrackVolume(MMSoundManagerTracks.Music,false) * 10;
        mySoundSlider.Value= MMSoundManager.Instance.GetTrackVolume(MMSoundManagerTracks.Sfx , false) * 10;
        myToggle.toggledOn = PlayerPrefs.GetString(Global.inputMode, Global.keyboardInput) == Global.arduinoInput;
    }
 
    private void OnEnable()
    {
        myMusicSlider.Value = MMSoundManager.Instance.GetTrackVolume(MMSoundManagerTracks.Music, false)* 10;
        mySoundSlider.Value = MMSoundManager.Instance.GetTrackVolume(MMSoundManagerTracks.Sfx, false)* 10;
    }
    public void ChangeInputMode(bool toggled)
    {
         PlayerPrefs.SetString(Global.inputMode, toggled ? Global.arduinoInput : Global.keyboardInput); 
         Debug.Log(PlayerPrefs.GetString(Global.inputMode, Global.keyboardInput));
    
    }
    public void ChangeMusicVolume(float amount)
    {
        amount=(int) amount;
        amount /= 10;
        musicVolume.Volume = amount;
        musicFB.PlayFeedbacks();

    }

    public void ChangeSoundVolume(float amount)
    {
        amount=(int) amount;
        amount /= 10;
        soundVolume.Volume = amount;
        soundFB.PlayFeedbacks();
       
    }
    
    
}
