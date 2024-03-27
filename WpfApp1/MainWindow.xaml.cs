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
using System.Xml.Serialization;
using System.IO;
using static System.Net.Mime.MediaTypeNames;


namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        private bool isEditing = false;
        private string password = "qwerty";
        private Test currentTest;

        public MainWindow()
        {
            InitializeComponent();


            LoadTestData();
        }

        private void LoadTestData()
        {
            try
            {

                XmlSerializer serializer = new XmlSerializer(typeof(Test));


                using (FileStream fs = new FileStream("test.xml", FileMode.Open))
                {

                    currentTest = (Test)serializer.Deserialize(fs);
                }


                if (currentTest == null)
                {
                    currentTest = new Test();
                }


                dataGridQuestions.ItemsSource = currentTest.Questions;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error loading test data: " + ex.Message);
            }
        }

        private void SaveTestData()
        {
            try
            {

                XmlSerializer serializer = new XmlSerializer(typeof(Test));


                using (FileStream fs = new FileStream("test.xml", FileMode.Create))
                {

                    serializer.Serialize(fs, currentTest);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error saving test data: " + ex.Message);
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {

            if (isEditing)
            {

                if (passwordBox.Password == password)
                {

                    tabControl.SelectedIndex = 1;
                }
                else
                {

                    MessageBox.Show("Incorrect password");
                }
            }
            else
            {

                if (currentTest.Questions.Count > 0)
                {

                    tabControl.SelectedIndex = 2;
                }
                else
                {

                    tabControl.SelectedIndex = 4;
                }
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {

            tabControl.SelectedIndex = 1;
        }

        private void ButtonTakeTest_Click(object sender, RoutedEventArgs e)
        {

            tabControl.SelectedIndex = 2;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

            SaveTestData();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {

            Question currentQuestion = (Question)dataGridQuestions.SelectedItem;


            string answer = "";
            if (radioButtonA.IsChecked == true)
            {
                answer = radioButtonA.Content.ToString();
            }
            else if (radioButtonB.IsChecked == true)
            {
                answer = radioButtonB.Content.ToString();
            }
            else if (radioButtonC.IsChecked == true)
            {
                answer = radioButtonC.Content.ToString();
            }
            else if (RadioButtonD.IsChecked == true)
            {
                answer = radioButtonD.Content.ToString();
            }
        }
    }
}