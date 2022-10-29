int chooseQuestion = 0;
do{
    Console.WriteLine("Для закрытия введите 0");
    Console.WriteLine("Введите номер задания(1|2|3|4*|5*): ");
    chooseQuestion = int.Parse(Console.ReadLine()!);
    switch (chooseQuestion) {
		case 1: Question47(); break;
        case 2: Question50(); break;
        case 3: Question52(); break;
        case 4: Question60(); break;
        case 5: Question62(); break;
	}

} while (chooseQuestion != 0);

// Задача 47. Задайте двумерный массив размером m×n, заполненный случайными вещественными числами.

void Question47(){
    int m = 4;
    int n = 5;
    object[,] array = GetDoubleArray(m,n,-10,10,"double");
    ViewDablArray(array,"double");
}


/* Задача 50. Напишите программу, которая на вход принимает позиции элемента в двумерном массиве, и возвращает значение этого элемента или же указание, что такого элемента нет.
Например, задан массив:
1 4 7 2
5 9 2 3
8 4 2 4
i = 4, j = 2 -> такого числа в массиве нет
i = 1, j = 2 -> 2 */

void Question50(){
    int rowSize = 3;
    int colSize = 5;
    object[,] array = GetDoubleArray(rowSize,colSize,0,9);
    ViewDablArray(array);
    Console.WriteLine("Введите позицию элемента по вертика: ");
    int row = int.Parse(Console.ReadLine()!)-1;
    Console.WriteLine("Введите позицию элемента по горизонтали: ");
    int col = int.Parse(Console.ReadLine()!)-1;
    Console.WriteLine();
    if (row < rowSize && row >= 0 && col < colSize && col >= 0) Console.WriteLine("Выбрано значение = " + array[row,col]);
    else Console.WriteLine("Вы вышли за диапазон таблицы");
}

/* Задача 52. Задайте двумерный массив из целых чисел. Найдите среднее арифметическое элементов в каждом столбце.

Например, задан массив:
1 4 7 2
5 9 2 3
8 4 2 4
Среднее арифметическое каждого столбца: 4,6; 5,6; 3,6; 3. */

void Question52(){
    int rowSize = 3;
    int colSize = 4;
    object[,] array = GetDoubleArray(rowSize,colSize,0,9);
    ViewDablArray(array);
    double[] arr = AverageRows(array);
    Console.Write("Среднее арифмитическое каждого столбца: ");
    foreach (double item in arr)
    {
        Console.Write($"{item:F2}; ");
    }
    Console.WriteLine();

}

/* Задача 60. ...Сформируйте трёхмерный массив из неповторяющихся двузначных чисел. Напишите программу, которая будет построчно выводить массив, добавляя индексы каждого элемента.
Массив размером 2 x 2 x 2
66(0,0,0) 25(0,1,0)
34(1,0,0) 41(1,1,0)
27(0,0,1) 90(0,1,1)
26(1,0,1) 55(1,1,1) */

void Question60(){
    int length = 2;
    int height = 2;
    int width = 2;
    if (length * height * width > 98) Console.WriteLine("К сожалению заданный размер выходит за рамки поставленных в задаче ограничений");
    else {
        int[,,] array = GetTripleArray(length,height,width);
        ViewTripleArray(array); 
    }
}

/* Задача 62. Напишите программу, которая заполнит спирально массив 4 на 4.
Например, на выходе получается вот такой массив:
01 02 03 04
12 13 14 05
11 16 15 06
10 09 08 07 */

void Question62(){
    int rows = 9;
    int cols = 10;
    object[,] array = GetSpiralArray(rows,cols);
    ViewDablArray(array, isNeedFormat: true);
}

//-----------------------------------------------------------------------------------------------------
// Методы для решения задач

