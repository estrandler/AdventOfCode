const fs = require("fs");

const map = {
  one: {
    "A X": 1 + 3, //Rock rock
    "A Y": 2 + 6, //Rock paper
    "A Z": 3 + 0, //Rock scissors
    "B X": 1 + 0, //Paper rock
    "B Y": 2 + 3, //Paper paper
    "B Z": 3 + 6, //Paper scissors
    "C X": 1 + 6, //Scissors rock
    "C Y": 2 + 0, //Scissors paper
    "C Z": 3 + 3, //Scissors scissors
  },
  two: {
    "A X": 3 + 0, //Rock lose
    "A Y": 1 + 3, //Rock draw
    "A Z": 2 + 6, //Rock win
    "B X": 1 + 0, //Paper lose
    "B Y": 2 + 3, //Paper draw
    "B Z": 3 + 6, //Paper win
    "C X": 2 + 0, //Scissors lose
    "C Y": 3 + 3, //Scissors draw
    "C Z": 1 + 6, //Scissors win
  },
};

const [score, score2] = fs
  .readFileSync("2.txt")
  .toString()
  .split("\r\n")
  .reduce(
    (prev, curr) => {
      return [prev[0] + map.one[curr], prev[1] + map.two[curr]];
    },
    [0, 0]
  );

console.log("2a", score);
console.log("2b", score2);
