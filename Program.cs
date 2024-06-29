// Сортировка подсчётом

int[] array = {0, 2, 3, 2, 1, 5, 9, 1, 1, 2, 1, 3, 4, 6, 3, 1, 4, 8, 5, 3};

void CountingSort(int[] inputArray)
{
    int[] counters = new int[10];

    for (int i = 0; i < inputArray.Length; i++)
    {
        // counters[inputArray[i]]++;
        int ourNumber = inputArray[i];
        counters[ourNumber]++;
    }

    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++)
        {
            inputArray[index] = i;
            index++;
        }
    }
}

CountingSort(array);

Console.WriteLine(string.Join(", ", array));

int[] CountingSortExtended(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();


    int offset = 0;
    if (min < 0)
    {
        offset = -min;
    };

    int[] sortedArray = new int[inputArray.Length];
    int[] counters = new int[max + offset + 1];

    for (int i = 0; i < inputArray.Length; i++)
    {
        counters[inputArray[i] + offset]++;
    }

    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++)
        {
            sortedArray[index] = i - offset;
            index++;
        }
    }
    return sortedArray;
}

int[] array2 = {0, 2, 4, 10, 20, 5, 6, 1, 2};
int[] sortedArray = CountingSortExtended(array2);
Console.WriteLine(string.Join(", ", sortedArray));

int[] array3 = {-10, -5, -9, 0, 2, 5, 1, 3, 1, 0, 1};
int[] sortedArray2 = CountingSortExtended(array3);
Console.WriteLine(string.Join(", ", sortedArray2));