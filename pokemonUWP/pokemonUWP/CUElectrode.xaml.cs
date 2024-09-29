using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace pokemonUWP
{
    public sealed partial class CUElectrode : UserControl
    {
        Storyboard muerte;
        Storyboard contento;
        Storyboard rojoDolor;
        Storyboard resucitar;
        Storyboard flotarAccion;
        Storyboard ataqueAnimacion;

        DispatcherTimer reloj = new DispatcherTimer();

        public CUElectrode()
        {
            this.InitializeComponent();
            this.muerte = (Storyboard)this.Resources["morir"];
            this.resucitar = (Storyboard)this.Resources["revivir"];
            this.contento = (Storyboard)this.Resources["contento"];
            this.flotarAccion = (Storyboard)this.Resources["flotar"];
            this.ataqueAnimacion = (Storyboard)this.Resources["atacar"];
            this.rojoDolor = (Storyboard)this.mitadCuerpo.Resources["dolorKey"];
           
            flotarAccion.Begin();
        }

       

        private void dolorAccion()
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

        private void atacar_MouseUp(object sender, object e)
        {
            if (this.reloj.IsEnabled)
            {
                this.reloj.Stop();
                this.contento.Stop();
                this.pocion.Opacity = 1;
            }
            if (this.barraVida.Value != 0)
            {
                ataqueAnimacion.Begin();
            }
        }

        private void pocionEscudo_MouseUp(object sender, object e)
        {
            if (this.barraEscudo.Value != 100 && this.barraVida.Value != 0)
            {
                this.barraEscudo.Value = 100;
                this.pocionEscudo.Opacity = 0;
            }
        }

        private void pocion_MouseUp(object sender, object e)
        {
            if (!this.reloj.IsEnabled)
            {
                this.reloj.Interval = TimeSpan.FromMilliseconds(100);
                this.reloj.Tick += incrementarBarra;
                this.reloj.Start();
                this.contento.Begin();
                this.pocion.Opacity = 0.5;
            }
        }

        private void incrementarBarra(object sender, object e)
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


        private void pokemon_MouseUp(object sender, object e)
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

            if (this.barraEscudo.Value <= 99)
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
                dolorAccion();
            }
        }

       
    }
}
