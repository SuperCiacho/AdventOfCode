using System.IO;

namespace AdventOfCode.Days
{
    public abstract class DayBase : IRunner
    {
        protected string[] InputFile;
        protected string InputFilePath;

        protected DayBase(bool isInputAvailable = true)
        {
            if (isInputAvailable)
            {
                this.InputFilePath = $"Inputs\\{this.GetType().Name}.txt";
                this.InputFile = File.ReadAllLines(this.InputFilePath);
            }
        }

        void IRunner.Run()
        {
            this.Run();
        }

        public abstract void Run();
    }
}