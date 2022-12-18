using System.Text;
using NUnit.Framework;

namespace DataDynamics.App.Tests;

public class LangTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void WriteLineDefault1()
    {
        // Declare without initializing.
        string message1;

        // Initialize to null.
        string message2 = null;

        // Initialize as an empty string.
        // Use the Empty constant instead of the literal "".
        string message3 = System.String.Empty;

        // Initialize with a regular string literal.
        string oldPath = "c:\\Program Files\\Microsoft Visual Studio 8.0";

        // Initialize with a verbatim string literal.
        string newPath = @"c:\Program Files\Microsoft Visual Studio 9.0";

        // Use System.String if you prefer.
        System.String greeting = "Hello World!";

        // In local variables (i.e. within a method body)
        // you can use implicit typing.
        var temp = "I'm still a strongly-typed System.String!";

        // Use a const string to prevent 'message4' from
        // being used to store another string value.
        const string message4 = "You can't get rid of me!";

        // Use the String constructor only when creating
        // a string from a char*, char[], or sbyte*. See
        // System.String documentation for details.
        char[] letters = { 'A', 'B', 'C' };
        string alphabet = new string(letters);
    }

    /// <summary>
    /// String Immutable 
    /// </summary>
    [Test]
    public void WriteLineDefault2()
    {
        string s1 = "A string is more ";
        string s2 = "than the sum of its chars.";

        // Concatenate s1 and s2. This actually creates a new
        // string object and stores it in s1, releasing the
        // reference to the original object.
        s1 += s2;

        System.Console.WriteLine(s1);
        // Output: A string is more than the sum of its chars.
    }

    [Test]
    public void WriteLineDefault3()
    {
        string columns = "Column 1\tColumn 2\tColumn 3";
        //Output: Column 1        Column 2        Column 3

        string rows = "Row 1\r\nRow 2\r\nRow 3";
        /* Output:
            Row 1
            Row 2
            Row 3
        */

        string title = "\"The \u00C6olean Harp\", by Samuel Taylor Coleridge";
        //Output: "The Æolean Harp", by Samuel Taylor Coleridge
    }

    [Test]
    public void WriteLineDefault4()
    {
        string filePath = @"C:\Users\scoleridge\Documents\";
        //Output: C:\Users\scoleridge\Documents\

        string text = @"My pensive SARA ! thy soft cheek reclined
    Thus on mine arm, most soothing sweet it is
    To sit beside our Cot,...";
        /* Output:
        My pensive SARA ! thy soft cheek reclined
            Thus on mine arm, most soothing sweet it is
            To sit beside our Cot,...
        */

        string quote = @"Her name was ""Sara.""";
        //Output: Her name was "Sara."
    }

    [Test]
    public void SubString()
    {
        string s3 = "Visual C# Express";
        System.Console.WriteLine(s3.Substring(7, 2));
        // Output: "C#"

        System.Console.WriteLine(s3.Replace("C#", "Basic"));
        // Output: "Visual Basic Express"

        // Index values are zero-based
        int index = s3.IndexOf("C");
        // index = 7
    }

    [Test]
    public void Character()
    {
        string s5 = "Printing backwards";

        for (int i = 0; i < s5.Length; i++)
        {
            System.Console.Write(s5[s5.Length - i - 1]);
        }
        // Output: "sdrawkcab gnitnirP"
    }

    [Test]
    public void LowerUpper()
    {
        string question = "hOW DOES mICROSOFT wORD DEAL WITH THE cAPS lOCK KEY?";
        System.Text.StringBuilder sb = new System.Text.StringBuilder(question);

        for (int j = 0; j < sb.Length; j++)
        {
            if (System.Char.IsLower(sb[j]) == true)
                sb[j] = System.Char.ToUpper(sb[j]);
            else if (System.Char.IsUpper(sb[j]) == true)
                sb[j] = System.Char.ToLower(sb[j]);
        }

        // Store the new string.
        string corrected = sb.ToString();
        System.Console.WriteLine(corrected);
        // Output: How does Microsoft Word deal with the Caps Lock key?
    }

    [Test]
    public void NullString()
    {
        string str = "hello";
        string nullStr = null;
        string emptyStr = String.Empty;

        string tempStr = str + nullStr;
        // Output of the following line: hello
        Console.WriteLine(tempStr);

        bool b = (emptyStr == nullStr);
        // Output of the following line: False
        Console.WriteLine(b);

        // The following line creates a new empty string.
        string newStr = emptyStr + nullStr;

        // Null strings and empty strings behave differently. The following
        // two lines display 0.
        Console.WriteLine(emptyStr.Length);
        Console.WriteLine(newStr.Length);
        // The following line raises a NullReferenceException.
        //Console.WriteLine(nullStr.Length);

        // The null character can be displayed and counted, like other chars.
        string s1 = "\x0" + "abc";
        string s2 = "abc" + "\x0";
        // Output of the following line: * abc*
        Console.WriteLine("*" + s1 + "*");
        // Output of the following line: *abc *
        Console.WriteLine("*" + s2 + "*");
        // Output of the following line: 4
        Console.WriteLine(s2.Length);
    }

    [Test]
    public void StringBuilder1()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder("Rat: the ideal pet");
        sb[0] = 'C';
        System.Console.WriteLine(sb.ToString());
        //Outputs Cat: the ideal pet
    }

    [Test]
    public void StringBuilder2()
    {
        var sb = new StringBuilder();

        // Create a string composed of numbers 0 - 9
        for (int i = 0; i < 10; i++)
        {
            sb.Append(i.ToString());
        }

        Console.WriteLine(sb); // displays 0123456789

        // Copy one character of the string (not possible with a System.String)
        sb[0] = sb[9];

        Console.WriteLine(sb); // displays 9123456789
    }

    [Test]
    public void WriteLineParams()
    {
        var pw = (firstName: "Phillis", lastName: "Wheatley", born: 1753, published: 1773);
        Console.WriteLine("{0} {1} was an African American poet born in {2}.", pw.firstName, pw.lastName, pw.born);
        Console.WriteLine("She was first published in {0} at the age of {1}.", pw.published, pw.published - pw.born);
        Console.WriteLine("She'd be over {0} years old today.", Math.Round((2018d - pw.born) / 100d) * 100d);
    }

    /// <summary>
    /// TryParse, Parse를 이용해서 사용자 입력의 유효성을 검사
    /// </summary>
    [Test]
    public void TypeTryParse()
    {
        int i = 0;
        string s = "108";
        bool result = int.TryParse(s, out i); //i now = 108 
        Console.Out.WriteLine(result);

        string numString = "1287543"; //"1287543.0" will return false for a long
        long number1 = 0;
        bool canConvert = long.TryParse(numString, out number1);
        if (canConvert == true)
            Console.WriteLine("number1 now = {0}", number1);
        else
            Console.WriteLine("numString is not a valid long");

        byte number2 = 0;
        numString = "255"; // A value of 256 will return false
        canConvert = byte.TryParse(numString, out number2);
        if (canConvert == true)
            Console.WriteLine("number2 now = {0}", number2);
        else
            Console.WriteLine("numString is not a valid byte");

        decimal number3 = 0;
        numString = "27.3"; //"27" is also a valid decimal
        canConvert = decimal.TryParse(numString, out number3);
        if (canConvert == true)
            Console.WriteLine("number3 now = {0}", number3);
        else
            Console.WriteLine("number3 is not a valid decimal");
    }

    [Test]
    public void StringSplit()
    {
        string phrase = "The quick brown    fox     jumps over the lazy dog.";
        string[] words = phrase.Split(' ');

        foreach (var word in words)
        {
            System.Console.WriteLine($"<{word}>");
        }
    }

    [Test]
    public void StringMultiDelimiter()
    {
        char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

        string text = "one\ttwo three:four,five six seven";
        System.Console.WriteLine($"Original text: '{text}'");

        string[] words = text.Split(delimiterChars);
        System.Console.WriteLine($"{words.Length} words in text:");

        foreach (var word in words)
        {
            System.Console.WriteLine($"<{word}>");
        }
    }

    [Test]
    public void StringOption()
    {
        string[] separatingStrings = { "<<", "..." };

        string text = "one<<two......three<four";
        System.Console.WriteLine($"Original text: '{text}'");

        string[] words = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
        System.Console.WriteLine($"{words.Length} substrings in text:");

        foreach (var word in words)
        {
            System.Console.WriteLine(word);
        }
    }

    [Test]
    public void StringFormat()
    {
        // https://learn.microsoft.com/ko-kr/dotnet/api/system.string.format?view=net-7.0
    }

    [Test]
    public void Exception1()
    {
        int x = 0;
        try
        {
            int y = 100 / x;
        }
        catch (ArithmeticException e)
        {
            Console.WriteLine($"ArithmeticException Handler: {e}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Generic Exception Handler: {e}");
        }
    }

    [Test]
    public void Exception2()
    {
        int x = 0;
        try
        {
            int y = 100 / x;
        }
        catch (Exception e)
        {
            Console.WriteLine("An exception ({0}) occurred.", e.GetType().Name);
            Console.WriteLine("Message:\n   {0}\n", e.Message);
            Console.WriteLine("Stack Trace:\n   {0}\n", e.StackTrace);
        }
    }
    [Test]
    public void InnerException()
    {
        // https://learn.microsoft.com/ko-kr/dotnet/api/system.exception?view=net-7.0

        int x = 0;
        try
        {
            try
            {
                int y = 100 / x;
            }
            catch (Exception e)
            {
                throw new InvalidCastException("0으로 나누기 하지 마세요...", e);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("An exception ({0}) occurred.", e.GetType().Name);
            Console.WriteLine("   Message:\n{0}", e.Message);
            Console.WriteLine("   Stack Trace:\n   {0}", e.StackTrace);
            Exception ie = e.InnerException;
            if (ie != null)
            {
                Console.WriteLine("   The Inner Exception:");
                Console.WriteLine("      Exception Name: {0}", ie.GetType().Name);
                Console.WriteLine("      Message: {0}\n", ie.Message);
                Console.WriteLine("      Stack Trace:\n   {0}\n", ie.StackTrace);
            }
        }
    }

}