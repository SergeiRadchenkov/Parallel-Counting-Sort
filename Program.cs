// Параллельная сортировка подсчётом

const int THREADES_NUMBER = 4; // число потоков
const int N = 1000; // размер массива
object locker = new object();

Random rand = new Random();
int[] resSerial = new int[N].Select(r => rand.Next(0, 5)).ToArray();
int[] resParalel = new int[N];

Array.Copy(resSerial, resParalel, N);
Console.WriteLine(string.Join(", ", resSerial));
Console.WriteLine(string.Join(", ", resParalel));

CountingSortExtended(resSerial);
PrepareParallelCountingSort(resParalel);
Console.WriteLine(EqualityMatrix(resSerial, resParalel));

Console.WriteLine(string.Join(", ", resSerial));
Console.WriteLine(string.Join(", ", resParalel));


void PrepareParallelCountingSort(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min;
    int[] counters = new int[max + offset + 1];

    int eachThreadCalc = N / THREADES_NUMBER;
    var threadsParall = new List<Thread>();

    for (int i = 0; i < THREADES_NUMBER; i++)
    {
        int startPos = i * eachThreadCalc;
        int endPos = (i + 1) * eachThreadCalc;
        if (i == THREADES_NUMBER - 1) endPos = N;
        threadsParall.Add(new Thread(() => CountingSortParallel(inputArray, counters, offset, startPos, endPos)));
        threadsParall[i].Start();
    }
    foreach(var tread in threadsParall)
    {
        tread.Join();
    }
    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++)
        {
            inputArray[index] = i - offset;
            index++;
        }
    }
}

void CountingSortParallel(int[] inputArray, int[] counters, int offset, int startPos, int endPos)
{
    for (int i = startPos; i < endPos; i++)
    {
        lock (locker)
        {
            counters[inputArray[i] + offset]++;
        }
    }
}

void CountingSortExtended(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = 0;
    if (min < 0)
    {
        offset = -min;
    };

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
            inputArray[index] = i - offset;
            index++;
        }
    }
}

bool EqualityMatrix(int[] fmatrix, int[] smatrix)
{
    bool res = true;
    for (int i = 0; i < N; i++)
    {
        res = res && (fmatrix[i] == smatrix[i]);
    }
    return res;
}