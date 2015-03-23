using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Math;
using Leap;
using Trame.Implementation.Skeleton;

namespace Trame.Implementation.Device.Adapter
{
    class LeapAdapter
    {
        private Controller leapController;

        public void StartController(Action<Frame> onFrameArrived)
        {
            leapController = new Controller(new FrameListener(onFrameArrived));
            leapController.SetPolicy(Controller.PolicyFlag.POLICY_BACKGROUND_FRAMES);
        }

        public void Stop()
        {
            
        }
    }

    class FrameListener : Listener
    {
        private Action<Frame> onFrameArrived;

        public FrameListener(Action<Frame> onFrameArrived)
        {
            this.onFrameArrived = onFrameArrived;
        }

        public override void OnFrame(Controller controller)
        {
            onFrameArrived(controller.Frame());
        }
    }
}
