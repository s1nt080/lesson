using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // #1
            Console.WriteLine("Найдём среднее арифметическое значение");
            Console.WriteLine("введите число 1");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("ввечите число 2");
            double b = Convert.ToDouble(Console.ReadLine());
            double res = (a + b) / 2;
            Console.WriteLine("Получаем " + res);
            //#2
            Console.WriteLine("Введите 3 числа, а затем получите их сумму и произведение");
            double d = double.Parse(Console.ReadLine());
            double e = double.Parse(Console.ReadLine());
            double f = double.Parse(Console.ReadLine());
            double res1 = d + e + f, res2 = d * e * f;
            Console.WriteLine("получаем: сумма = " + res1 + ", а произведение = " + res2);
            //#3
            Console.WriteLine("введите кол-во рублей для перевода в доллар ");
            float g = float.Parse(Console.ReadLine()); // Получаем кол-во рублей для перевода в доллары
            float h = 78.3f; // Рублей в 1 долларе
            Console.WriteLine("При переводе получаем " + (g / h));
            //#4
            Console.WriteLine("Проверка числа на чётность");
            Double i = Convert.ToDouble(Console.ReadLine());
            if (i % 2 == 0)
            {
                Console.WriteLine("число чётное");
            }
            else
            {
                Console.WriteLine("число нечётное");
            }

            //#5 калькулятоор через if else
            Console.WriteLine("Калькулятор написанный методом if else");
            Console.WriteLine("Введите 2 числа с которыми будете совершать операции");
            double num1 = double.Parse(Console.ReadLine()), num2 = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите арифметическую операцию");
            string arithmetic_operator = Console.ReadLine();
            if (arithmetic_operator == "+")
            {
                Console.WriteLine(num1 + num2);
            }
            else if (arithmetic_operator == "-")
            {
                Console.WriteLine(num1 - num2);
            }
            else if (arithmetic_operator == "*")
            {
                Console.WriteLine(num1 * num2);
            }
            else if (arithmetic_operator == "/")
            {
                Console.WriteLine(num1 / num2);
            }
            else if (arithmetic_operator == "%")
            {
                Console.WriteLine(num1 % num2);
            }
            else
            {
                Console.WriteLine("Тут явно что-то не то");
            }
            //#6 Калькулятор написанный через switch +try catch +do while

            Console.WriteLine("Посчитаем что-нибудь? введите: Да/Нет");
            string repeat = Console.ReadLine();
            if (repeat == "Да" || repeat == "да")
            {
                do
                {
                    Console.WriteLine("Этот калькулятор написан методом switch");
                    Console.WriteLine("Введите 2 числа с которыми будете совершать операции");
                    string number1 = Console.ReadLine(), number2 = Console.ReadLine();
                    double number11, number22;
                    bool right;
                    try
                    {
                        number11 = double.Parse(number1);
                        number22 = double.Parse(number2);
                        right = true;
                        Console.WriteLine("Числа приняты");
                        switch (right)
                        {
                            case true:
                                Console.WriteLine("введите арифметическую операцию");
                                string logic_symbol = Console.ReadLine();
                                switch (logic_symbol)
                                {
                                    case "+":
                                        Console.WriteLine(number11 + number22);
                                        break;
                                    case "-":
                                        Console.WriteLine(number11 - number22);
                                        break;
                                    case "*":
                                        Console.WriteLine(number11 * number22);
                                        break;
                                    case "/":
                                        Console.WriteLine(number11 / number22);
                                        break;
                                    case "%":
                                        Console.WriteLine(number11 % number22);
                                        break;
                                    default:
                                        Console.WriteLine("Ты явно вписал что-то не то");
                                        break;
                                }
                                break;
                            case false:
                                Console.WriteLine("где-то проёб");
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Где-то проёб");
                    }
                    Console.WriteLine("Посчитаем что-нибудь? введите: Да/Нет");
                    repeat = Console.ReadLine();
                } while (repeat == "Да");
            }
            //#7
            Console.WriteLine("Введиет значение 'MinNumber'");
            int MinNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Введиет значение 'MaxNumber'");
            int MaxNumber = int.Parse(Console.ReadLine());
            int NumSum = 0; // сумма чётных чисел
            int NumSum1 = 0; // сумма нечётных чисел
            int count = 0; // счётчик чётных чисел
            int count1 = 0; // счётчик нечётных чисел

            while (MinNumber < MaxNumber)
            {
                MinNumber++;
                if (MinNumber % 2 == 0)
                {
                    count++;
                    NumSum += MinNumber;
                }
                else
                {
                    count1++;
                    NumSum1 += MinNumber;
                }
            }
            Console.WriteLine("Итоговое значение чётных чисел " + count);
            Console.WriteLine("Итоговое значение нечётных чисел " + count1);
            Console.WriteLine("Сумма значений чётных чисел " + NumSum);
            Console.WriteLine("Сумма значений нечётных чисел " + NumSum1);

            //#8 Написать код, который нарисует в консоли 2 треугольника не используя if else ил switch

            string block = "#";
            int repeater = int.Parse(Console.ReadLine());
            Console.Clear();

            for (int NumRepeater = 0; NumRepeater < repeater; NumRepeater++)
            {
                for (int l = 0; l < 11; l++)
                {
                    for (int j = 0; j < l + 1; j++)
                    {
                        Console.Write(block);
                    }
                    Console.WriteLine();
                }
                int jz = 0;
                while (jz < 2)
                {
                    Console.WriteLine();
                    jz++;
                }
                for (int x = 10; x > -1; x--)
                {
                    for (int j = 0; j < x + 1; j++)
                    {
                        Console.Write(block);
                    }
                    Console.WriteLine();
                }
                jz = 0;
                while (jz < 2)
                {
                    Console.WriteLine();
                    jz++;
                }
                for (int x = 10; x > -1; x--)
                {
                    int l = 0;
                    for (; l < x; l++)
                    {
                        Console.Write(" ");
                    }
                    for (; l < 11; l++)
                    {
                        Console.Write(block);
                    }
                    Console.WriteLine();
                }
                jz = 0;
                while (jz < 2)
                {
                    Console.WriteLine();
                    jz++;
                }
                for (int x = 0; x < 11; x++)
                {
                    int l = 0;
                    for (; l < x; l++)
                    {
                        Console.Write(" ");
                    }
                    for (; l < 11; l++)
                    {
                        Console.Write(block);
                    }
                    Console.WriteLine();
                }
            }
            //#9 Заполнить массив с клавиатуры
            Console.WriteLine("Введите кол-во ячеек в массиве");
            int arraylen = int.Parse(Console.ReadLine());
            Console.Clear();
            int[] Array = new int[arraylen];
            Console.WriteLine("кол-во ячеек в массиве " + Array.Length + "\nзаполните их");
            for (int counter = 0; counter < Array.Length; counter++)
            {
                Array[counter] = int.Parse(Console.ReadLine());
            }
            for (int counter = 0; counter < Array.Length; counter++)
            {
                Console.WriteLine("Ячека массива # " + counter + " содержит в себе: " + Array[counter]);
            }
            //#9.1 Вывести массив в обратном порядке
            for (int counter = arraylen - 1; counter < Array[arraylen - 1]; counter--)
            {
                Console.WriteLine(Array[counter]);
            }
            //9.2 Найти сумму чисел в массиве
            int SumArray = 0;
            for (int counter = 0; counter < Array.Length; counter++)
            {
                Array[counter] = int.Parse(Console.ReadLine());
                SumArray = SumArray + Array[counter];
            }
            Console.WriteLine(SumArray);
            //9.3 Найти наименьшее число в массиве
            int MinNumArray = Array[0];
            for (int counter = 0; counter < Array.Length; counter++)
            {
                if (Array[counter] < MinNumArray)
                {
                    MinNumArray = Array[counter];
                }
            }
            Console.Clear();
            Console.WriteLine(MinNumArray);
        }
    }
}


