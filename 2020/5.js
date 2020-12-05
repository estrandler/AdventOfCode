const fs = require("fs");
const passes = fs
  .readFileSync("5.txt")
  .toString()
  .split("\r\n")
  .map((pass) => ({
    rowArray: pass.substring(0, 7).split(""),
    columnArray: pass.substring(7).split(""),
  }));

const calculateSeatFromInstructions = ({ low, high }, instruction) => {
  return ["F", "L"].some((i) => i === instruction)
    ? {
        low,
        high: high - Math.floor((high - low) / 2) - 1,
      }
    : {
        low: low + Math.floor((high - low) / 2) + 1,
        high,
      };
};

const computeSeatId = ({ rowArray, columnArray }) => {
  const row = rowArray.reduce(calculateSeatFromInstructions, {
    low: 0,
    high: 127,
  });
  const seat = columnArray.reduce(calculateSeatFromInstructions, {
    low: 0,
    high: 7,
  });

  return row.low * 8 + seat.low;
};

const generateFirstAndLastRow = [0, 1, 2, 3, 4, 5, 6, 7].concat(
  [0, 1, 2, 3, 4, 5, 6, 7].map((seat) => 127 * 8 + seat)
);

//RESULT
const A = passes.reduce((prev, instructions) => {
  return Math.max(prev, computeSeatId(instructions));
}, 0);

const B = passes
  .map(computeSeatId)
  .concat(generateFirstAndLastRow)
  .sort()
  .reduce((prev, current, index, all) => {
    if (
      !all.find((a) => a === current + 1) &&
      all.find((a) => a === current + 2)
    )
      return current + 1;

    return prev;
  }, 0);

console.log("1a", A);
console.log("1b", B);
