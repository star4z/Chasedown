using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicUI: MusicUIBase
{
    public List<bool> beats;
    public List<Image> beatImages;
    public List<Image> beatSelectedImages;
    public Sprite pendingSprite;
    public Sprite succeededSprite;
    public Sprite failedSprite;

    private int _currentBeat = -1;

    public override void SetCurrentBeat(int beat)
    {
        _currentBeat = beat;
        for (var i = 0; i < beatSelectedImages.Count; i++)
        {
            beatSelectedImages[i].gameObject.SetActive(i == beat % beatSelectedImages.Count);
        }
    }

    public override void SetBeatStatus(int beat, bool succeeded)
    {
        beatImages[beat % beatImages.Count].sprite = succeeded ? succeededSprite : failedSprite;
    }

    public override void ResetAllStatuses()
    {
        for (var i = _currentBeat + 1; i < _currentBeat + 1 + beatImages.Count; i++)
        {
            var imageIndex = i % beatImages.Count;
            if (beats[i])
            {
                beatImages[imageIndex].gameObject.SetActive(true);
                beatImages[imageIndex].sprite = pendingSprite;
            }
            else
            {
                beatImages[imageIndex].gameObject.SetActive(false);
            }
        }
    }
}