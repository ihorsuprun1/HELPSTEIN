using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HELPSTEIN
{
    class Inventory
    {
       
        public string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");

        public String RAMquerDataTable = "";
        public String CPUquerDataTable = "";
        public String DISKqueryDataTable = "";
        public String OPqueryDataTable = "";
        public String VIDEOqueryDataTable = "";
        public String MOTHERBOARDqueryDataTable = "";
        public String MONITORqueryDataTable = "";
        public String NETWORKSqueryDataTable = "";
        public String PRINTERSqueryDataTable = "";
        public String SOFTqueryDataTable = "";
        public String PROCESSqueryDataTable = "";
        public String SERVICESqueryDataTable = "";
        // public String RAMquery = "";
        // public String CPUquery = "";
        // public String DISKquery = "";
        // public String OPquery = "";
        // public String VIDEOquery = "";
        // public String MOTHERBOARDquery = "";
        // public String MONITORquery = "";
        // public String NETWORKSquery = "";
        // public String PRINTERSquery = "";
        // public String SOFTquery = "";
        // public String PROCESSquery = "";
        // public String SERVICESquery = "";
        public String RAMbody = "";
        public String CPUbody = "";
        public String DISKbody = "";
        public String OPbody = "";
        public String VIDEObody = "";
        public String MOTHERBOARDbody = "";
        public String MONITORbody = "";
        public String NETWORKSbody = "";
        public String PRINTERSbody = "";
        public String SOFTbody = "";
        public String PROCESSbody = "";
        public String SERVICESbody = "";
       
       



        ID ids = new ID();

        DateTime dataTime = DateTime.Now;
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
                try
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
                        //RAMquery += "('" + ids.ids + "','" + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "','" + RAM["BankLabel"] + "','" + Math.Round(System.Convert.ToDouble(RAM["Capacity"]) / 1024 / 1024 / 1024, 2).ToString() + "Gb','" + RAM["Manufacturer"] + "','" + RAM["MemoryType"] + "','" + RAM["PartNumber"] + "','" + RAM["SerialNumber"] + "','" + RAM["Speed"] + "')";
                        //RAMquery += ",";
                        if (RAM["BankLabel"] == null) { RAM["BankLabel"] = ""; };
                        if (RAM["Capacity"] == null) { RAM["Capacity"] = ""; };
                        if (RAM["Speed"] == null) { RAM["Speed"] = ""; };
                        if (RAM["Manufacturer"] == null) { RAM["Manufacturer"] = ""; };
                        if (RAM["MemoryType"] == null) { RAM["MemoryType"] = ""; };
                        if (RAM["PartNumber"] == null) { RAM["PartNumber"] = ""; };
                        if (RAM["SerialNumber"] == null) { RAM["SerialNumber"] = ""; };
                        if (RAM["Speed"] == null) { RAM["Speed"] = ""; };

                        //Наполняем переменную для отправки в DataTable
                        RAMquerDataTable += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + RAM["BankLabel"].ToString().Replace(",", ".").Replace(";", ".") + "," + Math.Round(System.Convert.ToDouble(RAM["Capacity"]) / 1024 / 1024 / 1024, 2).ToString().ToString().Replace(",", ".").Replace(";", ".") + "Gb," + RAM["Manufacturer"].ToString().Replace(",", ".").Replace(";", ".") + "," + RAM["MemoryType"].ToString().Replace(",", ".").Replace(";", ".") + "," + RAM["PartNumber"].ToString().Replace(",", ".").Replace(";", ".") + "," + RAM["SerialNumber"].ToString().Replace(",", ".").Replace(";", ".") + "," + RAM["Speed"].ToString().Replace(",", ".").Replace(";", ".");//+ "," + RAM["Speed"] 
                        RAMquerDataTable += ";";
                    }

                }
              
                catch (Exception ex)
                {
                    try
                    {
                        // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                        //Console.WriteLine(confFile);
                        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("WMI_RAM_INVENTORY_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                            sw.WriteLine("WMI_RAM_INVENTORY_Error  - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                        }

                    }
                    catch
                    {

                    }

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
                try
                {
                    //CPUquery = @"INSERT INTO public.cpus (id, date, device_id, name, number_of_cores, numberof_logical_processors, processor_id, serial_number, max_clock_speed) VALUES ";
                    CPUbody = "Информация о процессоре:";
                    ManagementObjectSearcher searcher_CPU = new ManagementObjectSearcher("root\\CIMV2", "SELECT DeviceID, Name, NumberOfCores, NumberofLogicalProcessors, ProcessorID, SerialNumber, MaxClockSpeed FROM Win32_Processor");
                    foreach (ManagementObject CPU in searcher_CPU.Get())
                    {
                        CPUbody += "\n" + "Name:" + CPU["Name"] + "\n" + "NumberOfCores:" + CPU["NumberOfCores"] + "\n" + "NumberofLogicalProcessors:" + CPU["NumberofLogicalProcessors"] + "\n" + CPU["ProcessorId"] + "\n" + "DeviceID: " + CPU["DeviceID"] + "\n" + "MaxClockSpeed: " + CPU["MaxClockSpeed"] + "\n" + "SerialNumber: " + CPU["SerialNumber"] + "\n";
                        CPUbody += "\n";

                        //Наполняем переменную для отправки в БД
                        //CPUquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + CPU["DeviceID"] + "','" + CPU["Name"] + "','" + CPU["NumberOfCores"] + "','" + CPU["NumberofLogicalProcessors"] + "','" + CPU["ProcessorID"] + "','" + CPU["SerialNumber"] + "','" + CPU["MaxClockSpeed"] + "')";
                        // CPUquery += ",";

                        if (CPU["DeviceID"] == null) { CPU["DeviceID"] = ""; };
                        if (CPU["Name"] == null) { CPU["Name"] = ""; };
                        if (CPU["NumberOfCores"] == null) { CPU["NumberOfCores"] = ""; };
                        if (CPU["NumberofLogicalProcessors"] == null) { CPU["NumberofLogicalProcessors"] = ""; };
                        if (CPU["ProcessorID"] == null) { CPU["ProcessorID"] = ""; };
                        if (CPU["SerialNumber"] == null) { CPU["SerialNumber"] = ""; };
                        if (CPU["MaxClockSpeed"] == null) { CPU["MaxClockSpeed"] = ""; };
                        // Наполняем переменную для отправки в DataTable
                        CPUquerDataTable += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + CPU["DeviceID"].ToString().Replace(",", ".").Replace(";", ".") + "," + CPU["Name"].ToString().Replace(",", ".").Replace(";", ".") + "," + CPU["NumberOfCores"].ToString().Replace(",", ".").Replace(";", ".") + "," + CPU["NumberofLogicalProcessors"].ToString().Replace(",", ".").Replace(";", ".") + "," + CPU["ProcessorID"].ToString().Replace(",", ".").Replace(";", ".") + "," + CPU["SerialNumber"].ToString().Replace(",", ".").Replace(";", ".") + "," + CPU["MaxClockSpeed"].ToString().Replace(",", ".").Replace(";", ".");
                        CPUquerDataTable += ";";

                    }
                    // CPUquery = CPUquery.TrimEnd(CPUquery[CPUquery.Length - 1]);
                    // CPUquery += ";";

                }
                catch (Exception ex)
                {
                    try
                    {
                        // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                        //Console.WriteLine(confFile);
                        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("WMI_CPU_INVENTORY_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                            sw.WriteLine("WMI_CPU_INVENTORY_Error  - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                        }

                    }
                    catch
                    {

                    }

                }
                
                Console.WriteLine("********************* CPU !!!!!!!!!!!!!");
            }
        }

        // Информация о жестких дисках
        public void getDISK(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {
                try
                {
                    ManagementObjectSearcher searcher_hard_disks = new ManagementObjectSearcher("root\\CIMV2", "SELECT DeviceId, InterfaceType, Model, Size, SerialNumber FROM Win32_DiskDrive");

                    DISKbody = "Информация о диске: ";
                    // DISKquery = @"INSERT INTO public.disks (id, date, device_id, interface_type, model, size, serial_number) VALUES ";

                    foreach (ManagementObject hard_disks in searcher_hard_disks.Get())
                    {
                        DISKbody += "\n" + "DeviceID: " + hard_disks["DeviceID"] + "\n" + "InterfaceType: " + hard_disks["InterfaceType"] + "\n" + "Model: " + hard_disks["Model"] + "\n" + "Size:" + Math.Round(System.Convert.ToDouble(hard_disks["Size"]) / 1024 / 1024 / 1024, 2).ToString() + "Gb" + "\n" + "SerialNumber: " + hard_disks["SerialNumber"] + "\n";
                        DISKbody += "\n";

                        //Наполняем переменную для отправки в БД
                        // DISKquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + hard_disks["DeviceID"] + "','" + hard_disks["InterfaceType"] + "','" + hard_disks["Model"] + "','" + Math.Round(System.Convert.ToDouble(hard_disks["Size"]) / 1024 / 1024 / 1024, 2).ToString() + "Gb" + "','" + hard_disks["SerialNumber"] + "')";
                        // DISKquery += ",";
                        if (hard_disks["DeviceID"] == null) { hard_disks["DeviceID"] = ""; };
                        if (hard_disks["InterfaceType"] == null) { hard_disks["InterfaceType"] = ""; };
                        if (hard_disks["Model"] == null) { hard_disks["Model"] = ""; };
                        if (hard_disks["Size"] == null) { hard_disks["Size"] = ""; };
                        if (hard_disks["SerialNumber"] == null) { hard_disks["SerialNumber"] = ""; };
                        // Наполняем переменную для отправки в DataTable
                        DISKqueryDataTable += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + hard_disks["DeviceID"].ToString().Replace(",", ".").Replace(";", ".") + "," + hard_disks["InterfaceType"].ToString().Replace(",", ".").Replace(";", ".") + "," + hard_disks["Model"].ToString().Replace(",", ".").Replace(";", ".") + "," + Math.Round(System.Convert.ToDouble(hard_disks["Size"]) / 1024 / 1024 / 1024, 2).ToString().Replace(",", ".").Replace(";", ".") + "Gb" + "," + hard_disks["SerialNumber"].ToString().Replace(",", ".").Replace(";", ".");
                        DISKqueryDataTable += ";";
                    }
                    // DISKquery = DISKquery.TrimEnd(DISKquery[DISKquery.Length - 1]);
                    //  DISKquery += ";";
                    Console.WriteLine("********************* DISK !!!!!!!!!!!!!");
                    Console.WriteLine(DISKqueryDataTable);

                }

                catch (Exception ex)
                {
                    try
                    {
                        // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                        //Console.WriteLine(confFile);
                        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("WMI_DISK_INVENTORY_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                            sw.WriteLine("WMI_DISK_INVENTORY_Error  - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                        }

                    }
                    catch
                    {

                    }

                }
                
            }
        }
        //Вывод информации о операционной системе, в том числе ее версию, номер сервиспака, количества свободной памяти и многое другое
        public void getOP(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {
                try
                {
                    ManagementObjectSearcher searcher_win_versions = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption, BuildNumber, Version, SerialNumber FROM Win32_OperatingSystem");

                    // OPquery = @"INSERT INTO public.op (id, date, caption, build_number, version, serial_number) VALUES ";
                    OPbody = "Информация о операционной системе: ";
                    foreach (ManagementObject win_versions in searcher_win_versions.Get())
                    {
                        OPbody += "\n" + "Caption: " + win_versions["Caption"] + "\n" + "BuildNumber: " + win_versions["BuildNumber"] + "\n" + "Version: " + win_versions["Version"] + "\n" + "SerialNumber: " + win_versions["SerialNumber"] + "\n";
                        OPbody += "\n";

                        //Наполняем переменную для отправки в БД
                        // OPquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + win_versions["Caption"] + "','" + win_versions["BuildNumber"] + "','" + win_versions["Version"] + "','" + win_versions["SerialNumber"] + "')";
                        // OPquery += ",";

                        if (win_versions["Caption"] == null) { win_versions["Caption"] = ""; };
                        if (win_versions["BuildNumber"] == null) { win_versions["BuildNumber"] = ""; };
                        if (win_versions["SerialNumber"] == null) { win_versions["SerialNumber"] = ""; };



                        // Наполняем переменную для отправки в DataTable
                        OPqueryDataTable += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + win_versions["Caption"].ToString().Replace(",", ".").Replace(";", ".") + "," + win_versions["BuildNumber"].ToString().Replace(",", ".").Replace(";", ".") + "," + win_versions["Version"].ToString().Replace(",", ".") + "," + win_versions["SerialNumber"].ToString().Replace(",", ".").Replace(";", ".");
                        OPqueryDataTable += ";";
                    }
                    // OPquery = OPquery.TrimEnd(OPquery[OPquery.Length - 1]);
                    // OPquery += ";";

                }
                catch (Exception ex)
                {
                    try
                    {
                        // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                        //Console.WriteLine(confFile);
                        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("WMI_OP_INVENTORY_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                            sw.WriteLine("WMI_OP_INVENTORY_Error  - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                        }

                    }
                    catch
                    {

                    }

                }

               
                Console.WriteLine("********************* OP !!!!!!!!!!!!!");

            }
        }

        // Информация о видеокарте
        public void getVIDEO(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {
                try
                {
                    ManagementObjectSearcher searcher_video_cards = new ManagementObjectSearcher("root\\CIMV2", "SELECT AdapterRAM, Caption, Description, VideoProcessor FROM Win32_VideoController");

                    VIDEObody += "Информация о видеокарте: ";
                    //  VIDEOquery += @"INSERT INTO public.video (id, date, adapter_ram, caption, description, video_processor) VALUES ";
                    foreach (ManagementObject video_cards in searcher_video_cards.Get())
                    {
                        VIDEObody += "\n" + "AdapterRAM: " + video_cards["AdapterRAM"] + "\n" + "Caption: " + video_cards["Caption"] + "\n" + "Description: " + video_cards["Description"] + "\n" + "VideoProcessor: " + video_cards["VideoProcessor"] + "\n";
                        VIDEObody += "\n";

                        //Наполняем переменную для отправки в БД
                        // VIDEOquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + video_cards["AdapterRAM"] + "','" + video_cards["Caption"] + "','" + video_cards["Description"] + "','" + video_cards["VideoProcessor"] + "')";
                        // VIDEOquery += ",";
                        //  if (video_cards["AdapterRAM"] == null) { video_cards["AdapterRAM"] = ""; };
                        // if (video_cards["Caption"] == null) { video_cards["Caption"] = ""; };
                        // if (video_cards["Description"] == null) { video_cards["Description"] = ""; };
                        // if (video_cards["VideoProcessor"] == null) { video_cards["VideoProcessor"] = ""; };

                        // Наполняем переменную для отправки в DataTable
                        VIDEOqueryDataTable += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + video_cards["AdapterRAM"] + "," + video_cards["Caption"] + "," + video_cards["Description"] + "," + video_cards["VideoProcessor"];
                        VIDEOqueryDataTable += ";";

                    }

                    // VIDEOquery = VIDEOquery.TrimEnd(VIDEOquery[VIDEOquery.Length - 1]);
                    // VIDEOquery += ";";

                }
                catch (Exception ex)
                {
                    try
                    {
                        // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                        //Console.WriteLine(confFile);
                        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("WMI_VIDEO_INVENTORY_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                            sw.WriteLine("WMI_VIDEO_INVENTORY_Error  - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                        }

                    }
                    catch
                    {

                    }

                }
               
                Console.WriteLine("********************* VIDEO !!!!!!!!!!!!!");
            }
        }

        // Информация о материнской плате
        public void getMOTHERBOARD(Boolean isNeed)
        {
            // ids.getID();
            if (isNeed)
            {
                try
                {
                    // Информация о материнской плате
                    ManagementObjectSearcher searcher_motherboard = new ManagementObjectSearcher("root\\CIMV2", "SELECT Manufacturer, Product, SerialNumber, Version FROM Win32_BaseBoard");
                    MOTHERBOARDbody = "Информация о материнской плате: ";
                    // MOTHERBOARDquery = @"INSERT INTO public.motherboard (id, date, manufacturer, product, serial_number, version) VALUES ";


                    foreach (ManagementObject motherboard in searcher_motherboard.Get())
                    {
                        MOTHERBOARDbody += "\n" + "Manufacturer: " + motherboard["Manufacturer"] + "\n" + "Product: " + motherboard["Product"] + "\n" + "SerialNumber: " + motherboard["SerialNumber"] + "\n" + "Version: " + motherboard["Version"] + "\n";
                        MOTHERBOARDbody += "\n";

                        //Наполняем переменную для отправки в БД
                        //  MOTHERBOARDquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + motherboard["Manufacturer"] + "','" + motherboard["Product"] + "','" + motherboard["SerialNumber"] + "','" + motherboard["Version"] + "')";
                        //  MOTHERBOARDquery += ",";
                        if (motherboard["Manufacturer"] == null) { motherboard["Manufacturer"] = ""; };
                        if (motherboard["Product"] == null) { motherboard["Product"] = ""; };
                        if (motherboard["SerialNumber"] == null) { motherboard["SerialNumber"] = ""; };
                        if (motherboard["Version"] == null) { motherboard["Version"] = ""; };

                        // Наполняем переменную для отправки в DataTable
                        MOTHERBOARDqueryDataTable += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + motherboard["Manufacturer"].ToString().Replace(",", ".").Replace(";", ".") + "," + motherboard["Product"].ToString().Replace(",", ".").Replace(";", ".") + "," + motherboard["SerialNumber"].ToString().Replace(",", ".").Replace(";", ".") + "," + motherboard["Version"].ToString().Replace(",", ".").Replace(";", ".");
                        MOTHERBOARDqueryDataTable += ";";
                    }

                    //  MOTHERBOARDquery = MOTHERBOARDquery.TrimEnd(MOTHERBOARDquery[MOTHERBOARDquery.Length - 1]);
                    //  MOTHERBOARDquery += ";";
                }
                catch (Exception ex)
                {
                    try
                    {
                        // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                        //Console.WriteLine(confFile);
                        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("WMI_MOTHERBOARD_INVENTORY_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                            sw.WriteLine("WMI_MOTHERBOARD_INVENTORY_Error  - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                        }

                    }
                    catch
                    {

                    }

                }

               
                Console.WriteLine("*********************  MOTHERBOARD!!!!!!!!!!!!!");

            }
        }


        public void getNETWORKS(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {
                try
                {
                    //Список сетевых интерфейсов с основными настройками
                    ManagementObjectSearcher searcher_networks = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption, Description, MACAddress, IPAddress FROM Win32_NetworkAdapterConfiguration");

                    NETWORKSbody += "Информация о сетевой плате";
                    // NETWORKSquery += @"INSERT INTO public.networks (id, date, caption, description, mac_address, ip_address) VALUES ";

                    foreach (ManagementObject networks in searcher_networks.Get())
                    {
                        NETWORKSbody += "\n" + "Caption: " + networks["Caption"] + "\n" + "Description: " + networks["Description"] + "\n" + "MACAddress: " + networks["MACAddress"] + "\n" + "IPAddress : " + networks["IPAddress"] + "\n";
                        NETWORKSbody += "\n";

                        //Наполняем переменную для отправки в БД
                        //  NETWORKSquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + networks["Caption"] + "','" + networks["Description"] + "','" + networks["MACAddress"] + "','" + networks["IPAddress"] + "')";
                        //  NETWORKSquery += ",";
                        string IP = "";
                        string[] ipaddr = (string[])networks["IPAddress"];
                        if (ipaddr != null)
                        {
                            foreach (string ipaddress in ipaddr)
                            {
                                IP += ipaddress;
                            }

                        };


                        if (networks["Caption"] == null) { networks["Caption"] = ""; };
                        if (networks["Description"] == null) { networks["Description"] = ""; };
                        if (networks["MACAddress"] == null) { networks["MACAddress"] = ""; };

                        // Наполняем переменную для отправки в DataTable
                        NETWORKSqueryDataTable += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + networks["Caption"].ToString().Replace(",", ".").Replace(";", ".") + "," + networks["Description"].ToString().Replace(",", ".").Replace(";", ".") + "," + networks["MACAddress"].ToString().Replace(",", ".").Replace(";", ".") + "," + IP.Replace(",", ".").Replace(";", ".");//networks["IPAddress"].ToString().Replace(",", ".");
                        NETWORKSqueryDataTable += ";";


                    }

                    // NETWORKSquery = NETWORKSquery.TrimEnd(NETWORKSquery[NETWORKSquery.Length - 1]);
                    // NETWORKSquery += ";";
                }
                catch (Exception ex)
                {
                    try
                    {
                        // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                        //Console.WriteLine(confFile);
                        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("WMI_NETWORKS_INVENTORY_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                            sw.WriteLine("WMI_NETWORKS_INVENTORY_Error  - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                        }

                    }
                    catch
                    {

                    }

                }

                
                Console.WriteLine("********************* NETWORKS !!!!!!!!!!!!!");

            }
        }



        // Информация о мониторе
        public void getMONITOR(Boolean isNeed)
        {
            string result = "";
            int ElementCount = 0;
            int PropertyCount = 0;
            dynamic temp;
            
            //string resultManufacturerName ="";
            //string resultSerialNumberID = "";
            //string resultProductCodeID = "";
            //string resultUserFriendlyName = "";
            string argSmallDelim = ";";
            //ids.getID();
            if (isNeed)
            {

                try
                {
                    ManagementObjectSearcher searcher_monitor = new ManagementObjectSearcher("root\\WMI", "SELECT ManufacturerName,  ProductCodeID,  SerialNumberID, UserFriendlyName FROM WmiMonitorID");

                    MONITORbody += "Информация о мониторе: ";
                    
                    // MONITORquery += @"INSERT INTO public.monitor (id, date, manufacturer_name, product_code_id, serial_number_id, user_friendly_name) VALUES ";
                    foreach (ManagementObject monitor in searcher_monitor.Get())
                    {

                        PropertyCount = 0;
                        ElementCount++;
                        if (ElementCount == 1) { result += ""; }else { result += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + ","; }
                        foreach (PropertyData data in monitor.Properties)
                        {
                            PropertyCount++;
                            //if (PropertyCount == 1) { result += data.Name + ":"; }
                            //else { result += argInternalDelim + data.Name + ":"; }
                            // если попадаются data.value типа UInt16[], то почарово конвертим в строку
                            if (data.Value != null && data.Value.GetType() == typeof(System.UInt16[]))
                            {
                                
                                temp = monitor[data.Name];
                                //if (temp == null) { temp = "not found"; } else
                                //{
                                    for (int i = 0; i <= temp.GetLength(0) - 1; i++)
                                    {
                                        result += Char.ConvertFromUtf32(temp[i]);

                                    }
                                    result += ",";
                                }
                              
                            //}
                            else if (data.Value != null)
                            {
                                result += data.Value.ToString();
                               
                            } else if (data.Value == null)
                            {
                            result +=  "not found";
                                //n = data.Value.ToString();
                            }


                            //result += argInternalDelim; //+ System.Environment.NewLine;
                            //Console.WriteLine(result);
                        }
                        result += argSmallDelim; //+ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + ",";
                        result = result.Replace("\0", "");





                    }
                    MONITORqueryDataTable += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + result; //"," + resultManufacturerName.Replace(@"\","").Replace(@"/","").Replace("0","") + "','" + resultProductCodeID.Replace(@"\", "").Replace(@"/", "").Replace("0", "") + "','" + resultSerialNumberID.Replace(@"\", "").Replace(@"/", "").Replace("0", "") + "','" + resultUserFriendlyName.Replace(@"\", "").Replace(@"/", "").Replace("0", "");
                       // MONITORqueryDataTable += ";";
                    Console.WriteLine("***********************");
                    Console.WriteLine(MONITORqueryDataTable);
                    //MessageBox.Show(MONITORqueryDataTable.ToString());
                    Console.WriteLine("***********************");








                    //try
                    //{
                    //Конвертация в текст из System.UInt16[]
                    //temp = monitor["UserFriendlyName"];
                    //if (temp == null) { resultUserFriendlyName = " "; }
                    //else
                    //{
                    //    for (int i = 0; i <= temp.GetLength(0) - 1; i++)
                    //    {
                    //        resultUserFriendlyName += Char.ConvertFromUtf32(temp[i]);
                    //    }

                    //}

                    //                           //Конвертация в текст из System.UInt16[]
                    //                           temp = monitor["ProductCodeID"];
                    //                       if (temp == null) { resultUserFriendlyName = "not found "; }
                    //                       else
                    //                       {
                    //                           for (int i = 0; i <= temp.GetLength(0) - 1; i++)
                    //                           {
                    //                               resultProductCodeID += Char.ConvertFromUtf32(temp[i]);
                    //                           }

                    //                       }
                    //                       if (temp == null) { resultUserFriendlyName = " "; }
                    //                       else
                    //                       {
                    ////Конвертация в текст из System.UInt16[]
                    //                           temp = monitor["ManufacturerName"];
                    //                           for (int i = 0; i <= temp.GetLength(0) - 1; i++)
                    //                           {
                    //                               resultManufacturerName += Char.ConvertFromUtf32(temp[i]);
                    //                           }
                    //                       }
                    //                       if (temp == null) { resultUserFriendlyName = " "; }
                    //                       else
                    //                       {
                    //                           //Конвертация в текст из System.UInt16[]
                    //                           temp = monitor["SerialNumberID"];
                    //                           for (int i = 0; i <= temp.GetLength(0) - 1; i++)
                    //                           {
                    //                               resultSerialNumberID += Char.ConvertFromUtf32(temp[i]);
                    //                           }

                    //                       }


                    //}
                    //catch
                    //{

                    //}


                    //MONITORbody += "\n" + "ManufacturerName: " + monitor["ManufacturerName"] + "\n" + "ProductCodeID: " + monitor["ProductCodeID"] + "\n" + "SerialNumberID: " + monitor["SerialNumberID"] + "\n" + "UserFriendlyName: " + monitor["UserFriendlyName"] + "\n";
                    //MONITORbody += "\n" + "ManufacturerName: " + resultManufacturerName + "\n" + "ProductCodeID: " + resultProductCodeID + "\n" + "SerialNumberID: " + resultSerialNumberID + "\n" + "UserFriendlyName: " + resultUserFriendlyName + "\n";
                    //MONITORbody += "\n";
                    //Console.WriteLine(MONITORbody);
                    //Наполняем переменную для отправки в БД
                    // MONITORquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + monitor["ManufacturerName"] + "','" + monitor["ProductCodeID"] + "','" + monitor["SerialNumberID"] + "','" + monitor["UserFriendlyName"] + "')";
                    // MONITORquery += ",";
                    //if (monitor["ManufacturerName"] == null) { monitor["ManufacturerName"] = ""; };
                    //if (monitor["ProductCodeID"] == null) { monitor["ProductCodeID"] = ""; };
                    //if (monitor["SerialNumberID"] == null) { monitor["SerialNumberID"] = ""; };
                    //if (monitor["UserFriendlyName"] == null) { monitor["UserFriendlyName"] = ""; };

                    // Наполняем переменную для отправки в DataTable

                   

                    // MONITORquery = MONITORquery.TrimEnd(MONITORquery[MONITORquery.Length - 1]);
                    //  MONITORquery += ";";

                }
                catch (Exception ex)
                {
                    try
                    {
                        // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                        //Console.WriteLine(confFile);
                        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("WMI_MONITOR_INVENTORY_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                            sw.WriteLine("WMI_MONITOR_INVENTORY_Error  - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                        }

                    }
                    catch
                    {

                    }

                }

                Console.WriteLine("*********************  MONITOR!!!!!!!!!!!!!");
            }


        }

        //Информация о принтерах
        public void getPRINTERS(Boolean isNeed)
        {
            //ids.getID();
            if (isNeed)
            {
                try
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
                        if (printer["Name"] == null) { printer["Name"] = ""; };
                        if (printer["Network"] == null) { printer["Network"] = ""; };
                        if (printer["PortName"] == null) { printer["PortName"] = ""; };

                        // Наполняем переменную для отправки в DataTable
                        PRINTERSqueryDataTable += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + printer["Name"].ToString().Replace(",", ".").Replace(";", ".") + "," + printer["Network"].ToString().Replace(",", ".").Replace(";", ".") + "," + printer["PortName"].ToString().Replace(",", ".").Replace(";", ".");
                        PRINTERSqueryDataTable += ";";


                    }

                    // PRINTERSquery = PRINTERSquery.TrimEnd(PRINTERSquery[PRINTERSquery.Length - 1]);
                    // PRINTERSquery += ";";
                }
                catch (Exception ex)
                {
                    try
                    {
                        // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                        //Console.WriteLine(confFile);
                        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("WMI_PRINTERS_INVENTORY_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                            sw.WriteLine("WMI_PRINTERS_INVENTORY_Error  - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                        }

                    }
                    catch
                    {

                    }

                }
                
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

                SOFTbody +=  "Cписок Установленого ПО: ";
               // SOFTquery += @"INSERT INTO public.softs (id, date, caption, install_date) VALUES ";

                foreach (ManagementObject soft in searcher_soft.Get())
                {
                    SOFTbody += "\n" + "Caption: " + soft["Caption"] + "\n" + "InstallDate: " + soft["InstallDate"] + "\n";
                    SOFTbody +=  "\n";

                    //Наполняем переменную для отправки в БД
                    //  SOFTquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + soft["Caption"] + "','" + soft["InstallDate"] + "')";
                    //   SOFTquery += ",";
                    if (soft["Caption"] == null) { soft["Caption"] = ""; };
                    if (soft["InstallDate"] == null) { soft["InstallDate"] = ""; };
                    string softCaption = soft["Caption"].ToString();
                    if (softCaption.Length > 226)
                    { softCaption = softCaption.Remove(softCaption.Length - (softCaption.Length - 226)); }

                    // Наполняем переменную для отправки в DataTable
                    SOFTqueryDataTable += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + softCaption.Replace(",", ".").Replace(";", ".") + "," + soft["InstallDate"].ToString().Replace(",", ".").Replace(";", ".");
                    SOFTqueryDataTable += ";";


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

                PROCESSbody +=  "Cписок запущеных процесов: ";
               // PROCESSquery += @"INSERT INTO public.process (id, date, name, command_line) VALUES ";
                //Ищет запущеные процесы где searcher_proces.Get() - вызывает заданный WMI-запрос и возвращает результирующий набор

                foreach (ManagementObject process in searcher_process.Get())
                {
                    PROCESSbody += "\n" + "Name: " + process["Name"] + "\n" + "CommandLine: " + process["CommandLine"] + "\n";
                    PROCESSbody += "\n";

                    //Наполняем переменную для отправки в БД
                    //  PROCESSquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + process["Name"] + "','" + process["CommandLine"] + "')";
                    //  PROCESSquery += ",";
                    if (process["CommandLine"] == null) { process["CommandLine"] = "" ; };
                    if (process["Name"] == null) { process["Name"] = ""; };
                    string CommandLine = process["CommandLine"].ToString();
                    if (CommandLine.Length > 226)
                    { CommandLine = CommandLine.Remove(CommandLine.Length - (CommandLine.Length - 226)); }

                    // Наполняем переменную для отправки в DataTable
                    PROCESSqueryDataTable += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + process["Name"].ToString().Replace(",", ".").Replace(";", ".") + "," + CommandLine.Replace(",", ".").Replace(";", ".");
                    PROCESSqueryDataTable += ";";

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

                    //Наполняем переменную для отправки в БД
                    //  SERVICESquery += "('" + ids.ids + "','" + dat1.ToString("HH:mm:ss dd.MM.yyyy") + "','" + services["Caption"] + "','" + services["Description"] + "','" + services["DisplayName"] + "','" + services["Name"] + "','" + services["PathName"] + "','" + services["Started"] + "')";
                    //  SERVICESquery += ",";
                    if (services["Caption"] == null) { services["Caption"] = ""; };
                    if (services["Description"] == null) { services["Description"] = ""; };
                    if (services["Name"] == null) { services["Name"] = ""; };
                    if (services["PathName"] == null) { services["PathName"] = ""; };
                    if (services["Started"] == null) { services["Started"] = ""; };

                    string servicesDescription = services["Description"].ToString();
                    if (servicesDescription.Length > 226)
                    { servicesDescription = servicesDescription.Remove(servicesDescription.Length - (servicesDescription.Length - 226)); }
     
                    // Наполняем переменную для отправки в DataTable
                    SERVICESqueryDataTable += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + services["Caption"].ToString().Replace(",", ".").Replace(";", ".") + "," + servicesDescription.Replace(",", ".").Replace(";", ".") + "," + services["DisplayName"].ToString().Replace(",", ".").Replace(";", ".") + "," + services["Name"] + "," + services["PathName"].ToString().Replace(",", ".").Replace(";", ".") + "," + services["Started"].ToString().Replace(",", ".").Replace(";", ".");
                    SERVICESqueryDataTable += ";";

                }


               // SERVICESquery = SERVICESquery.TrimEnd(SERVICESquery[SERVICESquery.Length - 1]);
              //  SERVICESquery += ";";
                Console.WriteLine("********************* service !!!!!!!!!!!!!");

            }


        }
    }
}
 //// *******************************************************************

 //       public string inventory_getInfo(string argRoot, string argQuery, string argBigDelim = constBigDelim, string argSmallDelim = constSmallDelim, string argInternalDelim = constInternalDelim)
 //       {

 //           string result = "";
 //           int ElementCount = 0;
 //           int PropertyCount = 0;
 //           dynamic temp;
 //           string querystr = argRoot + argQuery;
 //           ManagementObjectSearcher searcher = new ManagementObjectSearcher(argRoot, argQuery);
 //           // https://www.mycsharp.de/wbb2/thread.php?postid=3759071
 //           // ^^^^^ немецкий код ))
 //           try
 //           {
 //               foreach (ManagementObject element in searcher.Get())
 //               {
 //                   PropertyCount = 0;
 //                   ElementCount++;
 //                   if (ElementCount == 1) { result += ""; }
 //                   foreach (PropertyData data in element.Properties)
 //                   {
 //                       PropertyCount++;
 //                       //if (PropertyCount == 1) { result += data.Name + ":"; }
 //                       //else { result += argInternalDelim + data.Name + ":"; }
 //                       // если попадаются data.value типа UInt16[], то почарово конвертим в строку
 //                       if (data.Value != null && data.Value.GetType() == typeof(System.UInt16[]))
 //                       {
 //                           //Console.WriteLine("{0}:{1}", data.Name, data.Value);
 //                           temp = element[data.Name];
 //                           for (int i = 0; i <= temp.GetLength(0) - 1; i++)
 //                           {
 //                               result += Char.ConvertFromUtf32(temp[i]);
 //                           }
 //                       }
 //                       else if (data.Value != null)
 //                       {
 //                           result += data.Value.ToString();
 //                           //Console.WriteLine(result);
 //                       }

 //                       //result += argInternalDelim; //+ System.Environment.NewLine;
 //                       //Console.WriteLine(result);
 //                   }
 //                   result += argSmallDelim;
 //                   // result += argBDelim;
 //               }

 //           }
 //           catch (ManagementException)
 //           {
 //               // ПРОВЕРИТЬ, НЕ БУДЕТ ЛИ ОНО ПАРСИТСЯ БАЗОЙ, ВМЕСТО ЗАПИСИ В НЕЁ
 //               result += "*******************!!!ERROR IN WMI QUERY!!!" + querystr;
 //               //throw;
 //           }
 //           result = result.TrimEnd(result[result.Length - 1]);

 //           string res = "," + ElementCount.ToString() + argBigDelim + "'" + result.Replace("\0", "") + "'";
 //           //return res.Replace(argSmallDelim+argBDelim,argBDelim);
            
 //           return res;

 //       }

 //       //**************************************************************