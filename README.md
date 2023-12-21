# AdventOfCode2023

# Day One
## Part One
The solution for day one, part one required reading in a text file and then parsing that text file for numbers. 

Once the line was parsed, the goal was to take the first digit found and "glue" it together with the last digit found, then use that number to add to a final result. 
Ex: 
  line: hczsqfour3nxm5seven4
  first digit: 3
  last digit: 4
  number to add to result: 34

I accomplished this by learning how to use StreamReader to read the file, a char array to process each line's characters, and StringBuilder to build the final number before parsing it to an int for adding to the result.

## Part Two
Part two was quite a bit more complex than I initially thought. The goal for part 2 was to now find the numbers that were spelled out in the line item, and convert those to digits for processing a result.
Ex:
  line: hczsqfour3nxm5seven4
  first digit: 4
  last digit: 4
  number to add to result: 44

While it looked easy at first, I ended up struggling on the edge case where two numbers might overlap -- for instance, `oneight` is 1 and 8.

To solve for this, I used a Hashmap to store string values "one" ... "nine" with their int equivalents. Then, I would read each line, and check for each string. If I found one, I would replace the string with the first letter of the number, the number, and then the last number. So, "one" would become "o1e". Then, the rest of the program ran as the first part to glue the digits together and add to the result.
