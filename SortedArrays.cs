using System.Diagnostics;

class SortedArrays
{
    public static int[] SortedSquares(int[] array)
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
        void AppendFromLPointer()
        {
            squaredArray[newArrayIndex] = array[lPointer] * array[lPointer];
            lPointer--;
            validLPointer = lPointer >= 0;
            newArrayIndex++;
        }
        void AppendFromRPointer()
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
            { AppendFromLPointer(); } // lPointer points to the number with smaller or equal square
            else
            { AppendFromRPointer(); } // rPointer points to the number with smaller square 
        }

        //if only lPointer is valid, stop checking validRPointer
        if (!validRPointer)
        { while (validLPointer) { AppendFromLPointer(); }}
        //if only rPointer is valid, stop checking validLPointer
        else if (!validLPointer)
        { while (validRPointer) { AppendFromRPointer(); }}

        Debug.Assert(newArrayIndex == array.Length);
        #endregion populate array

        return squaredArray;
    }
}