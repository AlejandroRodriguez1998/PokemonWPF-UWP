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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PokemonIPO
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer reloj = new DispatcherTimer();
        Storyboard rojoDolor;
        Storyboard resucitar;
        Storyboard contento;
        Storyboard muerte;
        Storyboard ataqueAnimacion;

        public MainWindow()
        {
            InitializeComponent();
            flotar();
            this.muerte = (Storyboard)this.Resources["morir"];
            this.rojoDolor = (Storyboard)this.mitadCuerpo.Resources["dolorKey"];
            this.resucitar = (Storyboard)this.Resources["revivir"];
            this.contento = (Storyboard)this.Resources["contento"];
            this.ataqueAnimacion = (Storyboard)this.Resources["atacar"];

        }

        private void flotar()
        {
            DoubleAnimation flotarCuerpo = new DoubleAnimation();
            flotarCuerpo.From = this.pokemon.Height;
            flotarCuerpo.To = 350.0;
            flotarCuerpo.AutoReverse = true;
            flotarCuerpo.Duration = new Duration(TimeSpan.FromSeconds(1));
            flotarCuerpo.RepeatBehavior = RepeatBehavior.Forever;
            this.pokemon.BeginAnimation(Grid.HeightProperty, flotarCuerpo);
        }

        private void dolor()
        {
            this.rojoDolor.Begin();
        }

        private void morir()
        {
            this.resucitar.Stop();
            this.muerte.Begin();
        }

        private void revivir()
        {
            this.muerte.Stop();
            this.resucitar.Begin();
        }


        private void pocion_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.reloj.IsEnabled)
            {
                this.reloj.Interval = TimeSpan.FromMilliseconds(100);
                this.reloj.Tick += new EventHandler(incrementarBarra);
                this.reloj.Start();
                this.contento.Begin();
                this.pocion.Opacity = 0.5;
            }
        }

        private void incrementarBarra(object sender, EventArgs e)
        {
            if (this.barraVida.Value == 0)
            {
                revivir();
            }

            this.barraVida.Value += 0.2;
            if (this.barraVida.Value >= 100)
            {
                this.reloj.Stop();
                this.contento.Stop();
                this.pocion.Opacity = 0.0;
            }
        }

        private void pokemon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.reloj.IsEnabled)
            {
                this.reloj.Stop();
                this.contento.Stop();
                this.pocion.Opacity = 1;
            }

            var semilla = Environment.TickCount;
            var random = new Random(semilla);

            var valorEscudo = random.Next(1, 13);
            var valorVida = random.Next(1, 4);

            this.barraVida.Value -= valorVida;
           
            if (this.barraVida.Value <= 99)
            {
                this.pocion.Opacity = 1;
            }

            if(this.barraEscudo.Value <= 99)
            {
                this.pocionEscudo.Opacity = 1;
            }

            if (this.barraVida.Value == 0)
            {
                morir();
            }
            else
            {
                this.barraEscudo.Value -= valorEscudo;
                dolor();
            }
        }

        private void atacar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.reloj.IsEnabled)
            {
                this.reloj.Stop();
                this.contento.Stop();
                this.pocion.Opacity = 1;
            }
            if(this.barraVida.Value != 0)
            {
                ataqueAnimacion.Begin();
            }
            
        }

        private void pocionEscudo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(this.barraEscudo.Value != 100 && this.barraVida.Value != 0)
            {
                this.barraEscudo.Value = 100;
                this.pocionEscudo.Opacity = 0;
            }
        }

        private void pokemon_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pokemon_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void atacar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void atacar_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void pocion_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pocion_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void pocionEscudo_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pocionEscudo_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}
