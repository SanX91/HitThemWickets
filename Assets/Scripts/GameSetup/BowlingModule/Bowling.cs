using System;
using System.Collections;
using UnityEngine;

namespace HitThemWickets
{
    /// <summary>
    /// The Bowling class which implements the IBowling interface.
    /// Intializes the bowling cycle and repeats it until the player quits.
    /// Current bowling cycle -> 1. Start bowling. -> 2. Set spin. -> 3. Set ball bounce spot. -> 4. Set bounce amount. -> 5. Bowl. -> Repeat Step 1.
    /// </summary>
    public class Bowling : IBowling
    {
        private enum States
        {
            NewBall,
            Spinner,
            Indicator,
            Bouncer,
            EndBall
        }

        private IController controller;
        private IBall ball;
        private States state;

        public Bowling(IController controller, IBall ball)
        {
            this.controller = controller;
            this.ball = ball;

            EventManager.Instance.AddListener<SetSpinEvent>(OnSetSpinEvent);
            EventManager.Instance.AddListener<SetPositionEvent>(OnSetPositionEvent);
            EventManager.Instance.AddListener<SetBounceEvent>(OnSetBounceEvent);
            EventManager.Instance.AddListener<EndBallEvent>(OnEndBallEvent);
        }

        private void OnEndBallEvent(EndBallEvent evt)
        {
            if (state == States.EndBall)
            {
                state = States.NewBall;
            }
        }

        private void OnSetPositionEvent(SetPositionEvent evt)
        {
            ball.SetPosition((Vector3)evt.GetData());
        }

        private void OnSetSpinEvent(SetSpinEvent evt)
        {
            ball.SetSpin((float)evt.GetData());
        }

        private void OnSetBounceEvent(SetBounceEvent evt)
        {
            ball.SetBounce((float)evt.GetData());
        }


        /// <summary>
        /// This method requires some cleaning up.
        /// </summary>
        /// <returns></returns>
        public IEnumerator BowlingCycle()
        {
            //Bowling cycle
            while (true)
            {
                yield return new WaitForSeconds(2);

                //New Ball Event
                ball.Initialize();
                EventManager.Instance.TriggerEvent(new NewBallEvent());

                //Start new ball
                yield return new WaitUntil(() => controller.IsReady());
                state = States.Spinner;

                //Start spinner
                EventManager.Instance.TriggerEvent(new ToggleSpinnerEvent(true));
                yield return null;

                //Wait for player's input
                yield return new WaitUntil(() => controller.IsReady());
                state = States.Indicator;

                //Stop spinner, start indicator
                EventManager.Instance.TriggerEvent(new ToggleSpinnerEvent(false));
                EventManager.Instance.TriggerEvent(new ToggleIndicatorEvent(true));
                yield return null;

                //Wait for player's input
                yield return new WaitUntil(() => controller.IsReady());
                state = States.Bouncer;

                //Stop indicator, start bounce selector
                EventManager.Instance.TriggerEvent(new ToggleIndicatorEvent(false));
                EventManager.Instance.TriggerEvent(new ToggleBounceSelectorEvent(true));
                yield return null;

                //Wait for player's input
                yield return new WaitUntil(() => controller.IsReady());
                state = States.EndBall;

                //Stop bounce selector
                EventManager.Instance.TriggerEvent(new ToggleBounceSelectorEvent(false));
                yield return null;

                //Bowl
                ball.Bowl();

                //Wait for player's input to start next bowling cycle
                yield return new WaitUntil(() => state == States.NewBall);
            }
        }
    }
}
