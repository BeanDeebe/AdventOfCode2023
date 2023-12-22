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


# Day Two
## Part One
For part one, the goal was to be able to judge whether games were possible based on different rounds & a possible number of balls that were in a bag. 

For instance, with 12 red, 13 green, and 14 blue balls in a bag, the goal was to validate that every "round" where balls were pulled out of the bag had been possible. So, a valid round would be: `Red:7, Green:1, Blue:1`, and an invalid round would be `Red:20, Green:1, Blue:1`.

If a game was considered Valid, then I would add that game's round number to a sum. 

For this, I decided to utilize a hashtable to keep track of the number of balls in the bag, as well as the game sum of valid games. I read each line in from the input file, parsed the string to contain only the data I cared about (the rounds themselves), and then used a few different functions to check the validity of the rounds.

To keep track of game rounds, I used a counter variable initialized at 1, which would increase for each file line I read.

## Part Two
Part two did not care any longer about the game sum, but instead was focused on what the minimum number of balls would be in each round to make every game valid. Then, I would get the "power" of each game by multiplying the min number of red, green, and blue balls together. Lastly, I added this powers value to a game sum, and returned.

The approach to this problem was a bit different, as I no longer needed to check for validity in each game. Instead, I extended my hashtable to account for the "current max" ball count for each color, and kept track over each game before doing the powers and addition functionality.