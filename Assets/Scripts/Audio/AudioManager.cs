using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private List<Sound> music, sounds;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void PlayMusic(string soundName)
        {
            Sound s = music.Find(x => x.name == soundName);

            if (s == null)
            {
                Debug.LogWarning("Music: " + soundName + " not found!");
                return;
            }
            
            musicSource.clip = s.clip;
            musicSource.Play();
        }

        public void PlaySfx(string soundName)
        {
            Sound s = sounds.Find(x => x.name == soundName);

            if (s == null)
            {
                Debug.LogWarning("Sound " + soundName + " not found!");
                return;
            }
            
            sfxSource.PlayOneShot(s.clip);
        }

        public void ShuffleMusic()
        {
            List<Sound> playlist = music;
            int n = playlist.Count;

            if (n < 1)
            {
                Debug.Log("Music is empty!");
                return;
            }
            
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                (playlist[k], playlist[n]) = (playlist[n], playlist[k]);
            }

            StartCoroutine(PlayMusicPlaylist(playlist));
        }

        private IEnumerator PlayMusicPlaylist(List<Sound> playlist)
        {
            foreach (var song in playlist)
            {
                PlayMusic(song.name);
                yield return new WaitForSeconds(song.clip.length + 3);
            }
        }
    }
}