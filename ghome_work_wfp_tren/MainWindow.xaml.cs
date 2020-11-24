using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace ghome_work_wfp_tren
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
        }
        int counters_time = 0;
        int counters_mistake = 0;  
        private void Timer_Tick(object sender, EventArgs e)
        {
            counters_time++;
            Label_time.Content = counters_time;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (radioButto1.IsChecked == true || radioButto2.IsChecked == true)
            {
                radioButto1.IsEnabled = false;
                radioButto2.IsEnabled = false;
                slider.IsEnabled = false;
                timer.Start();
                text_main.Text = "";
                using (StreamReader fout = new StreamReader(@"C:\Users\Kolotyuk\source\repos\ghome_work_wfp_tren\ghome_work_wfp_tren\text.txt"))
                {

                    for (int i = 0; i < slider.Value; i++)
                    {
                        text_main.Text += fout.ReadLine();
                    }
                }
                progres.Maximum = text_main.Text.Length - 1;
            }
        }

        private void my_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool check = true;


            if (radioButto1.IsChecked == true)
            {
                if (my_text.Text.Length != 0)
                {
                    if (my_text.Text[my_text.Text.Length - 1] != text_main.Text[my_text.Text.Length - 1])
                    {
                        counters_mistake++;
                        count_mistake.Content = counters_mistake;
                        my_text.Foreground = Brushes.Red;
                        check = false;
                    }
                }
            }
            else if (radioButto2.IsChecked == true)
            {
                if (my_text.Text.Length != 0)
                {
                    if (my_text.Text[my_text.Text.Length - 1] - 32 != text_main.Text[my_text.Text.Length - 1] && my_text.Text[my_text.Text.Length - 1] + 32 != text_main.Text[my_text.Text.Length - 1] && my_text.Text[my_text.Text.Length - 1] != text_main.Text[my_text.Text.Length - 1])
                    {
                        counters_mistake++;
                        count_mistake.Content = counters_mistake;
                        my_text.Foreground = Brushes.Red;
                        check = false;
                    }
                }
            }

            if (check)
            {
                progres.Value = my_text.Text.Length;
                my_text.Foreground = Brushes.Green;
                if (my_text.Text.Length == text_main.Text.Length - 1)
                {
                    timer.Stop();
                    MessageBox.Show($"Time: {Label_time.Content} sec\nletters: {my_text.Text.Length} \nmistakes {counters_mistake.ToString()}\naverage speed: { (float)my_text.Text.Length/(int)Label_time.Content }");
                    radioButto1.IsEnabled = true;
                    radioButto2.IsEnabled = true;
                    radioButto1.IsChecked = false;
                    radioButto2.IsChecked = false;
                    slider.IsEnabled = true;
                    slider.Value = 0;
                    my_text.Text = "";
                    text_main.Text = "";
                    Label_time.Content = 0;
                    count_mistake.Content = 0;
                    counters_time = 0;
                    counters_mistake = 0;
        
                }
            }



        }
    }
}

