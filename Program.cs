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
    // iterate through to find when the sign changes
    // find the first nonnegative integer
    // if it's not found, set left pointer to rightmost element
    int lPointer  = array.Length - 1;
    int rPointer = array.Length;
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] >= 0)
        {
            lPointer = i - 1;
            rPointer = i;
            break;
        }
    }

    #endregion
    #region populate array
    // two pointers, one going left looking at negative numbers
    // one going right looking at positive numbers
    // compare absolute values to determine which square would be smaller
    // put the smaller square in, then shift whichever pointer that we used

    bool validLPointer = lPointer >= 0;
    bool validRPointer = rPointer < squaredArray.Length;
    int newArrayIndex = 0;

    //exit loop if either pointer is invalid
    while(validLPointer && validRPointer)
    {
        // figure out which points to smaller number
        if (Math.Abs(array[lPointer]) < array[rPointer])
        {
            // lPointer points to the number with smaller square
            squaredArray[newArrayIndex] = array[lPointer] * array[lPointer];
            lPointer--;
            validLPointer = lPointer >= 0;
            Debug.Assert(validLPointer);
            newArrayIndex++;
        }
        else
        {
            // rPointer points to the number with smaller square, or they are the same
            squaredArray[newArrayIndex] = array[rPointer] * array[rPointer];
            rPointer++;
            validRPointer = rPointer < squaredArray.Length;
            Debug.Assert(validRPointer);
            newArrayIndex++;
        }
    }

    //if only lPointer is valid, stop looking at rPointer
    if(!validRPointer)
    {
        while (validLPointer)
        {
            // same as above loop
            squaredArray[newArrayIndex] = array[lPointer] * array[lPointer];
            lPointer--;
            validLPointer = lPointer >= 0;
            Debug.Assert(validLPointer);
            newArrayIndex++;
        }
    }

    //if only rPointer is valid, stop looking at lPointer
    else if (!validLPointer)
    {
        while (validRPointer)
        {
            // same as above loop
            squaredArray[newArrayIndex] = array[rPointer] * array[rPointer];
            rPointer++;
            validRPointer = rPointer < squaredArray.Length;
            Debug.Assert(validRPointer);
            newArrayIndex++;
        }
    }
    #endregion

    return squaredArray;
}