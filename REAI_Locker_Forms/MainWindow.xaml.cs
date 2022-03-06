using LockerLib;
using REAI_Locker_Forms.FormUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace REAI_Locker_Forms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string generationSecretCode = string.Empty;
        

        public MainWindow()
        {
            InitializeComponent();
            
            generationSecretCode = ConfigurationUtil.GetAppConfig("generationSecretKey");
            Console.WriteLine("==========");
            Console.WriteLine(generationSecretCode);
            Console.WriteLine("==========");
        }

        //
        //

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (generationSecretCode == null || generationSecretCode == String.Empty)
            {
                generationSecretCode = (generationSecretCode == string.Empty || generationSecretCode == null) ? OTPUtil.GetBase32Secret() : generationSecretCode;

                secretKey.Text = generationSecretCode;
            }
            else
            {
                infoMessage1.Text = "인증키를 입력하세요.";
                infoMessage2.Visibility = Visibility.Hidden;
                SaveButton.Content = "인증";
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show(SaveButton.Content.ToString());
            if (SaveButton.Content.ToString() == "인증")
            {
                if (OTPUtil.checkOTP(generationSecretCode, secretKey.Text))
                    Environment.Exit(0);
                else
                    MessageBox.Show("올바른 OTP 코드를 입력하세요.\nSecret키를 잘못 설정 하셨을 확률도 있습니다.\n그런 경우엔 관리자에게 문의 하세요.","알림");
            }
            else
            {
                ConfigurationUtil.SetAppConfig("generationSecretKey", generationSecretCode);
            }
        }
    }
}
