namespace RestartWhenNoPlayers;

using System;
using System.Collections.Generic;
using Exiled.API.Features;
using MEC;

public class Plugin : Plugin<Config>
{
    public override string Name => "Restart When No Players";

    public override string Author => "TtroubleTT";

    public override Version Version => new(1, 0, 0);

    private static CoroutineHandle _coroutineHandle;

    public override void OnEnabled()
    {
        Timing.CallDelayed(5f, CallCoroutine);
        _coroutineHandle = Timing.RunCoroutine(RestartWhenNoPlayers());
    }

    public override void OnDisabled()
    {
        Timing.KillCoroutines(_coroutineHandle);
    }

    private static void CallCoroutine()
    {
        _coroutineHandle = Timing.RunCoroutine(RestartWhenNoPlayers());
    }

    private static IEnumerator<float> RestartWhenNoPlayers()
    {
        for (;;)
        {
            yield return Timing.WaitForSeconds(120);

            if (Server.PlayerCount <= 0)
            {
                Server.Restart();
            }
        }
    }
}