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

Demo([-1, 1, 2, 3]);
Demo([1, 2, 3, 4, 5]);
Demo([-4, -3, -2, -1,0]);
Demo([-3, -2, 0, 5]);

void Demo(int[] array)
{
    Console.WriteLine($"Input:  [{String.Join(", ", array)}]");
    Console.WriteLine($"Output: [{String.Join(", ", SortedSquares(array))}]\n");
}


int[] SortedSquares(int[] array)
{
    int[] squaredArray = new int[array.Length];

    #region set pointers
    // iterate through to find when the sign changes
    // two pointers, one going left looking at negative numbers
    // one going right looking at positive numbers
    // initialized to avoid compiler error

    int leftPointer =0;
    int rightPointer=array.Length;
    
    // 3 cases: all negative, all positive, mix of two
    if (array[0]>=0) // all positive
    {
        leftPointer = -1;
        rightPointer = 0;
    }
    else if(array[array.Length - 1] <= 0) // all negative
    {
        leftPointer = array.Length - 1;
        rightPointer = array.Length;
    }
    else // mix of pos and neg
    {
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] < 0 && array[i] >= 0)
            {
                leftPointer = i - 1;
                rightPointer = i;
            }
        }
    }
    #endregion
    #region populate array
    // compare absolute values to determine which square would be smaller
    // put the smaller square in, then shift whichever pointer that we used
    for (int i = 0; i < squaredArray.Length; i++)
    {
        bool validLeftPointer = leftPointer>=0;
        bool validRightPointer = rightPointer<squaredArray.Length;

        if (!validRightPointer || // if rightPointer is invalid, then we should use leftPointer without having to compare
            validLeftPointer && // if leftPointer is invalid, then we should use rightPointer without having to compare
            Math.Abs(array[leftPointer]) < array[rightPointer] // figure out which points to smaller number
            )
        {
            squaredArray[i] = array[leftPointer] * array[leftPointer];
            leftPointer--;
        }
        else
        {
            squaredArray[i] = array[rightPointer] * array[rightPointer];
            rightPointer++;
        }
    }
    #endregion

    return squaredArray;
}