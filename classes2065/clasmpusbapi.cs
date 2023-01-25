using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;
using PVOID = System.IntPtr;
using DWORD = System.UInt32;
using System.Text;

namespace Skydat.classes2065
{
    unsafe public class clasmpusbapi
    {
        public const byte P_A = 0xC0;//خانم دکتر: هنگام تست در زمان هنگ برنامه و مواردی دیگر با دادن ورودی های متفاوت و تست خروجی تنظیم شد
        public const byte P_B = 0xC4;
        public const byte P_C = 0xC8;
        public const byte P_P = 0xCC;
        public const byte BuffSize = 14;
        #region Imported DLL functions from mpusbapi.dll

        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBGetDLLVersion();
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBGetDeviceCount(string pVID_PID);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern PVOID _MPUSBOpen(DWORD instance, string pVID_PID, string pEP, DWORD dwDir, DWORD dwReserved);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBGetDeviceDescriptor(PVOID handle, string pDevDsc, DWORD dwLen, DWORD* pLength);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBGetConfigurationDescriptor(PVOID handle, byte bIndex, string pDevDsc, DWORD dwLen, DWORD* pLength);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBGetStringDescriptor(PVOID handle, byte bIndex, DWORD wLangId, string pDevDsc, DWORD dwLen, DWORD* pLength);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBSetConfiguration(PVOID handle, int bConfigSetting);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBRead(PVOID handle, PVOID pData, DWORD dwLen, DWORD* pLength, DWORD dwMilliseconds);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBWrite(PVOID handle, PVOID pData, DWORD dwLen, DWORD* pLength, DWORD dwMilliseconds);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBReadInt(PVOID handle, PVOID pData, DWORD dwLen, DWORD* pLength, DWORD dwMilliseconds);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]


        static extern bool _MPUSBClose(PVOID handle);

        #endregion
        #region  String Definitions of Pipes and VID_PID
      
        public const string vid_pid_norm = "vid_9610&pid_0020";
              
        public const string out_pipe = "\\MCHP_EP1";
        public const string in_pipe = "\\MCHP_EP1";
             
        internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        static PVOID myOutPipe = INVALID_HANDLE_VALUE;
        static PVOID myInPipe = INVALID_HANDLE_VALUE;

        #endregion

        // Open communication channels to USB module 
        public int OpenPipes()// باز کردن پورت یو اس بی
        {
            DWORD selection = 0; // Selects the device to connect to, in this example it is assumed you will only have one device per vid_pid connected. 

            myOutPipe = _MPUSBOpen(selection, vid_pid_norm, out_pipe, 0, 0);
            myInPipe = _MPUSBOpen(selection, vid_pid_norm, in_pipe, 1, 0);
            if ((myOutPipe == INVALID_HANDLE_VALUE) || (myInPipe == INVALID_HANDLE_VALUE))
            {
                return 0;
            }
            return 1;


        }

        // Close communication channels to USB module 
        public void ClosePipes()// قطع ارتباط با پورت یو اس بی
        {
            _MPUSBClose(myOutPipe);
            _MPUSBClose(myInPipe);
            myInPipe = INVALID_HANDLE_VALUE;
            myOutPipe = INVALID_HANDLE_VALUE;
        }

        //// Gets # of connected USB modules (GW's) 
        //public DWORD GetDeviceCount(string Vid_Pid)
        //{
        //    DWORD count = _MPUSBGetDeviceCount(Vid_Pid);
        //    return count;
        //}


        ////////////////////////////////////////////////////////////
         public void Outprt(byte addr, byte data0)//جهت ارسال یک دیتای مد نظر با هدف
         {
           
                int ADR, DAT;
                byte pointer_o = 0;
                // byte[] OutBuff = new byte[BuffSize - 1], InBuff = new byte[BuffSize - 1];
                byte* OutBuff = stackalloc byte[BuffSize - 1];
                byte* InBuff = stackalloc byte[BuffSize - 1];
                DWORD OutBuffLength;
                ADR = addr;
                DAT = data0;
                if (ADR == P_A) pointer_o = 0x03;
                if (ADR == P_B) pointer_o = 0x04;
                if (ADR == P_C) pointer_o = 0x05;
                if ((ADR == P_P) && (DAT == 0x07)) return;//All Out
                if ((ADR == P_P) && (DAT == 0x04))      //A And B  in , C out
                {
                    OutBuff[0] = 0x06;
                    OutBuff[1] = 0x00;
                    OutBuff[2] = 0x00;
                }
                else
                {
                    OutBuff[0] = 0x02;
                    OutBuff[1] = pointer_o;
                    OutBuff[2] = (byte)DAT;
                }
                _MPUSBWrite(myOutPipe, (PVOID)OutBuff, 3, &OutBuffLength, 1000);
           


         }

         public byte Inprt(byte ADR)// جهت دریافت یک دیتای مد نظر
         {
             byte pointer_i = 0;
             //byte[] OutBuff = new byte[BuffSize - 1], InBuff = new byte[BuffSize - 1];
             byte* OutBuff = stackalloc byte[BuffSize - 1];
             byte* InBuff = stackalloc byte[BuffSize - 1];
             DWORD InBuffLength, OutBuffLength;
             if (ADR == P_A) { pointer_i = 0x03; }
             if (ADR == P_B) { pointer_i = 0x04; }
             if (ADR == P_C) { pointer_i = 0x05; }
             if (ADR == P_P) { Console.WriteLine("ERR 110"); }

             OutBuff[0] = 1;
             OutBuff[1] = pointer_i;// (byte)(Convert.ToByte(ADRR) - 48);
             OutBuff[2] = 0;
             InBuff[0] = 0xFF;
             _MPUSBWrite(myOutPipe, (PVOID)OutBuff, 3, &OutBuffLength, 1000);
             _MPUSBRead(myInPipe, (PVOID)InBuff, 3, &InBuffLength, 1000);
             return (InBuff[0]);
          
         }
         public void SendReceivePacket(byte* SendData, DWORD SendLength, byte* ReceiveData, DWORD* ReceiveLength)//تابع ارسال و دریافت دیتا با پورت یو اس بی
         {
             uint SendDelay = 1000;
             uint ReceiveDelay = 1000;// 1000;
             DWORD SentDataLength;
             DWORD ExpectedReceiveLength = *ReceiveLength;

            _MPUSBWrite(myOutPipe, (PVOID)SendData, SendLength, &SentDataLength, SendDelay) ;
            _MPUSBRead(myInPipe, (PVOID)ReceiveData, ExpectedReceiveLength, ReceiveLength, ReceiveDelay) ;
        }
       public clasmpusbapi()
       {
           //
           // TODO: Add constructor logic here
           //
       }
        /////////////////////////////////////////////////////////////
 

    }
}  
             
                
   