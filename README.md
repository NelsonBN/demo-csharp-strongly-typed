# Demo CSharp Strongly Typed (Value Object)

An example of a strongly typed object in CSharp. Capable of initializing from primitive types, ability to compare and self-validate

* [Model](#model)
* [Initialization](#initialization)
  * [From double](#initialization-from-double)
  * [From string](#initialization-from-string)
  * [From int](#initialization-from-int)
* [Comparation](#comparation)
* [Validation](#validation)
    * [Return bool (TryParse)](#validation-try-parse)
    * [Return exception (Parse)](#validation-parse)



## Model <a name="model"></a>
```csharp
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
```



## Initialization <a name="initialization"></a>

### From double <a name="initialization-from-double"></a>
```csharp
double val = 21.12;
DegreesCelsius degrees = val;
```

### From string <a name="initialization-from-string"></a>
```csharp
string val = "27.21";
DegreesCelsius degrees = val;
```

### From int <a name="initialization-from-int"></a>
```csharp
int val = 32;
DegreesCelsius degrees = val;
```



## Comparation <a name="comparation"></a>

```csharp
DegreesCelsius degrees1 = 22.74;
DegreesCelsius degrees2 = 21.74;

// true
var result = degrees1 > degrees2;

// true
var result = degrees1 >= degrees2;

// false
var result = degrees1 == degrees2;

// false
var result = degrees1 != degrees2;

// false
var result = degrees1 < degrees2;

// false
var result = degrees1 <= degrees2;
```



## Validation <a name="validation"></a>

### Return bool (TryParse) <a name="validation-try-parse"></a>

```csharp
var val = 22.74;
DegreesCelsius? degrees;
bool success = DegreesCelsius.TryParse(val, out degrees);
```

### Return exception (Parse) <a name="validation-parse"></a>

```csharp
var val = -322.74;
try
{
    DegreesCelsius degrees DegreesCelsius.Parse(val);
}
catch(Exception exception)
{
    // ...
}
```



## LICENSE

[MIT](https://github.com/NelsonBN/demo-csharp-strongly-typed/blob/main/LICENSE)