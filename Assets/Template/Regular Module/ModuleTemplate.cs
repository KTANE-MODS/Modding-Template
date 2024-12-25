using System.Collections;
using UnityEngine;

public class ModuleTemplate : MonoBehaviour {

    static int ModuleIdCounter = 1;
    private int ModuleId;
    private bool ModuleSolved = false;

    private KMAudio Audio;
    private KMBombInfo Bomb;
    private KMBombModule Module;



    void Awake()
    {
        Audio = GetComponent<KMAudio>();
        Bomb = GetComponent<KMBombInfo>();
        ModuleId = ModuleIdCounter++;
        Module = GetComponent<KMBombModule>();

        //test to check module solving works. When clicked, the module will immedialy solve
        GetComponent<KMSelectable>().OnFocus += delegate () { if (!ModuleSolved) Solve(); };
    }

    void Start()
    { 
        //Get edgework here
    }

    private void Solve()
    {
        ModuleSolved = true;
        KTANELogging($"Module Solved");
        Module.HandlePass();
    }

    private void Strike(string s)
    {
        Module.HandleStrike();
        KTANELogging($"Strike! {s}");
    }


    /// <summary>
    /// Converts c# modulo from (-modulo, modulo) to [0, modulo)
    /// </summary>
    /// <param name="value">what is being moduloed</param>
    /// <param name="modulo">the value that value is being moduloed</param>
    /// <returns></returns>
    private int Modulo(int value, int modulo)
    {
        return ((value % modulo) + modulo) % modulo;
    }

    /// <summary>
    /// Logs something the log in the game. If just for debugging purposes, Debug.Log or Debug.LogFormat should be used instead
    /// </summary>
    /// <param name="s">What will be logged</param>
    private void KTANELogging(string s)
    {
        Debug.Log($"[InsertModNameHere #{ModuleId}] {s}");
    }

    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        yield return null;
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;

        //Solve the mod here

        if (!ModuleSolved)
        {
            //should be null, if needing the solve counter for something
            //otherwise should be true
            yield return true;
        }
    }
}
