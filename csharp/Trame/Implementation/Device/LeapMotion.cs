using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trame.Implementation.Device.Adapter;
using Trame.Implementation.Skeleton;

namespace Trame.Implementation.Device
{
    class LeapMotion : IDevice
    {
        readonly LeapAdapter adapter = new LeapAdapter();
        private Thread t;
        private bool running = true;

        public LeapMotion()
        {
            t = new Thread(Run);
            t.Start();
        }
        
        private void Run()
        {
            while (running)
            {
                FireNewSkeleton();
            }
        }

        public ISkeleton GetSkeleton()
        {
            ISkeleton s = Creator.GetNewDefaultSkeleton();
            return GetSkeleton(s);
        }

        public ISkeleton GetSkeleton(ISkeleton s)
        {
            IJoint leftWrist = s.GetJoint(JointType.WRIST_LEFT);
            IJoint rightWrist = s.GetJoint(JointType.WRIST_RIGHT);

            IJoint leftHand = adapter.LeftHand(leftWrist);
            leftWrist.Valid = leftHand.Valid;
            leftWrist.Update(JointType.HAND_LEFT, leftHand);

            IJoint rightHand = adapter.RightHand(rightWrist);
            rightWrist.Valid = rightHand.Valid;
            rightWrist.Update(JointType.HAND_RIGHT, rightHand);

            s.UpdateSkeleton(JointType.WRIST_LEFT, leftWrist);
            s.UpdateSkeleton(JointType.WRIST_RIGHT, rightWrist);

            s.Valid = leftWrist.Valid && rightHand.Valid;

            return s;
        }

        public void Stop()
        {
            adapter.Stop();
            running = false;
            t.Join();
        }

        private void FireNewSkeleton()
        {
            if (NewSkeleton != null)
            {
                NewSkeleton(GetSkeleton());
            }
        }

        public event Action<ISkeleton> NewSkeleton;
    }
}
