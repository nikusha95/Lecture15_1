// See https://aka.ms/new-console-template for more information

Thread t1 = new Thread(M1)
{
    Name = "first Thread"
};
Thread t2 = new Thread(M2)
{
    Name = "second Thread"
};
// t1.Start();
// t1.Join();
// t2.Start();
//new Thread(M1).Start();
Console.WriteLine(2);


Thread t = new Thread(Print);
t.Start("hello");

void Print(object message) => Console.WriteLine(message);


var task = Task.Run(() =>
{
    Console.WriteLine("Waiting");
    Thread.Sleep(5000);
    Console.WriteLine("Hi");
    int res = 0;
    for (int i = 0; i < 10; i++)
    {
        res += i;
    }
    
    return res;
});
//task.Wait();
var awaiter = task.GetAwaiter();

int x = awaiter.GetResult();
Console.WriteLine(x);

await Task.Run(() =>
{
    Console.WriteLine("Waiting");
    Thread.Sleep(5000);
    Console.WriteLine("Hi");
});

Console.WriteLine(task.Status);
Console.WriteLine(task.IsCompleted);


void M1()
{
    object dummy = new object();

    for (int i = 0; i < 10; i++)
    {
        lock (dummy)
        {
            Console.WriteLine($"Thread id :{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"M1 {i}");
        }
    }
}

void M2()
{
    try
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Thread id :{Thread.CurrentThread.ManagedThreadId}");
            //Console.WriteLine($"Thread name :{Thread.CurrentThread.Name}");
            Console.WriteLine($"M2 {i}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}