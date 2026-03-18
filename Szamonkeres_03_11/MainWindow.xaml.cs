using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using Microsoft.EntityFrameworkCore;

namespace Szamonkeres_03_11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            lbCim.Visibility = Visibility.Hidden;
            lbSzerzo.Visibility = Visibility.Hidden;
            lbEv.Visibility = Visibility.Hidden;
            lbAr.Visibility = Visibility.Hidden;
            btnHozzaad.Visibility = Visibility.Hidden;
            btnMegse.Visibility = Visibility.Hidden;
        }

        private void LoadData()
        {
            BookContext context = new BookContext();
            dbBook.ItemsSource = context.Books.ToList();
        }

        private void btnUj_Click(object sender, RoutedEventArgs e)
        {
            lbCim.Visibility = Visibility.Visible;
            lbSzerzo.Visibility = Visibility.Visible;
            lbEv.Visibility = Visibility.Visible;
            lbAr.Visibility = Visibility.Visible;
            btnHozzaad.Visibility = Visibility.Visible;
            btnMegse.Visibility = Visibility.Visible;
        }

        private void btnMegse_Click(object sender, RoutedEventArgs e)
        {
            lbCim.Visibility = Visibility.Hidden;
            lbSzerzo.Visibility = Visibility.Hidden;
            lbEv.Visibility = Visibility.Hidden;
            lbAr.Visibility = Visibility.Hidden;
            btnHozzaad.Visibility = Visibility.Hidden;
            btnMegse.Visibility = Visibility.Hidden;
        }

        private void btnHozzaad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BookContext context = new BookContext();

                Book ujKonyv = new Book
                {
                    title = lbCim.Text,
                    author = lbSzerzo.Text,
                    year = int.Parse(lbEv.Text),
                    price = int.Parse(lbAr.Text)
                };

                context.Books.Add(ujKonyv);
                context.SaveChanges();

                LoadData();

                lbCim.Visibility = Visibility.Hidden;
                lbSzerzo.Visibility = Visibility.Hidden;
                lbEv.Visibility = Visibility.Hidden;
                lbAr.Visibility = Visibility.Hidden;
                btnHozzaad.Visibility = Visibility.Hidden;
                btnMegse.Visibility = Visibility.Hidden;

                MessageBox.Show("Könyv sikeresen hozzáadva!");
            }
            catch
            {
                MessageBox.Show("Hibás adat!");
            }
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = dbBook.SelectedItem as Book;

            if (selectedBook != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Biztos törlöd a könyvet?",
                    "Törlés",
                    MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    BookContext context = new BookContext();
                    context.Books.Remove(selectedBook);
                    context.SaveChanges();

                    LoadData();
                }
            }
        }

        private void dbBook_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Book selectedBook = dbBook.SelectedItem as Book;

            if (selectedBook != null)
            {
                lbCim.Text = selectedBook.title;
                lbSzerzo.Text = selectedBook.author;
                lbEv.Text = selectedBook.year.ToString();
                lbAr.Text = selectedBook.price.ToString();

                lbCim.Visibility = Visibility.Visible;
                lbSzerzo.Visibility = Visibility.Visible;
                lbEv.Visibility = Visibility.Visible;
                lbAr.Visibility = Visibility.Visible;
                btnHozzaad.Visibility = Visibility.Visible;
                btnMegse.Visibility = Visibility.Visible;
            }
        }

        private void btnSzerkeszt_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = dbBook.SelectedItem as Book;

            if (selectedBook != null)
            {
                try
                {
                    BookContext context = new BookContext();

                    var book = context.Books.FirstOrDefault(b => b.id == selectedBook.id);

                    if (book != null)
                    {
                        book.title = lbCim.Text;
                        book.author = lbSzerzo.Text;
                        book.year = int.Parse(lbEv.Text);
                        book.price = int.Parse(lbAr.Text);

                        context.SaveChanges();
                    }

                    LoadData();

                    lbCim.Visibility = Visibility.Hidden;
                    lbSzerzo.Visibility = Visibility.Hidden;
                    lbEv.Visibility = Visibility.Hidden;
                    lbAr.Visibility = Visibility.Hidden;
                    btnHozzaad.Visibility = Visibility.Hidden;
                    btnMegse.Visibility = Visibility.Hidden;

                    MessageBox.Show("Könyv módosítva!");
                }
                catch
                {
                    MessageBox.Show("Hibás adat!");
                }
            }
            else
            {
                MessageBox.Show("Válassz ki egy könyvet!");
            }
        }
    }
}
