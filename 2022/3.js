const fs = require("fs");

const map = [
  "a",
  "b",
  "c",
  "d",
  "e",
  "f",
  "g",
  "h",
  "i",
  "j",
  "k",
  "l",
  "m",
  "n",
  "o",
  "p",
  "q",
  "r",
  "s",
  "t",
  "u",
  "v",
  "w",
  "x",
  "y",
  "z",
  "A",
  "B",
  "C",
  "D",
  "E",
  "F",
  "G",
  "H",
  "I",
  "J",
  "K",
  "L",
  "M",
  "N",
  "O",
  "P",
  "Q",
  "R",
  "S",
  "T",
  "U",
  "V",
  "W",
  "X",
  "Y",
  "Z",
];

const result = fs
  .readFileSync("3.txt")
  .toString()
  .split("\r\n")
  .map((r) => ({
    a: r.substring(0, r.length / 2).split(""),
    b: r.substring(r.length / 2).split(""),
  }))
  .map((r) => r.a.filter((l) => r.b.indexOf(l) > -1)[0])
  .reduce((prev, curr) => prev + map.indexOf(curr) + 1, 0);

console.log("3a", result);
