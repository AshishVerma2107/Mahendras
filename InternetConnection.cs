using System;
using Plugin.Connectivity;

namespace ImageSlider
{
    class InternetConnection
    {
        public Boolean connectivity()
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}