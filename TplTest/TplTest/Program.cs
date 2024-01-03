// See https://aka.ms/new-console-template for more information

using TplTest;

Console.WriteLine($"App Main: Current proc id: {Environment.ProcessId}");
var factory = new TaskFactory();

var service1 = new DummyService(1);
var handle1 = await factory.StartNew(service1.SomeMethod, TaskCreationOptions.LongRunning);

var service2 = new DummyService(2);
var handle2 = await factory.StartNew(service2.SomeMethod, TaskCreationOptions.LongRunning);

Task.WaitAll(handle1, handle2);

Console.WriteLine("done...");
