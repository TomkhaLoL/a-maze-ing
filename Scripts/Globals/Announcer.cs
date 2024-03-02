using amazeing.Scripts.Globals;
using Godot;
using Range = System.Range;

public partial class Announcer : Node {
    [Export] public static readonly AudioStream AMAZING = GD.Load<AudioStream>("res://Audio/Announcer/a-maze-ing.mp3");
    [Export] private static readonly AudioStream GREAT_JOB = GD.Load<AudioStream>("res://Audio/Announcer/great_job.mp3");
    [Export] private static readonly AudioStream SCREAM = GD.Load<AudioStream>("res://Audio/Announcer/spookyghost.mp3");


    private static AudioStream[] congratulatoryLines = new[] { AMAZING, GREAT_JOB };
    public static void PlayAnnouncerLine(AudioStream audioStream) {
        AudioManager.singleton.PlaySoundEffect(audioStream);
    }

    public static void PlayRandomCongratulatoryLine() {
        //congratulatoryLines[Range.EndAt(congratulatoryLines.Length - 1)];
    }
}