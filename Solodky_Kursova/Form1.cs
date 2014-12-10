using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solodky_Kursova
{
    public partial class Form1 : Form
    {
        public Form1()//конструктор
        {
            InitializeComponent();

            list = null;
            listSize = 0;
        }

        private void btnDeleteElem_Click(object sender, EventArgs e)//кнопка видалення елемента
        {
            int index = Convert.ToInt32(txtDelNumber.Text) - 1;//отримати індекс елемента, що необхідно видалити

            if (index < listSize && index > -1)//якщо індекс >=0 і менший за розмір списку
            {
                list = CitizenNode.removeElement(list, index);//видалити елемент за вказаним індексом

                listSize--;//зменшити розмір списку
            }

            displayList();//відобразити поточний список
            refreshComboBox();//оновити значення країн в комбо-боксі
        }

        private void btnAddElement_Click(object sender, EventArgs e)//кнопка додавання елементу
        {
            String country = txtCountry.Text.Trim();//отримати назву країни
            String city = txtCity.Text.Trim();//отримати назву міста
            int citizen = Convert.ToInt32(txtCitizen.Text.Trim());//отримати кількість мешканців

            listSize++;//збільшити розмір масиву

            list = CitizenNode.add(list,new CitizenNode(country,city,citizen));//додати новий вузол в кінець списку

            displayList();//відобразити поточний список
            refreshComboBox();//оновити значення країн в комбо-боксі
        }

        private void btnDelimeter_Click(object sender, EventArgs e)//кнопка розділення списку на два за кількістю мешканців
        {
            String kText = txtK.Text;//отримати значення К
            int k = Convert.ToInt32(txtK.Text);//перетворити в цілочисельне значення

            String header = formatTextStandart("№", "Країна", "Місто", "Кількість населення");//стрічка заголовку таблиці
            String resultText = "Міста, з населенням більше за " + kText + "\n" + header;//результуючий текст

            String biggest = "";//табличка з значеннями більшими за К
            String worst = "";//таблиця зі значеннями меншими чи рівними К

            if(k > 0)//якщо К більше невід'ємне
            {
                CitizenNode currentNode = list;//отримати поточний вузол

                int b = 1;//порядковий номер списку більших значень
                int w = 1;//порядковий номер списку менших значень
                do//цикл, який виконується мінімум один раз, доки поточний вузол не буде рівний null
                {
                    if (currentNode.citizenNum > k)//якщо кількість жителів більша за К
                    {
                        //додати в табличку більших значень
                        biggest += formatTextStandart(Convert.ToString(b), currentNode.country, currentNode.city, Convert.ToString(currentNode.citizenNum));

                        b++;
                    }
                    else//якщо кількість жителів менше чи рівне К
                    {
                        //додати в табличку менших значень
                        worst += formatTextStandart(Convert.ToString(w), currentNode.country, currentNode.city, Convert.ToString(currentNode.citizenNum));

                        w++;
                    }

                    currentNode = currentNode.next;//перейти до наступного вузла
                } while(currentNode != null);            

                //сформувати результуючий текст
                resultText += biggest + "Міста, з населенням меншим чи рівним за " + kText + "\n" + header + worst;
                //вивести текст в текст-бокс
                richTextBox1.Text = resultText;
            }
        }

        private void displayList()//функція, що відображує список
        {
            String header = formatTextStandart("№","Країна", "Місто", "Кількість населення");//заголовок таблиці
            String displayText = header;//результучий текст

            CitizenNode currentNode = list;//поточний вузол

            if (currentNode != null)//якщо поточний вузол не null
            {
                int index = 1;

                do//відображувати в таблиці список поелементно
                {
                    displayText += formatTextStandart(Convert.ToString(index),currentNode.country, currentNode.city, Convert.ToString(currentNode.citizenNum));

                    index++;
                    currentNode = currentNode.next;
                } while (currentNode != null);
            }

            richTextBox1.Text = displayText;//вивести текст в текст-бокс
        }

        private void btnCitizenCount_Click(object sender, EventArgs e)//кнопка, що підраховує кількість громадян в обраній країні
        {
            String filterCountry = (String) comboBox1.SelectedItem;//отримати назву країни для підрахунку
            CitizenNode currentNode = list;//отримати перший елемент списку
            int totalPopulation = 0;//загальна кількість людей

            do
            {
                if(currentNode.country.Equals(filterCountry))//якщо назва країни поточного вузла рівна заданій
                {
                    totalPopulation += currentNode.citizenNum;//додати кількість людей цього вузла до загальної кількості
                }

                currentNode = currentNode.next;//перейти до наступного вузла списку
            } while(currentNode != null);

            richTextBox1.Text = "Країна: " + filterCountry + "\nЧисло мешканців в усіх містах заданої країни: " + Convert.ToString(totalPopulation);
        }

        private void btnDisplayList_Click(object sender, EventArgs e)//відобразити список
        {
            displayList();
        }

        private String formatTextStandart(String index, String country, String city, String population)//форматування стрічки у вигляді таблиці
        {
            return String.Format("{0,-3}{1,-2}{2,-19}{3,-2}{4,-19}{5,-1}{6,19}\n",
                                 index, "| ", country, "| ", city, "|", population); 
        }

        private void refreshComboBox()//оновити значення в комбо-боксі
        {
            CitizenNode currentNode = list;//перший елемент списку

            if (currentNode != null)//якщо список не пустий
            {
                do
                {
                    String country = currentNode.country;

                    if (!comboBox1.Items.Contains(country))//якщо назва країни не міститься в комбо-боксі
                    {
                        comboBox1.Items.Add(currentNode.country);//додати назву країни
                    }

                    currentNode = currentNode.next;//наступний елемент списку
                } while(currentNode != null);

                comboBox1.SelectedIndex = 0;//встановити 0-ий елемент обраним
            }
        }

        private CitizenNode list;
        private int listSize;
    }
}
