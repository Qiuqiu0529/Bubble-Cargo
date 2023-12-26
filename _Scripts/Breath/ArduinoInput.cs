using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Nova;
using MoreMountains.Tools;

public class ArduinoInput : MonoBehaviour
{
    public ArduinoRead arduinoRead;
    public BubbleController bubbleController;
    public int maxBreathAmount;
    public float calmTime = 0;// time since the user stopped breathing
    public bool isBreath = false;
    public bool preBreath = false;
    public float lastBreathTime;
    public float breathDuration = 0;

    public float calmTargetTime = 15;// time threshold for calm state
    public float breathDurationTarget = 4;// target duration of a breath

    public MMF_Player exhaleFB, inhaleFB;// play sound for exhale and inhale actions
    public ColorChange colorChange;

    public Color exhaleColor, inhaleColor, exhaleFinishColor;
    public UIBlock2D sliderBlock;
    public TextBlock sliderProgress;
    public float exhaleDuration, inhaleDuration;
    bool exhale;

    private void Start()
    {
        StartCoroutine(SliderColorChangeIenum());
    }

    public IEnumerator SliderColorChangeIenum()
    {
        while (true)
        {
            sliderBlock.Color = inhaleColor;
            float tempStartTime = Time.time;
            float duration = 0;
            exhale = false;

            // inhale animation
            while (duration <= inhaleDuration)
            {
                float t = Mathf.Clamp01(duration / inhaleDuration);
                float progress = MMTweenDefinitions.Linear_Tween(t);
                float newValue = Mathf.Lerp(0, 1, progress);
                sliderProgress.Text = ((int)(newValue * 100)).ToString() + "%";
                sliderBlock.Size.X.Percent = newValue;
                yield return new WaitForSeconds(0.05f);
                duration += 0.05f;
            }
            duration = 0;
            sliderBlock.Color = exhaleColor;
            exhale = true;

            // exhale animation
            while (duration <= exhaleDuration)
            {
                float t = Mathf.Clamp01(duration / exhaleDuration);
                float progress = MMTweenDefinitions.Linear_Tween(t);
                float newValue = Mathf.Lerp(0, 1, progress);
                newValue = 1 - newValue;
                sliderProgress.Text = ((int)(newValue * 100)).ToString() + "%";
                sliderBlock.Size.X.Percent = newValue;
                yield return new WaitForSeconds(0.05f);
                duration += 0.05f;
            }
        }
    }


    void Update()
    {
        if (arduinoRead.keyValue == 3)
        {
            // size
            bubbleController.EnlargeBubble();
        }
        else
        {
            // move
            if (arduinoRead.keyValue == 1)
                bubbleController.Right();
            else if (arduinoRead.keyValue == 2)
                bubbleController.Down();
            else if (arduinoRead.keyValue == 4)
                bubbleController.Up();
            else if (arduinoRead.keyValue == 5)
                bubbleController.Left();
            else
                bubbleController.Stay();
        }

        // if air expulsion less than limit
        if (arduinoRead.breathAmount <= 10)
        {
            isBreath = false;
            calmTime += Time.deltaTime;

            float t = Mathf.Clamp01(calmTime / calmTargetTime);
            float newValue = Mathf.Lerp(0, 1, t);
            colorChange.ChangeMatColor(newValue);

            // transitioning from breath to calm state
            if (preBreath)
            {
                inhaleFB.PlayFeedbacks();
            }

            if (calmTime >= calmTargetTime)//calm too long, stop moving
            {
                bubbleController.canMove = false;
            }
        }
        else
        {
            if (!preBreath)
            {
                breathDuration = Time.time - lastBreathTime;
                lastBreathTime = Time.time;
                exhaleFB.PlayFeedbacks();

                // change slider color to exhale finish color
                if (exhale)
                {
                    sliderBlock.Color = exhaleFinishColor;
                }
            }
            
            // enable bubble movement
            if (breathDuration >= breathDurationTarget)
            {
                bubbleController.canMove = true;
                colorChange.ChangeMatColor(0);
            }
            else
            {
                colorChange.ChangeMatColor(1);
                bubbleController.canMove = false;
            }

            isBreath = true;
            calmTime = 0;
        }
        preBreath = isBreath;
    }

}