using UnityEngine;

public class MusicUI: MusicUIBase
{
    public override void SetCurrentBeat(int beat)
    {
        Debug.Log(beat);
    }

    public override void SetBeatStatus(int beat, bool succeeded)
    {
        Debug.Log($"{beat}={succeeded}");
    }

    public override void ResetAllStatuses()
    {
        Debug.Log("resetting");
    }
}