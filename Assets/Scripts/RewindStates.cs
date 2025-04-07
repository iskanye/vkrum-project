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
            float t = 1;
            while (t > 0) 
            {
                Time.timeScale = t;
                t -= Time.fixedDeltaTime;
                yield return null;
            }
            yield return base.Start();
        }

        public override IEnumerator Stop()
        { 
            IsStopping = true;
            yield return base.Stop();
            float t = 0;
            while (t < 1) 
            {
                Time.timeScale = t;
                t += Time.fixedDeltaTime;
                yield return null;
            }
            IsStopping = false;
        }
    }

    public class RewindState : State<RewindObject> 
    {
        private float prevGravity;

        public RewindState(RewindObject manager) : base(manager) { }

        public override IEnumerator Update()
        {   
            mn.Rigidbody.simulated = true;   
            mn.Rigidbody.sharedMaterial = mn.BouncelessMaterial;
            prevGravity = mn.Rigidbody.gravityScale; // TODO: исправить как нибудь это с гравитацией
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
            yield return base.Stop();
            mn.Rigidbody.sharedMaterial = mn.BouncyMaterial;
            mn.Rigidbody.gravityScale = prevGravity; // TODO: исправить как нибудь это с гравитацией
            mn.Rigidbody.velocity = mn.RewindMemory.Last.Value;
            mn.RewindMemory.Clear();
        }
    }

} 