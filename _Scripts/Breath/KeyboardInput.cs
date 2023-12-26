using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Nova;
using MoreMountains.Tools;

public class KeyboardInput : MonoBehaviour
{
    public BubbleController bubbleController;
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
        lastBreathTime = Time.time;
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
        if (Input.GetKey(KeyCode.Space))
        {
            bubbleController.EnlargeBubble();
        }
        bubbleController.SetHorizontalInput(Input.GetAxis("Horizontal"));
        bubbleController.SetVerticalInput(Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.B))// use keyboard to simulate exhale
        {
            breathDuration = Time.time - lastBreathTime;
            exhaleFB.PlayFeedbacks();
            lastBreathTime = Time.time;
            calmTime = 0;
            if (exhale)
            {
                sliderBlock.Color = exhaleFinishColor;
            }
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
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            inhaleFB.PlayFeedbacks();
        }

        if (Input.GetKey(KeyCode.B))
        {
            isBreath = true;
        }
        else
        {
            isBreath = false;
            calmTime += Time.deltaTime;
            float t = Mathf.Clamp01(calmTime / calmTargetTime);
            float newValue = Mathf.Lerp(0, 1, t);
            colorChange.ChangeMatColor(newValue);
            if (calmTime >= calmTargetTime)//calm too long, stop moving
            {
                bubbleController.canMove = false;
            }
        }
       
    }
}
