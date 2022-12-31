using System;
using System.Windows.Forms;

namespace ChatbotClientCSharp
{
    // internal class Program
    // {
    //     public static void Main(string[] args)
    //     {
    //         Console.WriteLine("Hello World");
    //         ChatbotForm myForm = new ChatbotForm();
    //         
    //     }
    // }
    static class Exe
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChatbotForm());
        }
    }
}