using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public partial class RewindBall : StateManager<RewindBall> 
{
    public class SimulatedState : State<RewindBall> 
    {
        public SimulatedState(RewindBall manager) : base(manager) { }

        public override IEnumerator Update()
        {
            mn.rigidbody.sharedMaterial = mn.bouncyMaterial;
            
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
            Time.timeScale = 0;
            yield return base.Start();
        }

        public override IEnumerator Stop()
        { 
            yield return base.Stop();
            Time.timeScale = 1;
        }
    }

    public class RewindState : State<RewindBall> 
    {
        private float prevGravity;

        public RewindState(RewindBall manager) : base(manager) { }

        public override IEnumerator Update()
        {     
            mn.rigidbody.sharedMaterial = mn.bouncelessMaterial;
            prevGravity = mn.rigidbody.gravityScale;
            mn.rigidbody.gravityScale = 0;
            mn.OnStartRewind?.Invoke();

            while (mn.rewindMemory.Count > 1) 
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
            mn.rigidbody.sharedMaterial = mn.bouncyMaterial;
            mn.rigidbody.gravityScale = prevGravity;
            mn.rigidbody.velocity = mn.rewindMemory.Last.Value;
            mn.rewindMemory.Clear();
            yield return base.Stop();
        }
    }

    public class DestroyedState : State<RewindBall>
    {        
        public DestroyedState(RewindBall manager) : base(manager) { }

        public override IEnumerator Start()
        {
            mn.OnDestroy?.Invoke();
            mn.gameObject.SetActive(false);
            Instantiate(mn.particles, mn.transform.position, Quaternion.identity);
            yield return base.Start();
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
        }
    }
} 