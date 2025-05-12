using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Dialogue")]
public class DialogueSO : ScriptableObject
{
    [SerializeField] private string[] _lines;
    [SerializeField] private AudioClip[] _clips; // Agregar los clips de audio

    public string[] Lines
    {
        get { return _lines; }
        set { _lines = value; }
    }

    public AudioClip[] Clips // Propiedad para acceder a los clips de audio
    {
        get { return _clips; }
        set { _clips = value; }
    }
}
