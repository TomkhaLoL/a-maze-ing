using amazeing.Scripts.Globals;
using Godot;

public partial class Announcer : Node {
    [Export] public static readonly AudioStream AMAZING = GD.Load<AudioStream>("res://Audio/Announcer/a-maze-ing.mp3");
    [Export] private static readonly AudioStream GREAT_JOB = GD.Load<AudioStream>("res://Audio/Announcer/great_job.mp3");
    
    public static void PlayAnnouncerLine(AudioStream audioStream) {
        AudioManager.singleton.PlaySoundEffect(audioStream);
    }
}