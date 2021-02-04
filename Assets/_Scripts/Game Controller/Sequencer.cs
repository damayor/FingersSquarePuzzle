using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sequences.Detail
{
    public class Sequencer : MonoBehaviour, ISequencer
    {
        public event Action OnSecuenceFinished;

        [SerializeField] private bool playSequenceOnStart = false;

        [SerializeField]
        protected List<SequenceStep> steps;

        protected int stepIndex = 0;

        public bool IsPlaying { get; private set; } = false;

        private void Start()
        {
            if (playSequenceOnStart) StartSequence();
        }

        public void StartSequence()
        {
            if (steps.Count == 0)
            {
                EndSequence();
            }
            else
            {
                stepIndex = 0;
                steps[0].StepEnded += OnFinishStep;
                steps[0].StartStep();
            }

            IsPlaying = true;
        }

        protected virtual void OnFinishStep(SequenceStep finishedStep)
        {
            finishedStep.StepEnded -= OnFinishStep;
            ExcecuteNextStep();
        }

        private void ExcecuteNextStep()
        {
            stepIndex++;
            if (steps.Count <= stepIndex)
            {
                EndSequence();
            }
            else
            {
                steps[stepIndex].StepEnded += OnFinishStep;
                steps[stepIndex].StartStep();
            }
        }

        protected virtual void EndSequence()
        {
            IsPlaying = false;
            OnSecuenceFinished?.Invoke();
        }


    }
}

namespace Sequences
{
    public interface ISequencer
    {
        void StartSequence();
    }
}
