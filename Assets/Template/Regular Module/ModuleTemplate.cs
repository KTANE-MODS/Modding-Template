using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using HarmonyLib;

public class ModuleTemplate : MonoBehaviour {

    static int ModuleIdCounter = 1;
    private int ModuleId;
    private bool ModuleSolved = false;
    private const string moduleName = "InsertModNameHere"; //change this to the name of your module. This is used for logging

    private KMAudio Audio;
    private KMBombInfo Bomb;
    private KMBombModule Module;

    //This is all the vanilla edgework, you can delete ones you don't need. Or you can add moded widgets
    private int battaryCount; //number of batteries on the bomb
    private int batterHolderCount; //number of battery holders on the bomb
    private int AABattaryCount { get { return 2 * (battaryCount - batterHolderCount);  } } //number of AA batteries on the bomb
    private int DBatteryCount { get { return battaryCount - AABattaryCount; } } //number of D batteries on the bomb
    private string serialNumber; //the serial number of the bomb
    private string[] litIndicators; //the lit indicators on the bomb
    private string[] unlitIndicators; //the unlit indicators on the bomb
    private string[][] portPlates; //the port plates on the bomb (each array has a collection of ports on that plate)

    void Awake()
    {
        Audio = GetComponent<KMAudio>();
        Bomb = GetComponent<KMBombInfo>();
        ModuleId = ModuleIdCounter++;
        Module = GetComponent<KMBombModule>();

        //test to check module solving works. When clicked, the module will immedialy solve
        //This should be deleted when you implement your own solving logic
        GetComponent<KMSelectable>().OnFocus += delegate () { if (!ModuleSolved) Solve(); };
    }

    void Start()
    { 
        //Get edgework here
        battaryCount = Bomb.GetBatteryCount();
        batterHolderCount = Bomb.GetBatteryHolderCount();
        serialNumber = Bomb.GetSerialNumber();
        litIndicators = Bomb.GetOnIndicators().ToArray();
        unlitIndicators = Bomb.GetOffIndicators().ToArray();
        portPlates = Bomb.GetPortPlates().ToArray();

        //his is just an example of how to log the edgework. Delete this if your mod does not require it
        KTANELogging($"Battary Count {battaryCount}");
        KTANELogging($"Battery Holder Count {batterHolderCount}");
        KTANELogging($"AA Battery Count {AABattaryCount}");
        KTANELogging($"D Battery Count {DBatteryCount}");
        KTANELogging($"Serial Number {serialNumber}");
        KTANELogging($"Lit Indicators {string.Join(", ", litIndicators)}");
        KTANELogging($"Unlit Indicators {string.Join(", ", unlitIndicators)}");
        KTANELogging($"There are {portPlates.Length} port plate(s) on the bomb");
        for (int i = 0; i < portPlates.Length; i++)
        {
            KTANELogging($"Port Plate {i + 1} has {string.Join(", ", portPlates[i])}");
        }
       
        //if edgework is required to solve the module, start calculations here.
        //Otherwie, you can either do it here, or in Awake
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
        Debug.Log($"[{moduleName} #{ModuleId}] {s}");
    }

    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        //make the command given entirley captialized. To make capitialization redundant
        //EX: "Solve" and "solve" will be the same command
        command = command.ToUpperInvariant();

        //set up commands here (I personally perfer regex, but you can use string.Contains or string.StartsWith if you want)
        yield return null;
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;

        //Solve the mod here

        if (!ModuleSolved)
        {
            //should be null if needing the solve counter for something
            //otherwise should be true
            yield return true;
        }
    }
}
