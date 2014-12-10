using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solodky_Kursova
{
    class CitizenNode
    {
        public String country;//назва країни
        public String city;//назва міста
        public int    citizenNum;//кількість мешканців

        public CitizenNode next;//вказівник на наступний елемент списку

        public CitizenNode(String country, String city, int citizenNum)//конструктор
        {
            this.country = country;
            this.city = city;
            this.citizenNum = citizenNum;

            next = null;
        }

        public static CitizenNode add(CitizenNode startNode, CitizenNode newNode)//статичний метод класу, що додає елемент до списку
        {
            if (startNode == null)//якщо список пустий
            {
                startNode = newNode;//зробити першим елементом переданий вузол
            }
            else//якщо в списку є елементи
            {
                CitizenNode nextNode = startNode;//отримати перший елемент списку

                while (nextNode.next != null)//доки не останній елемент
                {
                    nextNode = nextNode.next;//перейти до наступного елемента
                }

                nextNode.next = newNode;//додати вказівник на новий елемент в кінець списку
            }                      

            return startNode;//повернути початковий список з доданим елементом
        }

        public static CitizenNode removeElement(CitizenNode startNode, int index)//видалення елементу з списку за індексом
        {
            CitizenNode node = startNode;//отримати початок списку
            CitizenNode currentNode = startNode;//отримати перший елемент списку

            if (index == 0)//якщо індекс 0-го елемента
            {
                node = node.next;//початком списку зробити 1-ий елемент (0-ий видаляється)
            }
            else//інакше
            {
                for (int i = 0; i < index - 1 && currentNode.next != null; i++)//доки не досягнуто кінця списку або не отримано елемент, після якого потрібно видалити вузол
                {
                    currentNode = currentNode.next;//поточним вузлом зробити наступний
                }

                currentNode.next = currentNode.next.next;//змінити вказівник поточного вузла на елемент, на який вказує вузол, що видаляємо
            }

            return node;//поввернути список
        }
    }
}
