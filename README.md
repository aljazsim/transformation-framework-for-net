# Transformation Framework for .NET

Transformation framework offers programmers an easy way of cleaning up property values.

## Transformations

Object can be transformed partially by transforming the value of a specific property or as a whole by transforming the values of all its properties.

### How to use the Transformation Framework

When using the Transformation Framework the programmer has two options of including the transformation functionality:

- By inheriting from the Transformable class:

 ```csharp
using TransformationFramework;

public class Example : Transformable
{
}
```

- Sometimes you might not have the option of inheriting from a certain class. In such a case, you can implement the ITransformable interface:

 ```csharp
using TransformationFramework;

public class Example : ITransformable
{
    public virtual IEnumerable<string> GetActiveTransformationContexts()
    {
      return Array.Empty<string>();
    }

    public virtual void Transform(string propertyName, object propertyValue, string transformationContext)
    {
        this.TransformAttributes(propertyName, propertyValue, transformationContext);
    }
}
```

Make sure that GetActiveTransformationContexts() and Transform() methods are marked as virtual if you are creating a transformable base class, so you can override them in sub-classes!
By choosing either of the two options and with the help of extension methods, you can do the following:

 ```csharp
Example example;

example = new Example();
example.Transform(); // transform all properties
example.Transform(nameof(example.Name)); // transform a specific property
```

### Transformation attributes

The easiest way of transforming property values is by using transformation attributes:

 ```csharp
public class Example : Transformable
{
  [ToUpperCaseString()]
  public string Name
  {
    get;
    set;
  }
}
```

The example above transforms the value of property Name to upper case.

Transformation framework contains a large number of transformation attributes to help you easily set transformations on properties. For example:

- ToUpperCaseString (transforms the string value to upper case),
- ToLowerCaseString (transforms the string value to upper case),
- ToTrimmedString (trims the string).

and many more. See the source code for a complete list.

Using transformation attributes is simple and requires little code to write, keeping your classes short and easy to understand. Making your own custom transformation attributes is easy to do (see section on extending Transformation Framework).
Not all transformation attributes can be used with all property types. For example using ToUpperCaseString attribute on an integer property makes no sense and will throw a TransformationErrorException:

 ```csharp
public class Example : Transformable
{
  [ToUpperCaseString()]
  public int Length
  {
    get;
    set;
  }
}

Example example;

example = new Example();
example.Length = 2;

try
{
    example.Transform();
}
catch (TransformationErrorException ex)
{
   // ex.Message == "Unhandled transformation exception occurred."
   // ex.InnerException.Message == "Value must be of type String."
}
```

All transformation attributes check if value type is compatible with their transformation procedure.

### Custom transformation

Using attributes might not be flexible enough for all scenarios. In that case you have the option of performing transformations within the Transform() method:

 ```csharp
public override void Transform(string propertyName, object propertyValue, string transformationContext)
{
    base.Transform(propertyName, propertyValue, transformationContext);

    if (propertyName == nameof(this.Property1))
    {
        if (this.Property1 != null)
        {
            this.Property1 = $"{this.Property1}...";
        }
    }
}
```

Make sure you call the base.Transform(...) method at the beginning of the method; otherwise attribute transformations will be skipped!

### Transformation context

Sometimes you want the transformation attributes to be executed only  in certain cases. This is where transformation contexts comes in. The default transformation context is always used (TransformationContext.Default), which has actually null as a value.

Example:

 ```csharp
public sealed class Example : Transformable
{
    [ToUpperCaseString(TransformationContext = "long")]
    [ToLowerCaseString(TransformationContext = "short")]
    public string Property1
    {
        get;
        set;
    }

    public override IEnumerable<string> GetActiveTransformationContexts()
    {
        if (this.Property1 == null)
        {
            return base.GetActiveTransformationContexts();
        }
        else if (this.Property1.Length < 10)
        {
            return new string[] { "short" };
        }
        else
        {
            return new string[] { "long" };
        }
    }
}
```

In this case the value of property Property1 will be transformed to a lower case string if the containing string is shorter than 10 characters, otherwise it will be transformed to an upper case string. If the property value equals null, none of the transformation attributes will be executed, since the default transformation context is active in that case. The Default transformation context will always be executed, but you have the option of adding as many different active transformation contexts as needed that change according to the internal state of the object by overriding the GetActiveTransformationContexts() method.

### Transformation priority

Sometimes you have multiple transformation rules that can be executed:

```csharp
[ToUpperCaseString(TransformationPriority = 0)]
[ToLowerCaseString(TransformationPriority = 1)]
public string Property1
{
    get;
    set;
}
 ```

In such a case you have control over the order of the execution of the transformation attributes. The attributes will be executed in ascending order of their transformation priorities. Default value for the transformation priority is 0.

## Extending the Transformation Framework

If you need a custom transformation attribute, you can implement your own. It is easy to do:

```csharp
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ToTrimmedStringAttribute : TransformationAttribute
{
    public override object Transform(object value)
    {
        if (value == null ||
            value == DBNull.Value)
        {
            return value;
        }
        else
        {
            value.MustBeTypeOf(typeof(string));

            return ((string)value).Trim();
        }
    }
}
```

Instructions:

- Make sure you derive from the TransformationAttribute class.
- Make sure you use the AttributeUsage attribute. Just copy it from  the example.
- Make sure you handle null and DbNull values correctly.
- When expecting a value of a specific type, make sure you validate the value type.

Using the custom attribute:

```csharp
[ToTrimmedString()]
public string Name
{
  get;
  set;
}
```

Sometimes you need to provide the attribute with additional parameters:

```csharp
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ToTrimmedStringAttribute : TransformationAttribute
{
    public ToTrimmedStringAttribute(params char[] trimCharacters)
    {
        this.TrimCharacters = trimCharacters;
    }

    public ToTrimmedStringAttribute()
    {
    }
    
    public char[] TrimCharacters
    {
        get;
    }
    
    public override object Transform(object value)
    {
        if (value == null ||
            value == DBNull.Value)
        {
            return value;
        }
        else
        {
            value.MustBeTypeOf(typeof(string));

            if (this.TrimCharacters == null ||
        this.TrimCharacters.Length == 0)
            {
                return ((string)value).Trim();
            }
            else
            {
                return ((string)value).Trim(this.TrimCharacters);
            }
        }
    }
}
```

Using the custom attribute:

```csharp
[ToTrimmedString('.', ',', '-')]
public string Name
{
  get;
  set;
}
```

After you've created a custom attribute, you can use it as many times as you want. If your attribute throws an exception during transformation for whatever reason, a TransformationErrorException will be thrown during invocation of of the Transform() method, containing the original exception in its InnerException property, making it easier for you to debug.

## Installation

Run the following command in your Package Manager Console in Visual Studio:

```csharp
Install-Package TransformationFramework 
```

Or just simply search for TransformationFramework in NuGet package manager in Visual Studio.

## Guidelines

- Transformation Framework can be used in different environments, like console applications, web applications, desktop applications, mobile applications or class libraries. You can use it to clean up user input, request payloads, configuration, etc. It is flexible, but does not constrain you.
- Implement custom transformations attributes whenever you can. They will make your code short, clean and reusable.
- When you need to perform transformation whose result depends on value of more than one property, do so by overriding the Transform() method. That cannot be done with a transformation attribute.
- Transformation Framework is not thread safe.
