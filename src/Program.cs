using System.Diagnostics;
using static System.Console;

var valueDoubleBefore = 23.47;
var degreesFromDouble = valueDoubleBefore;
var valueDoubleAfter = degreesFromDouble;

WriteLine($"Value double before: {valueDoubleBefore}");
WriteLine($"Degrees from double: {degreesFromDouble}");
WriteLine($"Value double after: {valueDoubleAfter}");

WriteLine();

var valueStringeBefore = "41.78";
var degreesFromString = valueStringeBefore;
var valueStringeAfter = degreesFromString;

WriteLine($"Value string before: {valueStringeBefore}");
WriteLine($"Degrees from string: {degreesFromString}");
WriteLine($"Value string after: {valueStringeAfter}");








WriteLine();
WriteLine();
WriteLine("******* TRY PARSE *******");
WriteLine();

var resultOk = DegreesCelsius.TryParse(12.841, out var valueOk);
WriteLine($"Result Ok: {resultOk}");
WriteLine($"Value Ok: {valueOk}");

WriteLine();

var resultNotOk = DegreesCelsius.TryParse(-384, out var valueTryNotOk);
WriteLine($"Result NotOk: {resultNotOk}");
WriteLine($"Value NotOk: {valueTryNotOk}");








WriteLine();
WriteLine();
WriteLine("******* PARSE *******");
WriteLine();

var result = DegreesCelsius.Parse(29.41);
WriteLine($"Result: {result}");

WriteLine();

try
{
    var notResult = DegreesCelsius.Parse(-384);
    WriteLine($"Result: {notResult}");
}
catch(Exception exception)
{
    WriteLine($"ERROR: {exception.Message}");
}








WriteLine();
WriteLine();
WriteLine("******* COMPARE *******");
WriteLine();

var degrees1 = 29.41;
var degrees2 = DegreesCelsius.Parse(29.41);
var degrees3 = 10.01;
var degrees4 = 100.18;

WriteLine($"Compare > Degrees1 ({degrees1}) == Degrees2 ({degrees2}) : {degrees1 == degrees2}");
WriteLine($"Compare > Degrees1 ({degrees1}) == Degrees3 ({degrees3}) : {degrees1 == degrees3}");
WriteLine($"Compare > Degrees1 ({degrees1}) < Degrees2 ({degrees2}) : {degrees1 < degrees2}");
WriteLine($"Compare > Degrees1 ({degrees1}) <= Degrees2 ({degrees2}) : {degrees1 <= degrees2}");
WriteLine($"Compare > Degrees1 ({degrees1}) >= Degrees2 ({degrees4}) : {degrees1 >= degrees4}");








WriteLine();
WriteLine();
WriteLine("******* SEARCH *******");
WriteLine();


const int TOTAL = 10_000_000;

var list = new List<DegreesCelsius>(TOTAL);
for(var count = 0; count < TOTAL; count++)
{
    list.Add(count);
}



var memorySizeMB = Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024;
DegreesCelsius degreesToSearch = -1;


var gcGen0Before = GC.CollectionCount(0);
var gcGen1Before = GC.CollectionCount(1);
var gcGen2Before = GC.CollectionCount(2);

var sw = Stopwatch.StartNew();
var contains = list.Contains(degreesToSearch);
sw.Stop();

var gcGen0After = GC.CollectionCount(0);
var gcGen1After = GC.CollectionCount(1);
var gcGen2After = GC.CollectionCount(2);


WriteLine($"Total items: {list.Count}");
WriteLine($"Memory: {memorySizeMB} MB");

WriteLine($"Expend time: {sw.ElapsedMilliseconds} ms");
WriteLine($"Gen0: {gcGen0After - gcGen0Before}");
WriteLine($"Gen1: {gcGen1After - gcGen1Before}");
WriteLine($"Gen2: {gcGen2After - gcGen2Before}");
WriteLine($"Contains: {contains}");











public readonly record struct DegreesCelsius
{
    private const double MIN = -273.15;

    private readonly double _value;

    private DegreesCelsius(double degrees)
        => _value = degrees;



    public static implicit operator DegreesCelsius(double degrees)
        => Parse(degrees);

    public static implicit operator DegreesCelsius(string degrees)
        => Parse(degrees);


    public static implicit operator string(DegreesCelsius degrees)
        => degrees.ToString();

    public static implicit operator double(DegreesCelsius degrees)
        => degrees._value;


    public static bool operator <(DegreesCelsius left, DegreesCelsius right)
        => left._value < right._value;

    public static bool operator >(DegreesCelsius left, DegreesCelsius right)
        => left._value > right._value;
    public static bool operator <=(DegreesCelsius left, DegreesCelsius right)
        => left._value <= right._value;

    public static bool operator >=(DegreesCelsius left, DegreesCelsius right)
        => left._value >= right._value;


    public bool Equals(DegreesCelsius other)
        => _value == other._value;

    public override int GetHashCode()
    => HashCode.Combine(GetType(), _value);



    public static DegreesCelsius Parse(double degrees)
    {
        if(TryParse(degrees, out var result))
        {
            return result!.Value;
        }

        throw new ArgumentException($"The value '{degrees}' is not valid", nameof(degrees));
    }

    public static bool TryParse(double degrees, out DegreesCelsius? result)
    {
        if(degrees < MIN)
        {
            result = null;
            return false;
        }

        result = new(degrees);

        return true;
    }


    public static DegreesCelsius Parse(string degrees)
    {
        if(TryParse(degrees, out var result))
        {
            return result!.Value;
        }

        throw new ArgumentException($"The value '{degrees}' is not valid", nameof(degrees));
    }

    public static bool TryParse(string degrees, out DegreesCelsius? result)
    {
        if(!double.TryParse(degrees, out var value))
        {
            result = null;
            return false;
        }

        return TryParse(value, out result);
    }


    public override string ToString()
        => _value.ToString();
}
