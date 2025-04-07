using System.Collections;
using UnityEngine;

namespace RewindStates 
{
    public class SimulatedState : State<RewindObject> 
    {
        public SimulatedState(RewindObject manager) : base(manager) { }

        public override IEnumerator Start()
        {
            mn.Rigidbody.simulated = true;
            yield return base.Start();
        }
    }    
    
    public class WriteState : State<RewindObject> 
    {
        public WriteState(RewindObject manager) : base(manager) { }

        public override IEnumerator Start()
        {
            float t = 0;
            
            while (t <= mn.MaxWriteTime) 
            {
                mn.RewindMemory.Push(-mn.Rigidbody.velocity);
                t += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            mn.StopWriting();
            yield break;
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

            while (mn.RewindMemory.Count != 0) 
            {
                mn.Rigidbody.velocity = mn.RewindMemory.Pop();
                Debug.Log(mn.Rigidbody.velocity);
                yield return new WaitForFixedUpdate();
            }   
           
            yield return base.Start();
        }
    }

}