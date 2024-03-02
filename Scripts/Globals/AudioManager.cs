using System.Collections.Generic;
using Godot;

namespace amazeing.Scripts.Globals;

public partial class AudioManager : Node {
    [Export] private AudioStreamPlayer bgmPlayer;
    private List<AudioStreamPlayer> sfxPlayerPool = new List<AudioStreamPlayer>();

    public AudioStream bgmMusicNormal = GD.Load<AudioStream>("res://Audio/Music/easy_cheesy_bitcrushed_base-48k.mp3");
    public AudioStream bgmMusicCursed = GD.Load<AudioStream>("res://Audio/Music/easy_cheesy_bitcrushed_reversed.mp3");

    public AudioStream bgmMusicCutoff =
        GD.Load<AudioStream>("res://Audio/Music/easy_cheesy_bitcrushed_base-fuckedup.mp3");

    private int maxSfxPlayers = 6;

    public static AudioManager singleton;

    public override void _Ready() {
        if (singleton != null) {
            singleton = this;
        }

        bgmPlayer = new AudioStreamPlayer();
        for (int i = 0; i < maxSfxPlayers; i++) {
            AudioStreamPlayer sfxPlayer = new AudioStreamPlayer();
            GetTree().Root.AddChild(sfxPlayer);
            sfxPlayerPool.Add(sfxPlayer);
        }
        PlayBackgroundMusic(bgmMusicNormal);
    }

    public void PlayBackgroundMusic(AudioStream audioStream) {
        if (bgmPlayer != null) {
            bgmPlayer.Stop();
            bgmPlayer.Stream = audioStream;
            bgmPlayer.Play();
        }
        else {
            GD.PushError("No bgm player found, make sure it has been initialized correctly.");
        }
    }

    public void PlaySoundEffect(AudioStream audioStream) {
        AudioStreamPlayer sfxPlayer = GetSfxPlayer();
        if (sfxPlayer != null) {
            sfxPlayer.Stream = audioStream;
            sfxPlayer.Play();
        }
    }

    public void StopAllSfx() {
        foreach (var sfxPlayer in sfxPlayerPool) {
            sfxPlayer.Stop();
        }
    }

    private AudioStreamPlayer GetSfxPlayer() {
        foreach (var sfxPlayer in sfxPlayerPool) {
            if (!sfxPlayer.Playing) {
                return sfxPlayer;
            }
        }

        GD.PushError("No free sfxplayers, make sure to increase max sfx players or clean up old ones.");
        return null;
    }
}