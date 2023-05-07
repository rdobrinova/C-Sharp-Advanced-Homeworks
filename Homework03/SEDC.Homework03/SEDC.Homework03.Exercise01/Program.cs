using System.Threading.Tasks;

//EXERCISE01
//●Create a folder called Exercise
//● Create a txt file in it called calculations
//● Create a method that calculates 2 numbers and returns a string
//in the format: num1 + num2 = result(Ex: 2 + 3 = 5)
//● Ask the user to enter 2 numbers, call the calculate method and
//give the result
//● After the result is written in the console it should also be written
//in the calculations.txt file with a time stamp next to it
//● Call the console 3 times and write 3 results in the txt file



string appPath = @"..\..\..\";
string newFolder = appPath + @"\Exercise";
string filesPath = newFolder + @"\calculatons.txt";

if (!Directory.Exists(newFolder))
{
    Directory.CreateDirectory(newFolder);
}

if (!File.Exists(filesPath))
{
    File.Create(filesPath).Close();
}



void Calculator(int number1, int number2)
{
    var result = $"[{DateTime.Now}]: {number1} + {number2} = {number1 + number2}";
    using (StreamWriter sw = new StreamWriter(filesPath, true))
    {
        Console.WriteLine(result);
        sw.WriteLine(result);
    }
}


Console.WriteLine("Enter first number: ");
bool isNum1Parsed = int.TryParse(Console.ReadLine(), out int num1);

Console.WriteLine("Enter second number: ");
bool isNum2Parsed = int.TryParse(Console.ReadLine(), out int num2);

if (!isNum1Parsed || !isNum2Parsed)
{
    Console.WriteLine("Invalid input!");
}
else
{
    Calculator(num1, num2);
}