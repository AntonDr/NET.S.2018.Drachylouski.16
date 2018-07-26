using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLightLogic
{
    public class TrafficLightV2
    {
       
        public ITrafficLightState State { get; set; }

        public TrafficLightV2(ITrafficLightState trafficLightState)
        {
            State = trafficLightState;
        }

        public void CurrentColor()
        {
            State.CurrentColor(this);
        }

        public void NextColor()
        {
            State.NextColor(this);
        }

        public void PriviousColor()
        {
            State.PreviousColor(this);
        }
    }

    public interface ITrafficLightState
    {
        void CurrentColor(TrafficLightV2 trafficLight);
        void NextColor(TrafficLightV2 trafficLight);
        void PreviousColor(TrafficLightV2 trafficLight);
    }

    public class GreenTrafficLightColor : ITrafficLightState
    {
        public void CurrentColor(TrafficLightV2 trafficLight)
        {
            Console.WriteLine("Green!");
        }

        public void NextColor(TrafficLightV2 trafficLight)
        {
            Console.WriteLine("Yellow!");
            trafficLight.State = new YellowAfterGreenTrafficLightColor();
        }



        public void PreviousColor(TrafficLightV2 trafficLight)
        {
            Console.WriteLine("Yellow!");
            trafficLight.State = new YellowAfterGreenTrafficLightColor();
        }
    }

    public class YellowAfterRedTrafficLightColor : ITrafficLightState
    {
        public void CurrentColor(TrafficLightV2 trafficLight)
        {
            Console.WriteLine("Yellow!");
        }

        public void NextColor(TrafficLightV2 trafficLight)
        {
            Console.WriteLine("Green!");
            trafficLight.State = new GreenTrafficLightColor();
        }

        public void PreviousColor(TrafficLightV2 trafficLight)
        {
            Console.WriteLine("Red!");
            trafficLight.State = new RedTrafficLightColor();
        }
    }

    public class YellowAfterGreenTrafficLightColor : ITrafficLightState
    {
        public void CurrentColor(TrafficLightV2 trafficLight)
        {
            Console.WriteLine("Yellow!");
        }

        public void NextColor(TrafficLightV2 trafficLight)
        {
            Console.WriteLine("Red");
            trafficLight.State = new RedTrafficLightColor();
        }

        public void PreviousColor(TrafficLightV2 trafficLight)
        {
            Console.WriteLine("Green!");
            trafficLight.State = new GreenTrafficLightColor();

        }
    }

    public class RedTrafficLightColor : ITrafficLightState
    {
        public void CurrentColor(TrafficLightV2 trafficLight)
        {
            Console.WriteLine("Red!");
        }

        public void NextColor(TrafficLightV2 trafficLight)
        {
            Console.WriteLine("Yellow!");
            trafficLight.State = new YellowAfterRedTrafficLightColor();
        }

        public void PreviousColor(TrafficLightV2 trafficLight)
        {
            Console.WriteLine("Yellow");
            trafficLight.State = new YellowAfterGreenTrafficLightColor();
        }
    }

}
