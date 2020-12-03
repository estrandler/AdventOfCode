const fs = require("fs");
const rows = fs.readFileSync("3.txt").toString().split("\r\n");

const slopes = [
  { right: 1, down: 1 },
  { right: 3, down: 1 },
  { right: 5, down: 1 },
  { right: 7, down: 1 },
  { right: 1, down: 2 },
];

const calculateNumberOfTrees = ({ right, down }) => {
  return rows
    .map((row, i) => ({
      x: ((i + 1) * right) % row.split("").length,
      y: (i + 1) * down,
    }))
    .filter(({ y }) => !!rows[y])
    .filter(({ x, y }) => rows[y].split("")[x % rows.length] === "#").length;
};

console.log("3a", calculateNumberOfTrees(slopes[1]));
console.log(
  "3b",
  slopes.reduce((prev, next) => prev * calculateNumberOfTrees(next), 1)
);
