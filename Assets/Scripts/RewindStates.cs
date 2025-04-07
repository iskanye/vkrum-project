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
            mn.Rigidbody.simulated = false;  
            yield return base.Start();
        }
    }

    public class RewindState : State<RewindObject> 
    {
        public RewindState(RewindObject manager) : base(manager) { }

        public override IEnumerator Start()
        {   
            mn.Rigidbody.simulated = true;   
            mn.Rigidbody.sharedMaterial = mn.BouncelessMaterial;
            mn.Rigidbody.gravityScale /= 2; // TODO: исправить как нибудь это с гравитацией

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
            mn.Rigidbody.gravityScale *= 2; // TODO: исправить как нибудь это с гравитацией
            mn.Rigidbody.velocity = mn.RewindMemory.Last.Value;
            mn.RewindMemory.RemoveLast();
            yield return base.Stop();
        }
    }

} 