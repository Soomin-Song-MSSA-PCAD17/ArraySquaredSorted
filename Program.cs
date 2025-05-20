using System.Diagnostics;

// write a function that accepts an array of int, sorted asc
// return a new array that are squares, also sorted in asc

// input: [1,2,3,4]
// output: [1,4,9,16]

// input [-3,-2,0,5]
// output: [0,4,9,25]

// input [-3,0,3]
// output: [0,9,9]

// input [-3,1,2,3]
// output: [1,4,9,9]

// TODO: didn't handle minvalue and maxvalue yet
// input [int.MaxValue]
// output [(whatever the number is, in long)]

Demo([1, 2, 3, 4, 5]); //all positive input
Demo([-4, -3, -2, -1]); //all negative input
Demo([-3, -2, 0, 5]); // mix of negative, zero, and positive
Demo([]); // empty array

// ArgumentNullException should be thrown for null input
try { Demo(null); } catch (ArgumentNullException e) { Console.WriteLine(e.Message); }
Console.ReadKey();

void Demo(int[] array)
{
    int[] newArray = SortedSquares(array);
    Console.WriteLine($"Input:  [{String.Join(", ", array)}]");
    Console.WriteLine($"Output: [{String.Join(", ", newArray)}]\n");
}


int[] SortedSquares(int[] array)
{
    // handle edge cases: null or empty array
    ArgumentNullException.ThrowIfNull( array );
    if(array.Length == 0) { return []; }

    int[] squaredArray = new int[array.Length];

    #region set pointers
    // search for first nonnegative integer to set as rPointer
    // assign lPointer to rightmost element if all elements are negative
    int lPointer = array.Length - 1;
    int rPointer = array.Length;
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] >= 0)
        {
            // if i=0, then lPointer=-1; validLPointer will catch this and prevent IndexOutOfRangeException
            lPointer = i - 1;
            rPointer = i;
            break;
        }
    }
    Debug.Assert(lPointer == rPointer - 1);

    #endregion set pointers
    #region populate array
    // lPointer moves left away from zero, rPointer moves right away from zero
    // compare absolute values of the two pointers to determine which square is smaller
    // put the smaller square into squaredArray, then shift whichever pointer was used
    bool validLPointer = lPointer >= 0;
    bool validRPointer = rPointer < squaredArray.Length;
    int newArrayIndex = 0;
    Debug.Assert(validLPointer || validRPointer); // at least one of the pointers should be valid

    #region append functions
    void AppendFromLeft()
    {
        squaredArray[newArrayIndex] = array[lPointer] * array[lPointer];
        lPointer--;
        validLPointer = lPointer >= 0;
        newArrayIndex++;
    }
    void AppendFromRight()
    {
        squaredArray[newArrayIndex] = array[rPointer] * array[rPointer];
        rPointer++;
        validRPointer = rPointer < squaredArray.Length;
        newArrayIndex++;
    }
    #endregion append functions

    //exit loop if either pointer is invalid
    while (validLPointer && validRPointer)
    {
        // determine which has smaller square
        // array[lPointer] is always negative, array[rPointer] is always 0 or positive
        if (-array[lPointer] <= array[rPointer])
        { AppendFromLeft(); } // lPointer points to the number with smaller or equal square
        else
        { AppendFromRight(); } // rPointer points to the number with smaller square 
    }

    //if only lPointer is valid, stop checking validRPointer
    if (!validRPointer)
    {
        while (validLPointer) { AppendFromLeft(); }
    }
    //if only rPointer is valid, stop checking validLPointer
    else if (!validLPointer)
    {
        while (validRPointer) { AppendFromRight(); }
    }
    Debug.Assert(newArrayIndex == array.Length);
    #endregion populate array

    return squaredArray;
}