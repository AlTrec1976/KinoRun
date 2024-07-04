using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;

namespace KinoRun
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (args[0] == "a")
                {
                    WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                    bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);

                    if (!hasAdministrativeRight)
                    {
                        ProcessStartInfo processInfo = new ProcessStartInfo(); //создаем новый процесс
                        processInfo.Verb = "runas"; //в данном случае указываем, что процесс должен быть запущен с правами администратора
                        processInfo.Arguments = "a"; //в данном случае указываем, что процесс должен быть запущен с правами администратора
                        processInfo.FileName = Application.ExecutablePath; //указываем исполняемый файл (программу) для запуска
                        try
                        {
                            Process.Start(processInfo); //пытаемся запустить процесс
                        }
                        catch (Win32Exception)
                        {
                            //Ничего не делаем
                        }
                        Application.Exit(); //закрываем текущую копию программы (в любом случае, даже если пользователь отменил запуск с правами администратора в окне UAC)
                    }
                    else //имеем права администратора, значит, стартуем
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        //Application.Run(new frmMain());
                        try
                        {
                            Application.Run(new frmMain(args[0]));
                        }
                        catch (Exception)
                        {
                            Application.Run(new frmMain(""));
                        }
                    }
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //Application.Run(new frmMain());
                    try
                    {
                        Application.Run(new frmMain(args[0]));
                    }
                    catch (Exception)
                    {
                        Application.Run(new frmMain(""));
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                try
                {

                    Application.Run(new frmMain(args[0]));
                }
                catch (Exception)
                {
                    Application.Run(new frmMain(""));
                }
            }
        }
    }
}