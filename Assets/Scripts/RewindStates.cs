using System.Collections;
using UnityEngine;

public partial class RewindObject : StateManager<RewindObject> 
{
    public class SimulatedState : State<RewindObject> 
    {
        public SimulatedState(RewindObject manager) : base(manager) { }

        public override IEnumerator Update()
        {
            mn.rigidbody.simulated = true;
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

    public class StopState : State<RewindObject> 
    {
        public StopState(RewindObject manager) : base(manager) { }

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

    public class RewindState : State<RewindObject> 
    {
        private float prevGravity;

        public RewindState(RewindObject manager) : base(manager) { }

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

} 