// Общий метод для создания двумерных массивов
object[,] GetDoubleArray(int rows, int cols, int minValue, int maxValue, String type = "int"){
    object[,] collection = new object[rows,cols];
    if (type == "double") {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++)
            {
                collection[i,j] = new Random().NextDouble()*(maxValue*2)+(minValue);
            }
        }
    }
    else {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++)
            {
                collection[i,j] = new Random().Next(minValue, maxValue+1);
            }
        }
    }
    
    return collection; 
}

// Общий метод для вывода на экран двумерных массивов
void ViewDablArray(object[,] collection, String type = "int", bool isNeedFormat = false){
    int lengthNumber = (collection.GetLength(0) * collection.GetLength(1)).ToString().Length;
    if (type == "double"){
        for (int i = 0; i < collection.GetLength(0); i++)
        {
            for (int j = 0; j < collection.GetLength(1); j++)
            {
                Console.Write($"{collection[i,j]:F2} | ");
            }
            Console.WriteLine();
        }
    }
    else{
        for (int i = 0; i < collection.GetLength(0); i++)
        {
            for (int j = 0; j < collection.GetLength(1); j++)
            {
                if (isNeedFormat){
                        for (int y = 0; y < lengthNumber - collection[i,j].ToString()!.Length; y++) Console.Write("0");
                    }
                Console.Write($"{collection[i,j]} | ");
            }
            Console.WriteLine();
        }  
    }
}    

// Метод для нахождения среднего арифместичского значения в столбцах заданного двумерного массива (Q52)
double[] AverageRows(object[,] collection){
    double[] array = new double[collection.GetLength(1)];
    for (int i = 0; i < collection.GetLength(0); i++)
    {
        for (int j = 0; j < collection.GetLength(1); j++)
        {
            array[j] += Convert.ToInt32(collection[i,j]);
        }
    }
    for (int i = 0; i < array.Length; i++)
    {
        array[i]/=collection.GetLength(0);
    }

    return array;
}

// Метод для создания трехмерного массива (Q60)
int[,,] GetTripleArray(int length, int height, int width){
    int[,,] collection = new int[length,height,width];
    int[] numbers = new int[100];
    for (int i = 0; i < 100; i++)
    {
        numbers[i]=i;
    }

    for (int i = 0; i < length; i++)
    {
        for (int j = 0; j < height; j++)
        {
            for (int y = 0; y < width; y++)
            {
                do
                {
                    int x =new Random().Next(0,100);
                    collection [i,j,y] = numbers[x];
                    numbers[x] = 0;
                } while (collection[i,j,y] == 0);
            }
        }
    }

    return collection;
}

// Метод для вывода на экран трехмерного массива (Q60)
void ViewTripleArray(int[,,] collection){
    for (int i = 0; i < collection.GetLength(0); i++)
    {
        for (int j = 0; j < collection.GetLength(1); j++)
        {
            for (int y = 0; y < collection.GetLength(2); y++)
            {
                Console.Write($"{collection[i,j,y]}({i},{j},{y}) | ");
            }
            Console.WriteLine();
        }
    }
}

// Метод для создания двумерного массива в виде спирали (Q62)
object[,] GetSpiralArray(int rows, int cols){
    object[,] collection = new object[rows,cols];
    int x = rows * cols;
    int count=1;
    int startRow=0;
    int startCol = 0;
    int start = 0;
    do{
            for (int j = startCol; j < cols; j++)
            {
                collection[startRow,j] = count;
                count++;
                startCol=j;
            }
            if (count > x) break;
            count--;
            for (int i = startRow; i < rows; i++)
            {
                collection[i,startCol] = count;
                count++;
                startRow=i;
            }
            if (count > x) break;
            count--;
            for (int j = startCol; j >= start; j--)
            {
                collection[startRow,j] = count;
                count++;
                startCol=j;
            } 
            if (count > x) break;
            count--;
             for (int i = startRow; i > start; i--)
            {
                collection[i,startCol] = count;
                count++;
                startRow=i;
            }
            if (count > x) break;
            count--;
        start++;
        rows--;
        cols--;
    } while (count < x);

    return collection;

}