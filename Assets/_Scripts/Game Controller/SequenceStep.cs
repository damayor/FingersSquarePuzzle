using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sequences.Detail
{
    public abstract class SequenceStep : MonoBehaviour
    {
        public event System.Action<SequenceStep> StepEnded;

        public virtual void StartStep()
        {
            ExcecuteStep();
        }

        protected abstract void ExcecuteStep();

        protected void EndStep()
        {
            StepEnded?.Invoke(this);
        }
    }
}