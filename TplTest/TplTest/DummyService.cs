
namespace TplTest
{
    class DummyService
    {
        private readonly int _number;

        public DummyService(int number)
        {
            _number = number;
        }

        public async Task SomeMethod()
        {
            var delayTimeSpan = TimeSpan.FromSeconds(3);

            while (true)
            {
                Console.WriteLine($"{nameof(DummyService)}:{_number}: Current proc id: {Environment.ProcessId}, Thread: {Environment.CurrentManagedThreadId}");
                await Task.Delay(delayTimeSpan);
            }
        }
    }
}
