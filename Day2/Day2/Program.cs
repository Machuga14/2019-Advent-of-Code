using System;
using System.Linq;

namespace Day2
{
  class Program
  {
    static string input = @"1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,13,1,19,1,6,19,23,2,6,23,27,1,5,27,31,2,31,9,35,1,35,5,39,1,39,5,43,1,43,10,47,2,6,47,51,1,51,5,55,2,55,6,59,1,5,59,63,2,63,6,67,1,5,67,71,1,71,6,75,2,75,10,79,1,79,5,83,2,83,6,87,1,87,5,91,2,9,91,95,1,95,6,99,2,9,99,103,2,9,103,107,1,5,107,111,1,111,5,115,1,115,13,119,1,13,119,123,2,6,123,127,1,5,127,131,1,9,131,135,1,135,9,139,2,139,6,143,1,143,5,147,2,147,6,151,1,5,151,155,2,6,155,159,1,159,2,163,1,9,163,0,99,2,0,14,0";
    static void Main(string[] args)
    {
      // test code: remember to comment out the raw massaging below if running test code...
      // input = "1,9,10,3,2,3,11,0,99,30,40,50";

      int[] baseProgram = input
        .Split(new char[] { '\r', '\n', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(t => int.Parse(t))
        .ToArray();

      // copy of the program to execute.
      int[] programCopy = new int[baseProgram.Length];
      baseProgram.CopyTo(programCopy, 0);

      // Apparantly we need to replace [1] with 12, and [2] with 2, per specs...
      // Because index 0 is return, index 1 is left input, index 2 is right input
      int noun = 24;
      int verb = 48;
      programCopy[1] = noun;
      programCopy[2] = verb;
      RunProgram(programCopy);

      // Solution # 1:
      Console.WriteLine("Processing complete. Memory[0]: {0}", programCopy[0]);

      // Solution # 2: (because, I guess, brute force...)
      int desiredOutput = 19690720;
      bool done = false;
      for (noun = 0; noun < 100 && !done; noun++)
      {
        for (verb = 0; verb < 100 && !done; verb++)
        {
          baseProgram.CopyTo(programCopy, 0);
          programCopy[1] = noun;
          programCopy[2] = verb;
          RunProgram(programCopy);
          done = programCopy[0] == desiredOutput;

          if (done)
          {
            42.ToString();
          }
        }
      }

      // Solution # 2:
      Console.WriteLine("Processing complete. Noun={0}, Verb={1}, 100*Noun+Verb={2}. Got Answer? {3}", noun - 1, verb - 1, 100 * (noun - 1) + (verb - 1), done);
      Console.ReadKey();
    }

    static void RunProgram(int[] memory)
    {
      int index = 0;
      bool isDone = false;
      while (!isDone)
      {
        StepVersion1(ref index, ref isDone, memory);
      }
    }

    static void StepVersion1(ref int index, ref bool isDone, int[] memory)
    {
      int op = memory[index];

      switch(op)
      {
        case 1: // add
          memory[memory[index + 3]] = memory[memory[index + 1]] + memory[memory[index + 2]];
          index += 4;
          break;
        case 2: // Multiply
          memory[memory[index + 3]] = memory[memory[index + 1]] * memory[memory[index + 2]];
          index += 4;
          break;
        case 99: // HALT
          isDone = true;
          break;
        default:
          throw new Exception(string.Format("Op [{0}] from index [{1}] illegal", op, index));
      }
    }
  }
}