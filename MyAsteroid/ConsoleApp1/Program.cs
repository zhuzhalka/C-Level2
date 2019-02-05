using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4_HW_ex2
{
    class Program
    {
        static void Main()
        {
            Random rnd = new Random();
            var MyList1 = new List<int>();
            var MyList2 = new List<int>();
            
            //Формирование листа из элементо и его вывод на консоль
            for ( int i=0; i<10; i++)
            {
                MyList1.Add(rnd.Next(1,5));
            }
            foreach(int el in MyList1)
                Console.Write(el+" ");

            Console.WriteLine();

            //Определение сколько всего разных элементов имеется в нашем списке
            var MyHashList = new HashSet<int>();
            MyHashList.UnionWith(MyList1);
            for (int i = 0; i < MyList1.Count; i++)
            {
                MyHashList.Add(MyList1[i]);
            }
            foreach (int el in MyHashList)
                Console.Write(el + " ");
            Console.WriteLine();
            //Определяем сколько раз каждый элемент встречается в списке
            foreach(int el in MyHashList)
            {
                int k = 0;
                for (int j = 0; j < MyList1.Count; j++)
                {
                    if (el == MyList1[j]) k++;
    
                }
                Console.WriteLine("Элемент " + el.ToString() + "встречается в множестве " + k.ToString());
            }
            
            Console.ReadLine();
        }
    }
}
