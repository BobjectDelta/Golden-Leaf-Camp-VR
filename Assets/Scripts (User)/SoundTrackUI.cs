using TMPro;
using UnityEngine;

public class SoundTrackUI : MonoBehaviour
{
    [SerializeField] private PlaySoundsFromList _soundList;

    [SerializeField] private TextMeshProUGUI _trackNameUI;
    [SerializeField] private TextMeshPro _trackLengthUI;

    string nowPlaying;

    public void UpdateTrackNameUI()
    {
        nowPlaying = _soundList.audioClips[_soundList.GetCurrentTrackIndex()].name;

        _trackNameUI.text = nowPlaying;
    }

}
