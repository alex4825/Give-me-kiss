using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _soundClip;

    private void OnEnable()
    {
        ButtonHandler.OnSomeClick += PlaySound;
        Messanger.OnSomeMessageSent += PlaySound;
    }

    public void PlaySound()
    {
        _audioSource.PlayOneShot(_soundClip);
    }
}
