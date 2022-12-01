const fs = require("fs");

const calories = fs
  .readFileSync("1.txt")
  .toString()
  .split("\r\n")
  .reduce(
    (prev, curr) => {
      const value = parseInt(curr);
      if (isNaN(value)) {
        prev.push(0);
      } else {
        prev[prev.length - 1] += value;
      }

      return prev;
    },
    [0]
  )
  .sort((a, b) => b - a);

console.log("1a", calories[0]);
console.log("1b", calories[0] + calories[1] + calories[2]);
