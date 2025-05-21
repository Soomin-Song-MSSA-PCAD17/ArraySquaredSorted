using static SortedArrays;
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