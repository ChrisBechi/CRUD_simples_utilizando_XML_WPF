using Exercicio_XML.DAO;
using Exercicio_XML.Model;
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

namespace Exercicio_XML
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        ClienteDAO dao;
        Produto prodAtual;
        public MainWindow()
        {
            InitializeComponent();
            dao = new ClienteDAO();
            limpar();
        }

        public void limpar()
        {
            Cnome.Text = String.Empty;
            Ccodigo.Text = String.Empty;
            Cvalor.Text = String.Empty;
            prodAtual = null;
        }

        private void texto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (texto.Text == "Disponivel")
            {
                ColorAnimation animation;
                animation = new ColorAnimation();
                animation.To = Colors.Red;
                animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                this.borda.Background = new SolidColorBrush(Colors.Green);
                this.borda.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                texto.Text = "Indisponivel";
            }
            else
            {
                ColorAnimation animation = new ColorAnimation();
                animation.To = Colors.Green;
                animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                this.borda.Background = new SolidColorBrush(Colors.Red);
                this.borda.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                texto.Text = "Disponivel";
            }
        }

        private void disponivel()
        {
            if (texto.Text == "Disponivel")
            {
                ColorAnimation animation = new ColorAnimation();
                animation.To = Colors.Green;
                animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                this.borda.Background = new SolidColorBrush(Colors.Red);
                this.borda.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                texto.Text = "Disponivel";
            }
            else
            {
                ColorAnimation animation;
                animation = new ColorAnimation();
                animation.To = Colors.Red;
                animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                this.borda.Background = new SolidColorBrush(Colors.Green);
                this.borda.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                texto.Text = "Indisponivel";
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            limpar();
        }

        private void btnIncluir_Click(object sender, RoutedEventArgs e)
        {
            Produto prod = new Produto()
            {
                cod = Convert.ToInt32(Ccodigo.Text),
                nome = Cnome.Text,
                valor = (float)Convert.ToDouble(Cvalor.Text),
                disponivel = (texto.Text == "Disponivel")?true:false
            };
            dao.incluir(prod);
            limpar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            prodAtual = dao.consultar(Convert.ToInt32(Ccodigo.Text));
            if (prodAtual != null)
            {
                prodAtual.nome = Cnome.Text;
                prodAtual.valor = (float) Convert.ToDouble(Cvalor.Text);
                prodAtual.disponivel = (texto.Text.Equals("Disponivel")) ? true : false;
                dao.alterar(prodAtual);
            }
            else
                MessageBox.Show($"Não foi possivel localizar o produto com o codigo:{Ccodigo.Text}!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            prodAtual = dao.consultar(Convert.ToInt32(Ccodigo.Text));
            if (prodAtual != null)
            {
                dao.Estoque.Remove(prodAtual);
                limpar();
            }
            else
            {
                MessageBox.Show($"Não foi possivel localizar o produto com o codigo:{Ccodigo.Text}!");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            prodAtual = dao.consultar(Convert.ToInt32(Ccodigo.Text));
            if (prodAtual != null)
            {
                Cnome.Text = prodAtual.nome;
                Cvalor.Text = $"{prodAtual.valor}";
                texto.Text = (prodAtual.disponivel) ? $"Disponivel" : $"Indisponivel";
                disponivel();
            }
            else
                MessageBox.Show($"Não foi possivel localizar o produto com o codigo:{Ccodigo.Text}!");
        }
    }
}
