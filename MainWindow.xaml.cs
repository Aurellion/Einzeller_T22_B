using System;
using System.Collections.Generic;
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

namespace Einzeller
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Zellen> Zoo = new List<Zellen>();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Start();
            timer.Tick += animate;

            
            for (int i = 0; i < 30; i++)
            {
                Zoo.Add(new Bakterie());
            }

            for (int i = 0; i < 20; i++)
            {
                Zoo.Add(new Amöbe());
            }

            
        }

        private void animate(object sender, EventArgs e)
        {
            Petrischale.Children.Clear();
            List<Zellen> Kinder = new List<Zellen>();
            List<Zellen> Tote = new List<Zellen>();
            foreach (Zellen item in Zoo)
            {
                if (!Tote.Contains(item))
                {
                    item.Draw(Petrischale);
                    item.Move();
                    Kinder.AddRange(item.Teilen());
                    Tote.AddRange(item.Fressen(Zoo, Tote));
                    Tote.AddRange(item.Sterben());
                }
            }
            Zoo.AddRange(Kinder);
            //foreach (Zellen item in Tote)
            //{
            //    Zoo.Remove(item);
            //}
            Zoo.RemoveAll(x => Tote.Contains(x));
           

        }
    }
}
