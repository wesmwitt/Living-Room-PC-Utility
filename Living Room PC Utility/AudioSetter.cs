using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Living_Room_PC_Utility
{
    
    public static class AudioSetter
    {


        //0 = Stereo, 1 = 5.1, 2 = 7.1
        public static void setSurround(int settingNum)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"resources\programs\svcl.exe");

            string soundStr = "0x3 0x3 0x3"; //Default Stereo
             if (settingNum == 1)
            {
                soundStr = " 0x3f 0x3f 0x3f";
            } else if (settingNum == 2)
            {
                soundStr = "0x63f 0x63f 0x63f";
            }

        }


    }
}
