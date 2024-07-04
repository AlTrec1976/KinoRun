using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System;

namespace KinoRun
{
    public class FileAssociations
    {
        private const string FILE_EXTENSION = ".knr";
        private const long SHCNE_ASSOCCHANGED = 0x8000000L;
        private const uint SHCNF_IDLIST = 0x0U;

        public static void Associate()
        {
            string description = "Файл раздачи KinoRun";
            string icon = Application.StartupPath + @"\KinoRun.exe";
            try
            {
                Registry.ClassesRoot.CreateSubKey(FILE_EXTENSION).SetValue("", Application.ProductName);

                if (Application.ProductName != null && Application.ProductName.Length > 0)
                {
                    using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(Application.ProductName, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        if (description != null)
                            key.SetValue("", description);

                        if (icon != null)
                            key.CreateSubKey("DefaultIcon").SetValue("", ToShortPathName(icon));

                        key.CreateSubKey(@"Shell\Open\Command").SetValue("", ToShortPathName(Application.ExecutablePath) + " \"%1\"");
                    }
                }

                SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);

                if (frmMain.access == "")
                {
                    MessageBox.Show(
                        "Файлы *.knr теперь будут открываться в данном приложении",
                        "Успешно!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    frmMain.access = "";
                }

            }
            catch (UnauthorizedAccessException ex)
            {
                //var msg = MessageBox.Show(ex.Message + "\nЗакройте программу и запустите вновь, но от имени администратора", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool IsAssociated
        {
            get { return (Registry.ClassesRoot.OpenSubKey(FILE_EXTENSION, false) != null); }
        }

        public static void Remove()
        {
            try
            {
                var msg = MessageBox.Show("Вы уверены, что хотите отменить ассоциацию?\nПосле этих действий Вы уже не сможете открывать файлы\nраздач в данном приложении из проводника!", "Успешно!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (msg == DialogResult.Yes)
                {
                    Registry.ClassesRoot.DeleteSubKeyTree(FILE_EXTENSION, false);
                    Registry.ClassesRoot.DeleteSubKeyTree(Application.ProductName, false);
                    if (!IsAssociated)
                        msg = MessageBox.Show("Ассоциация файлов *.knr с данным приложением отменена", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        //msg = MessageBox.Show("Недостаточно прав для выполнения данной операции.\nСейчас программа перезапустится с расширенными правами.", //Закройте программу и запустите вновь, но от имени администратора",
                        //    "Ошибка",
                        //    MessageBoxButtons.OK, 
                        //    MessageBoxIcon.Error);
                    }
                }
            }
            catch(UnauthorizedAccessException ex)
            {
                //var msg = MessageBox.Show(ex.Message + "\nЗакройте программу и запустите вновь, но от имени администратора", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public static void sRemove()
        {
            try
            {
                Registry.ClassesRoot.DeleteSubKeyTree(FILE_EXTENSION, false);
                Registry.ClassesRoot.DeleteSubKeyTree(Application.ProductName, false);
                if (!IsAssociated)
                    MessageBox.Show("Ассоциация файлов *.knr с данным приложением отменена", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmMain.access = "";
            }
            catch (UnauthorizedAccessException ex)
            {
                var msg = MessageBox.Show(ex.Message + "\nЗакройте программу и запустите вновь, но от имени администратора", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        [DllImport("shell32.dll", SetLastError = true)]
        private static extern void SHChangeNotify(long wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        [DllImport("Kernel32.dll")]
        private static extern uint GetShortPathName(string lpszLongPath, [Out]StringBuilder lpszShortPath, uint cchBuffer);

        private static string ToShortPathName(string longName)
        {
            StringBuilder s = new StringBuilder(1000);
            uint iSize = (uint)s.Capacity;
            uint iRet = GetShortPathName(longName, s, iSize);
            return s.ToString();
        }
    }
}
