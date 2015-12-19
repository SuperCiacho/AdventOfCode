using System.IO;

namespace AdventOfCode.Days
{
    public abstract class DayBase : IRunner
    {
        protected string InputFile;

        protected DayBase(bool isInputAvailable = true)
        {
            if (isInputAvailable)
            {
                var path = $"Inputs\\{this.GetType().Name}.txt";
                this.InputFile = File.ReadAllText(path);
            }
        }

        public abstract void Run();

        void IRunner.Run()
        {
            this.Run();
        }
    }
}
