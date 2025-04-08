using System.Collections;
using UnityEngine;

namespace RewindStates 
{
    public class SimulatedState : State<RewindObject> 
    {
        public SimulatedState(RewindObject manager) : base(manager) { }

        public override IEnumerator Update()
        {
            mn.Rigidbody.simulated = true;
            mn.Rigidbody.sharedMaterial = mn.BouncyMaterial;
            
            while (true) 
            {
                if (mn.RewindMemory.Count == mn.MaxMemorySize) 
                {                    
                    mn.RewindMemory.RemoveFirst();
                }
                mn.RewindMemory.AddLast(-mn.Rigidbody.velocity);

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
            mn.Rigidbody.sharedMaterial = mn.BouncelessMaterial;
            prevGravity = mn.Rigidbody.gravityScale;
            mn.Rigidbody.gravityScale = 0;

            while (mn.RewindMemory.Count > 1) 
            {
                mn.Rigidbody.velocity = mn.RewindMemory.Last.Value;
                mn.RewindMemory.RemoveLast();
                yield return new WaitForFixedUpdate();
            }   
        
            mn.StartSimulating();
            yield break;
        }

        public override IEnumerator Stop()
        {
            mn.Rigidbody.sharedMaterial = mn.BouncyMaterial;
            mn.Rigidbody.gravityScale = prevGravity;
            mn.Rigidbody.velocity = mn.RewindMemory.Last.Value;
            mn.RewindMemory.Clear();
            yield return base.Stop();
        }
    }

} 