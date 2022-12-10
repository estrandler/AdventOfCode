const fs = require("fs");

const input = fs
  .readFileSync("8.txt")
  .toString()
  .split("\r\n")
  .map((row) => row.split(""));

let visible = 0;
let scenic = 0;

const countWhileHigher = (arr, val) =>
  arr.reduce(
    (prev, curr) => {
      if (val > curr) {
        prev.count++;
      } else if (val === curr) {
        prev.count++;
        prev.continue = false;
      } else {
        prev.continue = false;
      }

      return prev;
    },
    {
      count: 0,
      continue: true,
    }
  );

for (let i = 0; i < input.length; i++) {
  for (let j = 0; j < input[i].length; j++) {
    const val = parseInt(input[i][j]);

    const above = input.map((r) => parseInt(r[j])).filter((_, idx) => idx < i);
    const below = input.map((r) => parseInt(r[j])).filter((_, idx) => idx > i);
    const left = input[i].map((r) => parseInt(r)).filter((_, idx) => idx < j);
    const right = input[i].map((r) => parseInt(r)).filter((_, idx) => idx > j);

    const visibleAbove = above.every((v) => v < val);
    const visibleBelow = below.every((v) => v < val);
    const visibleLeft = left.every((v) => v < val);
    const visibleRight = right.every((v) => v < val);

    if (visibleAbove || visibleBelow || visibleLeft || visibleRight) {
      visible++;
    }

    const scenicRight = countWhileHigher(right, val);
    const scenicBelow = countWhileHigher(below, val);
    const scenicAbove = countWhileHigher(above.reverse(), val);
    const scenicLeft = countWhileHigher(left.reverse(), val);

    const totalScenicness =
      scenicRight.count *
      scenicBelow.count *
      scenicAbove.count *
      scenicLeft.count;

    scenic = Math.max(scenic, totalScenicness);
  }
}

console.log("a", visible); //1715
console.log("b", scenic); //5762400
