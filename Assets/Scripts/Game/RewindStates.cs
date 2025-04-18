using System.Collections;
using UnityEngine;

public partial class RewindBall : StateManager<RewindBall> 
{
    public class SimulatedState : State<RewindBall> 
    {
        public SimulatedState(RewindBall manager) : base(manager) { }

        public override IEnumerator Update()
        {
            mn.OnStartSimulation?.Invoke();
            mn.bounciness = mn.defualtBounciness;
            
            while (true) 
            {
                if (mn.rewindMemory.Count == mn.MaxMemorySize) 
                {                    
                    mn.rewindMemory.RemoveFirst();
                }
                mn.rewindMemory.AddLast(-mn.rigidbody.velocity);

                yield return new WaitForFixedUpdate();
            }
        }
    } 

    public class StopState : State<RewindBall> 
    {
        public StopState(RewindBall manager) : base(manager) { }

        public override IEnumerator Start()
        {
            mn.OnStartStop?.Invoke();
            Time.timeScale = 0;
            yield return base.Start();
        }

        public override IEnumerator Stop()
        { 
            mn.OnEndStop?.Invoke();
            Time.timeScale = 1;
            yield return base.Stop();
        }
    }

    public class RewindState : State<RewindBall> 
    {
        public RewindState(RewindBall manager) : base(manager) { }

        public override IEnumerator Update()
        {     
            mn.bounciness = 0;
            mn.rigidbody.gravityScale = 0;
            mn.OnStartRewind?.Invoke();

            while (mn.rewindMemory.Count > 0) 
            {
                mn.rigidbody.velocity = mn.rewindMemory.Last.Value;
                mn.rewindMemory.RemoveLast();
                yield return new WaitForFixedUpdate();
            }   
        
            mn.StartSimulating();
            yield break;
        }

        public override IEnumerator Stop()
        {
            mn.OnEndRewind?.Invoke();
            BaseRewind.StopRewindAll();
            mn.bounciness = mn.defualtBounciness;
            mn.rigidbody.gravityScale = mn.defualtGravity;
            yield return base.Stop();
        }
    }

    public class DestroyedState : State<RewindBall>
    {        
        public DestroyedState(RewindBall manager) : base(manager) { }

        public override IEnumerator Start()
        {
            mn.OnDestroy?.Invoke();
            Instantiate(mn.particles, mn.transform.position, Quaternion.identity);
            mn.rigidbody.simulated = false;
            mn.spriteRenderer.enabled = false;
            yield return base.Start();

            yield return new WaitForSeconds(mn.defeatDelay);
            mn.AfterDestroy?.Invoke();
        }
    }

    public class WinState : State<RewindBall>
    {        
        public WinState(RewindBall manager) : base(manager) { }

        public override IEnumerator Start()
        {
            mn.OnWin?.Invoke();
            mn.rigidbody.simulated = false;
            yield return base.Start();
            
            yield return new WaitForSeconds(mn.winDelay);
            mn.AfterWin?.Invoke();
        }
    }
} 