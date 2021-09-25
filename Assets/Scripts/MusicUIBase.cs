using UnityEngine;

public abstract class MusicUIBase: MonoBehaviour
{
    /// <summary>
    /// | *  *  *  *  |  *  *  *  * |
    ///
    /// SetCurrentBeat(1)
    ///
    /// | * (*) *  *  |  *  *  *  * |
    /// 
    /// </summary>
    /// <param name="beat"></param>
    public abstract void SetCurrentBeat(int beat);

    public abstract void SetBeatStatus(int beat, bool succeeded);

    public abstract void ResetAllStatuses();
}