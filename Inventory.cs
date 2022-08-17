using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace HELPSTEIN_Frida
{
    class Inventory
    {
       // public String RAMquery = "";
        public String RAMbody = "";
      //  public String CPUquery = "";
        public String CPUbody = "";
        public String DISKbody = "";
       // public String DISKquery = "";
        public String OPbody = "";
       // public String OPquery = "";
        public String VIDEObody = "";
       // public String VIDEOquery = "";
        public String MOTHERBOARDbody = "";
     //   public String MOTHERBOARDquery = "";
        public String MONITORbody = "";
     //   public String MONITORquery = "";
        public String NETWORKSbody = "";
      //  public String NETWORKSquery = "";
        public String PRINTERSbody = "";
      //  public String PRINTERSquery = "";
        public String SOFTbody = "";
     //   public String SOFTquery = "";
        public String PROCESSbody = "";
     //  public String PROCESSquery = "";
        public String SERVICESbody = "";
      //  public String SERVICESquery = "";
        //    private static string[,] WMIqueries = new string[,]
        //    {   { "root\\CIMV2", "SELECT Caption, BuildNumber, Version, SerialNumber FROM Win32_OperatingSystem" },
        //        { "root\\CIMV2", "SELECT DeviceID, Name, NumberOfCores, NumberofLogicalProcessors, ProcessorID, SerialNumber, MaxClockSpeed FROM Win32_Processor" },
        //        { "root\\CIMV2", "SELECT Manufacturer, Product, SerialNumber, Version FROM Win32_BaseBoard" },
        //        { "root\\CIMV2", "SELECT BankLabel, Capacity, Manufacturer, MemoryType, PartNumber, SerialNumber, Speed FROM Win32_PhysicalMemory" },
        //        { "root\\CIMV2", "SELECT AdapterRAM, Caption, Description, VideoProcessor FROM Win32_VideoController" },
        //        { "root\\CIMV2", "SELECT DeviceId, InterfaceType, Model, Size, SerialNumber  FROM Win32_DiskDrive" },
        //        { "root\\WMI", "SELECT ManufacturerName,  ProductCodeID,  SerialNumberID, UserFriendlyName FROM WmiMonitorID" },
        //        { "root\\CIMV2", "SELECT Name, Network, PortName from Win32_Printer"},
        //        { "root\\CIMV2", "SELECT Caption, Description, MACAddress, IPAddress FROM Win32_NetworkAdapterConfiguration" },
        //        { "root\\CIMV2", "SELECT * FROM Win32_Product"},
        //    };
        //ID ids = new ID();

        DateTime dat1 = DateTime.Now;
        //string strdate = dat1.ToString("yyyy.MM.dd HH:mm:ss");
        //Информация о оперативной памяти !! getRamOption
        // 1. мы должны проверить доступность БД
        // 2. если БД есть, ко вытягиваем ИЗ БД опции, и записываем их в ИНИ
        // 3. читаем настройки из ИНИ, и в зависимости от настроек, инвентаризируем, или нет


        //Информация о оперативной памяти
        public void getRam(Boolean isNeed)

        {
           // ids.getID();

            if (isNeed)
            {
                //Информация о оперативной памяти
                ManagementObjectSearcher searcher_RAM = new ManagementObjectSearcher("root\\CIMV2", "SELECT BankLabel, Capacity, Manufacturer, MemoryType, PartNumber, SerialNumber, Speed FROM Win32_PhysicalMemory");
                //переменная запроса для базы данных
               // RAMquery = @"INSERT INTO public.rams (id, date, bank_label, capacity, manufacturer, memory_type, part_number, serial_number, speed) VALUES ";
                RAMbody = "Информация об Оперативной памяте:";
                foreach (ManagementObject RAM in searcher_RAM.Get())
                {
                    //Наполняем переменную для отправки по почте
                    RAMbody += "\n" + "BankLabel: " + RAM["BankLabel"] + "\n" + "Capacity: " + Math.Round(System.Convert.ToDouble(RAM["Capacity"]) / 1024 / 1024 / 1024, 2).ToString() + "Gb" + "\n" + "Speed:" + RAM["Speed"] + "\n";
                    RAMbody += "\n";
                    //Наполняем переменную для отправки в БД
                    //RAMquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + RAM["BankLabel"] + "','" + Math.Round(System.Convert.ToDouble(RAM["Capacity"]) / 1024 / 1024 / 1024, 2).ToString() + "Gb','" + RAM["Manufacturer"] + "','" + RAM["MemoryType"] + "','" + RAM["PartNumber"] + "','" + RAM["SerialNumber"] + "','" + RAM["Speed"] + "')";
                    //RAMquery += ",";
                }
                // Удаляем последний знак в переменной
                //RAMquery = RAMquery.TrimEnd(RAMquery[RAMquery.Length - 1]);
                //// добавляем нужный разделитель
                //RAMquery += ";";
                Console.WriteLine("********************* RAM !!!!!!!!!!!!!");

            }
        }


        //Информация о процессоре
        public void getCPU(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {
                //CPUquery = @"INSERT INTO public.cpus (id, date, device_id, name, number_of_cores, numberof_logical_processors, processor_id, serial_number, max_clock_speed) VALUES ";
                CPUbody = "Информация о процессоре:";
                ManagementObjectSearcher searcher_CPU = new ManagementObjectSearcher("root\\CIMV2", "SELECT DeviceID, Name, NumberOfCores, NumberofLogicalProcessors, ProcessorID, SerialNumber, MaxClockSpeed FROM Win32_Processor");
                foreach (ManagementObject CPU in searcher_CPU.Get())
                {
                    CPUbody += "\n" + "Name:" + CPU["Name"] + "\n" + "NumberOfCores:" + CPU["NumberOfCores"] + "\n" + "NumberofLogicalProcessors:" + CPU["NumberofLogicalProcessors"] + "\n" + CPU["ProcessorId"] + "\n" + "DeviceID: " + CPU["DeviceID"] + "\n" + "MaxClockSpeed: " + CPU["MaxClockSpeed"] + "\n" + "SerialNumber: " + CPU["SerialNumber"] + "\n";
                    CPUbody += "\n";
                    //CPUquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + CPU["DeviceID"] + "','" + CPU["Name"] + "','" + CPU["NumberOfCores"] + "','" + CPU["NumberofLogicalProcessors"] + "','" + CPU["ProcessorID"] + "','" + CPU["SerialNumber"] + "','" + CPU["MaxClockSpeed"] + "')";
                   // CPUquery += ",";

                }
               // CPUquery = CPUquery.TrimEnd(CPUquery[CPUquery.Length - 1]);
               // CPUquery += ";";
                Console.WriteLine("********************* CPU !!!!!!!!!!!!!");
            }
        }

        // Информация о жестких дисках
        public void getDISK(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {
                ManagementObjectSearcher searcher_hard_disks = new ManagementObjectSearcher("root\\CIMV2", "SELECT DeviceId, InterfaceType, Model, Size, SerialNumber FROM Win32_DiskDrive");

                DISKbody = "Информация о диске: ";
               // DISKquery = @"INSERT INTO public.disks (id, date, device_id, interface_type, model, size, serial_number) VALUES ";

                foreach (ManagementObject hard_disks in searcher_hard_disks.Get())
                {
                    DISKbody += "\n" + "DeviceID: " + hard_disks["DeviceID"] + "\n" + "InterfaceType: " + hard_disks["InterfaceType"] + "\n" + "Model: " + hard_disks["Model"] + "\n" + "Size:" + Math.Round(System.Convert.ToDouble(hard_disks["Size"]) / 1024 / 1024 / 1024, 2).ToString() + "Gb" + "\n" + "SerialNumber: " + hard_disks["SerialNumber"] + "\n";
                    DISKbody += "\n";
                   // DISKquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + hard_disks["DeviceID"] + "','" + hard_disks["InterfaceType"] + "','" + hard_disks["Model"] + "','" + Math.Round(System.Convert.ToDouble(hard_disks["Size"]) / 1024 / 1024 / 1024, 2).ToString() + "Gb" + "','" + hard_disks["SerialNumber"] + "')";
                   // DISKquery += ",";

                }
               // DISKquery = DISKquery.TrimEnd(DISKquery[DISKquery.Length - 1]);
              //  DISKquery += ";";
                Console.WriteLine("********************* DISK !!!!!!!!!!!!!");
            }
        }
        //Вывод информации о операционной системе, в том числе ее версию, номер сервиспака, количества свободной памяти и многое другое
        public void getOP(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {
                ManagementObjectSearcher searcher_win_versions = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption, BuildNumber, Version, SerialNumber FROM Win32_OperatingSystem");

               // OPquery = @"INSERT INTO public.op (id, date, caption, build_number, version, serial_number) VALUES ";
                OPbody = "Информация о операционной системе: ";
                foreach (ManagementObject win_versions in searcher_win_versions.Get())
                {
                    OPbody += "\n" + "Caption: " + win_versions["Caption"] + "\n" + "BuildNumber: " + win_versions["BuildNumber"] + "\n" + "Version: " + win_versions["Version"] + "\n" + "SerialNumber: " + win_versions["SerialNumber"] + "\n";
                    OPbody += "\n";

                   // OPquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + win_versions["Caption"] + "','" + win_versions["BuildNumber"] + "','" + win_versions["Version"] + "','" + win_versions["SerialNumber"] + "')";
                   // OPquery += ",";

                }
               // OPquery = OPquery.TrimEnd(OPquery[OPquery.Length - 1]);
               // OPquery += ";";
                Console.WriteLine("********************* OP !!!!!!!!!!!!!");

            }
        }

        // Информация о видеокарте
        public void getVIDEO(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {
                ManagementObjectSearcher searcher_video_cards = new ManagementObjectSearcher("root\\CIMV2", "SELECT AdapterRAM, Caption, Description, VideoProcessor FROM Win32_VideoController");

                VIDEObody += "Информация о видеокарте: ";
              //  VIDEOquery += @"INSERT INTO public.video (id, date, adapter_ram, caption, description, video_processor) VALUES ";
                foreach (ManagementObject video_cards in searcher_video_cards.Get())
                {
                    VIDEObody += "\n" + "AdapterRAM: " + video_cards["AdapterRAM"] + "\n" + "Caption: " + video_cards["Caption"] + "\n" + "Description: " + video_cards["Description"] + "\n" + "VideoProcessor: " + video_cards["VideoProcessor"] + "\n";
                    VIDEObody += "\n";

                   // VIDEOquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + video_cards["AdapterRAM"] + "','" + video_cards["Caption"] + "','" + video_cards["Description"] + "','" + video_cards["VideoProcessor"] + "')";
                   // VIDEOquery += ",";

                }

               // VIDEOquery = VIDEOquery.TrimEnd(VIDEOquery[VIDEOquery.Length - 1]);
               // VIDEOquery += ";";
                Console.WriteLine("********************* VIDEO !!!!!!!!!!!!!");
            }
        }

        // Информация о материнской плате
        public void getMOTHERBOARD(Boolean isNeed)
        {
            // ids.getID();
            if (isNeed)
            {
                // Информация о материнской плате
                ManagementObjectSearcher searcher_motherboard = new ManagementObjectSearcher("root\\CIMV2", "SELECT Manufacturer, Product, SerialNumber, Version FROM Win32_BaseBoard");
                MOTHERBOARDbody = "Информация о материнской плате: ";
               // MOTHERBOARDquery = @"INSERT INTO public.motherboard (id, date, manufacturer, product, serial_number, version) VALUES ";


                foreach (ManagementObject motherboard in searcher_motherboard.Get())
                {
                    MOTHERBOARDbody += "\n" + "Manufacturer: " + motherboard["Manufacturer"] + "\n" + "Product: " + motherboard["Product"] + "\n" + "SerialNumber: " + motherboard["SerialNumber"] + "\n" + "Version: " + motherboard["Version"] + "\n";
                    MOTHERBOARDbody += "\n";

                  //  MOTHERBOARDquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + motherboard["Manufacturer"] + "','" + motherboard["Product"] + "','" + motherboard["SerialNumber"] + "','" + motherboard["Version"] + "')";
                  //  MOTHERBOARDquery += ",";
                }

              //  MOTHERBOARDquery = MOTHERBOARDquery.TrimEnd(MOTHERBOARDquery[MOTHERBOARDquery.Length - 1]);
              //  MOTHERBOARDquery += ";";
                Console.WriteLine("*********************  MOTHERBOARD!!!!!!!!!!!!!");

            }
        }


        public void getNETWORKS(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {

                //Список сетевых интерфейсов с основными настройками
                ManagementObjectSearcher searcher_networks = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption, Description, MACAddress, IPAddress FROM Win32_NetworkAdapterConfiguration");

                NETWORKSbody += "Информация о сетевой плате";
                // NETWORKSquery += @"INSERT INTO public.networks (id, date, caption, description, mac_address, ip_address) VALUES ";

                foreach (ManagementObject networks in searcher_networks.Get())
                {
                    NETWORKSbody += "\n" + "Caption: " + networks["Caption"] + "\n" + "Description: " + networks["Description"] + "\n" + "MACAddress: " + networks["MACAddress"] + "\n" + "IPAddress : " + networks["IPAddress"] + "\n";
                    NETWORKSbody += "\n";

                    //  NETWORKSquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + networks["Caption"] + "','" + networks["Description"] + "','" + networks["MACAddress"] + "','" + networks["IPAddress"] + "')";
                    //  NETWORKSquery += ",";


                }

                // NETWORKSquery = NETWORKSquery.TrimEnd(NETWORKSquery[NETWORKSquery.Length - 1]);
                // NETWORKSquery += ";";
                Console.WriteLine("********************* NETWORKS !!!!!!!!!!!!!");

            }
        }



        // Информация о мониторе
        public void getMONITOR(Boolean isNeed)
        {
            //int ElementCount = 0;
            //int PropertyCount = 0;
            dynamic temp;
            string resultManufacturerName ="";
            string resultSerialNumberID = "";
            string resultProductCodeID = "";
            string resultUserFriendlyName = "";
            //string result = "";
            //ids.getID();
            if (isNeed)
            {
                ManagementObjectSearcher searcher_monitor = new ManagementObjectSearcher("root\\WMI", "SELECT ManufacturerName,  ProductCodeID,  SerialNumberID, UserFriendlyName FROM WmiMonitorID");

                MONITORbody += "Информация о мониторе: ";
               // MONITORquery += @"INSERT INTO public.monitor (id, date, manufacturer_name, product_code_id, serial_number_id, user_friendly_name) VALUES ";
                foreach (ManagementObject monitor in searcher_monitor.Get())
                {
                    //Конвертация в текст из System.UInt16[]
                    temp = monitor["UserFriendlyName"];
                        for (int i = 0; i <= temp.GetLength(0) - 1; i++)
                    {
                        resultUserFriendlyName += Char.ConvertFromUtf32(temp[i]);
                    }
                    //Конвертация в текст из System.UInt16[]
                    temp = monitor["ProductCodeID"];
                    for (int i = 0; i <= temp.GetLength(0) - 1; i++)
                    {
                        resultProductCodeID += Char.ConvertFromUtf32(temp[i]);
                    }
                    //Конвертация в текст из System.UInt16[]
                    temp = monitor["ManufacturerName"];
                    for (int i = 0; i <= temp.GetLength(0) - 1; i++)
                    {
                        resultManufacturerName += Char.ConvertFromUtf32(temp[i]);
                    }
                    //Конвертация в текст из System.UInt16[]
                    temp = monitor["SerialNumberID"];
                    for (int i = 0; i <= temp.GetLength(0) - 1; i++)
                    {
                        resultSerialNumberID += Char.ConvertFromUtf32(temp[i]);
                    }

                    //MONITORbody += "\n" + "ManufacturerName: " + monitor["ManufacturerName"] + "\n" + "ProductCodeID: " + monitor["ProductCodeID"] + "\n" + "SerialNumberID: " + monitor["SerialNumberID"] + "\n" + "UserFriendlyName: " + monitor["UserFriendlyName"] + "\n";
                    MONITORbody += "\n" + "ManufacturerName: " + resultManufacturerName + "\n" + "ProductCodeID: " + resultProductCodeID + "\n" + "SerialNumberID: " + resultSerialNumberID + "\n" + "UserFriendlyName: " + resultUserFriendlyName + "\n";
                    MONITORbody += "\n";

                   // MONITORquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + monitor["ManufacturerName"] + "','" + monitor["ProductCodeID"] + "','" + monitor["SerialNumberID"] + "','" + monitor["UserFriendlyName"] + "')";
                   // MONITORquery += ",";
                }


               // MONITORquery = MONITORquery.TrimEnd(MONITORquery[MONITORquery.Length - 1]);
              //  MONITORquery += ";";
                Console.WriteLine("*********************  MONITOR!!!!!!!!!!!!!");
            }


        }

        //Информация о принтерах
        public void getPRINTERS(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {
                ManagementObjectSearcher searcher_printer = new ManagementObjectSearcher("root\\CIMV2", "SELECT Name, Network, PortName from Win32_Printer");

                PRINTERSbody += "Информация о принтерах: ";
               // PRINTERSquery += @"INSERT INTO public.printer (id, date, name, network, port_name) VALUES ";

                foreach (ManagementObject printer in searcher_printer.Get())
                {
                    PRINTERSbody += "\n" + "Name: " + printer["Name"] + "\n" + "Network: " + printer["Network"] + "\n" + "PortName: " + printer["PortName"] + "\n";
                    PRINTERSbody += "\n";

                  //  PRINTERSquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + printer["Name"] + "','" + printer["Network"] + "','" + printer["PortName"] + "')";
                  //  PRINTERSquery += ",";
                }

               // PRINTERSquery = PRINTERSquery.TrimEnd(PRINTERSquery[PRINTERSquery.Length - 1]);
               // PRINTERSquery += ";";
                Console.WriteLine("********************* PRINTERS !!!!!!!!!!!!!");


            }


        }


        //Получаем список Установленого ПО и дату установки
        public void getSOFT(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {
                //Получаем список Установленого ПО и дату установки
                ManagementObjectSearcher searcher_soft = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption, InstallDate FROM Win32_Product");

                SOFTbody += "Cписок Установленого ПО: ";
               // SOFTquery += @"INSERT INTO public.softs (id, date, caption, install_date) VALUES ";

                foreach (ManagementObject soft in searcher_soft.Get())
                {
                    SOFTbody += "\n" + "Caption: " + soft["Caption"] + "\n" + "InstallDate: " + soft["InstallDate"] + "\n";
                    SOFTbody += "\n";

                  //  SOFTquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + soft["Caption"] + "','" + soft["InstallDate"] + "')";
                 //   SOFTquery += ",";


                }

               // SOFTquery = SOFTquery.TrimEnd(SOFTquery[SOFTquery.Length - 1]);
               // SOFTquery += ";";
                Console.WriteLine("********************* SOFTS !!!!!!!!!!!!!");
            }




        }

        //Получаем список запущеных процесов
        public void getPROCESS(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {

                ManagementObjectSearcher searcher_process = new ManagementObjectSearcher("root\\CIMV2", "Select Name, CommandLine From Win32_Process");

                PROCESSbody += "Cписок запущеных процесов: ";
               // PROCESSquery += @"INSERT INTO public.process (id, date, name, command_line) VALUES ";
                //Ищет запущеные процесы где searcher_proces.Get() - вызывает заданный WMI-запрос и возвращает результирующий набор

                foreach (ManagementObject process in searcher_process.Get())
                {
                    PROCESSbody += "\n" + "Name: " + process["Name"] + "\n" + "CommandLine: " + process["CommandLine"] + "\n";
                    PROCESSbody += "\n";

                  //  PROCESSquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + process["Name"] + "','" + process["CommandLine"] + "')";
                  //  PROCESSquery += ",";
                }

               // PROCESSquery = PROCESSquery.TrimEnd(PROCESSquery[PROCESSquery.Length - 1]);
               // PROCESSquery += ";";
                Console.WriteLine("********************* PROCESS !!!!!!!!!!!!!");
            }



        }


        //Получаем список сервисов
        public void getSERVICES(Boolean isNeed)
        {
            if (isNeed)
            {

                ManagementObjectSearcher searcher_services = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Service");

                SERVICESbody += "Cписок запущеных сервисов: ";
               // SERVICESquery += @"INSERT INTO public.servicese (id, date, caption, description, display_name, name, path_name, started ) VALUES ";


                foreach (ManagementObject services in searcher_services.Get())
                {

                    SERVICESbody += "\n" + "Caption: " + services["Caption"] + "\n" + "Description: " + services["Description"] + "\n" + "DisplayName: " + services["DisplayName"] + "\n" + "Name: " + services["Name"] + "\n" + "\n" + "PathName: " + services["PathName"] + "\n" + "Started: " + services["Started"] + "\n";
                    SERVICESbody += "\n";

                  //  SERVICESquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + services["Caption"] + "','" + services["Description"] + "','" + services["DisplayName"] + "','" + services["Name"] + "','" + services["PathName"] + "','" + services["Started"] + "')";
                  //  SERVICESquery += ",";


                }


               // SERVICESquery = SERVICESquery.TrimEnd(SERVICESquery[SERVICESquery.Length - 1]);
              //  SERVICESquery += ";";
                Console.WriteLine("********************* service !!!!!!!!!!!!!");

            }

        }
    }
}
