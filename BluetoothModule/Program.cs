using System;
using System.IO;
using InTheHand.Net.Sockets; // e.g. BluetoothDeviceInfo, BluetoothClient, BluetoothListener
using InTheHand.Net.Bluetooth; // e.g. BluetoothService, BluetoothRadio
using System.Linq;

namespace BluetoothModule
{
    class Program
    {
        static void Main(string[] args)
        {
            BluetoothClient client = new BluetoothClient();

            BluetoothDeviceInfo device = null;

            Console.WriteLine(client.PairedDevices);

            foreach (var D in client.PairedDevices)
            {
                Console.WriteLine(D.DeviceName);
                if (D.DeviceName.Contains("boAt"))
                {
                    device = D;
                    break;
                }
            }

            Console.WriteLine( device == null ? "No device available." : device.ClassOfDevice );

            if (device != null)
            {
                foreach (var ser in device.InstalledServices)
                {
                    try
                    {
                        client.Connect(device.DeviceAddress, ser);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(ser);
                    }
                }
                
                Console.WriteLine("Connected...");
            } else
            {
                Console.WriteLine("No device available....");
            }


            client.Dispose();
            client.Close();
            

        }
    }
}